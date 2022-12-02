/*
<copyright>
  Copyright (c) 2012 Codefarts
  All rights reserved.
  contact@codefarts.com
  http://www.codefarts.com
</copyright>
*/

namespace Codefarts.Input
{
    using System;
    using System.Collections.Generic;
    using Codefarts.Input.Interfaces;
    using Codefarts.Input.Models;

    /// <summary>
    /// The input manager is used to handle inputSource state changes.
    /// </summary>
    public class InputManager
    {
        /// <summary>
        /// Holds a collection of objects the implement the IBinder interface.
        /// </summary>
        protected Dictionary<string, BindingData> bindings;

        /// <summary>
        /// Gets or sets a list of registered inputSourcesDictionary.
        /// </summary>
        protected Dictionary<string, IInputSource> inputSourcesDictionary;

        /// <summary>
        /// Initializes a new instance of the <see cref="InputManager"/> class. 
        /// </summary>
        public InputManager()
        {
            this.bindings = new Dictionary<string, BindingData>();
            this.inputSourcesDictionary = new Dictionary<string, IInputSource>();
        }

        /// <summary>
        /// Removes the inputSource using the inputSourcesDictionary name.
        /// </summary>
        /// <param name="name">The name of the inputSource to remove.</param>
        /// <exception cref="System.ArgumentException">Device name is null or missing.;name</exception>
        public virtual void RemoveDevice(string name)
        {
            if (name == null || name.Trim() == string.Empty)
            {
                throw new ArgumentException("Device name is null or missing.", "name");
            }

            var device = this.inputSourcesDictionary[name];
            device.Changed -= this.InputSourceStateChanged;
            this.inputSourcesDictionary.Remove(device.Name);
        }

        /// <summary>
        /// Gets the names of the inputSourcesDictionary that have been added.
        /// </summary>
        public virtual string[] Devices
        {
            get
            {
                var keys = new string[this.inputSourcesDictionary.Count];
                this.inputSourcesDictionary.Keys.CopyTo(keys, 0);
                return keys;
            }
        }

        /// <summary>
        /// Adds the inputSource.
        /// </summary>
        /// <param name="inputSource">The inputSource to add.</param>
        /// <exception cref="System.ArgumentNullException">inputSource</exception>
        /// <exception cref="System.ArgumentException">Device name is null or missing.;inputSource</exception>
        /// <remarks>Can not add the same inputSource twice.</remarks>
        public virtual void AddDevice(IInputSource inputSource)
        {
            if (inputSource == null)
            {
                throw new ArgumentNullException("inputSource");
            }

            if (string.IsNullOrWhiteSpace(inputSource.Name))
            {
                throw new ArgumentException("Input source name is null or missing.", "inputSource");
            }

            this.inputSourcesDictionary.Add(inputSource.Name, inputSource);
            inputSource.Changed += this.InputSourceStateChanged;
        }

        /// <summary>
        /// Handles inputSource state changes and updates binders.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The inputSource arguments.</param>
        protected virtual void InputSourceStateChanged(object sender, InputSourceArgs e)
        {
            foreach (var binding in this.bindings)
            {
                var data = binding.Value;
                if (!data.Device.Equals(e.Device) || !data.Source.Equals(e.Source))
                {
                    continue;
                }

                data.Value = e.Value;
                if (e.Type == EventType.Button)
                {
                    switch (data.State)
                    {
                        case PressedState.Pressed:
                            if (data.RelativeValue > 0)
                            {
                                this.RaiseActionEvent(this, data);
                            }

                            continue;

                        case PressedState.Released:
                            if (data.RelativeValue < 0)
                            {
                                this.RaiseActionEvent(this, data);
                            }

                            continue;

                        default:
                            if (Math.Abs(data.RelativeValue) > float.Epsilon)
                            {
                                this.RaiseActionEvent(this, data);
                            }

                            continue;
                    }
                }

                this.RaiseActionEvent(this, data);
            }
        }

        /// <summary>
        /// The Action event is raised every time a user inputSource change is detected and it matches a binding.
        /// </summary>
        public event EventHandler<BindingData> Action;

        /// <summary>
        /// Gets the number of inputSourcesDictionary that have been added.
        /// </summary>
        public virtual int DeviceCount
        {
            get
            {
                return this.inputSourcesDictionary.Count;
            }
        }

        /// <summary>
        /// Raises the action event.
        /// </summary>
        /// <param name="sender">The object sending the event.</param>
        /// <param name="e">A reference to an <see cref="BindingData"/> object containing information about the event.</param>
        public virtual void RaiseActionEvent(object sender, BindingData e)
        {
            // only raise if the event has an invocation list.
            var handler = this.Action;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        /// <summary>
        /// Unbinds a binding.
        /// </summary>
        /// <param name="name">The name of the binding to unbind. See remarks for more info.</param>
        /// <remarks>
        /// The name parameter is case sensitive.
        /// </remarks>
        public virtual void Unbind(string name)
        {
            BindingData? bindingData = null;
            this.bindings.Remove(name, out bindingData);
        }

        /// <summary>
        /// Unbinds all bindings.
        /// </summary>
        public virtual void UnbindAll()
        {
            this.bindings.Clear();
        }

        /// <summary>
        /// Binds the specified name to a inputSource and inputSource source.
        /// </summary>
        /// <param name="name">The name of the binder.</param>
        /// <param name="device">The inputSource name.</param>
        /// <param name="source">The source name on the inputSource.</param>
        /// <param name="pressState">The pressed state if the inputSource source is a button.</param>
        /// <param name="player">The player id associated with the binding.</param>
        /// <remarks>
        /// The name parameter is case sensitive.
        /// </remarks>
        public virtual void Bind(string name, string device, string source, PressedState pressState, int player)
        {
            if (!this.inputSourcesDictionary.ContainsKey(device))
            {
                throw new KeyNotFoundException("No inputSource with the name '" + device + "' was found.");
            }

            var dev = this.inputSourcesDictionary[device];
            var any = false;
            foreach (var x in dev.Sources)
            {
                if (x.Equals(source))
                {
                    any = true;
                    break;
                }
            }

            if (!any)
            {
                throw new Exception(string.Format("Device '{0}' does not appear to contain the requested source '{1}'.", device, source));
            }

            var data = new BindingData(name, device, source, pressState, player);
            this.bindings.Add(name, data);
        }

        /// <summary>
        /// Polls all inputSourcesDictionary that have been added to the InputManager.
        /// </summary>       
        public virtual void Update()
        {
            foreach (var device in this.inputSourcesDictionary)
            {
                device.Value.Poll();
            }
        }

        /// <summary>
        /// Gets the value of a binding.
        /// </summary>
        /// <param name="name">The binding name.</param>
        /// <param name="relative">if set to <c>true</c> the relative value will be returned based on it's previous value.</param>
        /// <param name="value">Will contain the value of the binding.</param>
        /// <returns>
        /// Returns true if the binder was found.
        /// </returns>
        /// <remarks>
        /// Keep in mind that the value may not represent the actual state of the hardware.
        /// The name parameter is case sensitive.
        /// </remarks>
        public virtual bool GetValue(string name, bool relative, out float value)
        {
            if (!this.bindings.ContainsKey(name))
            {
                value = 0;
                return false;
            }

            var binder = this.bindings[name];
            value = binder.Value - (relative ? binder.PreviousValue : 0);
            return true;
        }
    }
}