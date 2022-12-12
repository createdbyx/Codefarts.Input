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
        /// <exception cref="System.ArgumentException">InputSource name is null or missing.;name</exception>
        public virtual void RemoveSource(string name)
        {
            if (string.IsNullOrWhiteSpace(name?.Trim()))
            {
                throw new ArgumentException("InputSource name is null or missing.", "name");
            }

            var source = this.inputSourcesDictionary[name];
            this.inputSourcesDictionary.Remove(source.Name);
        }

        /// <summary>
        /// Gets the names of the inputSourcesDictionary that have been added.
        /// </summary>
        public virtual string[] Sources
        {
            get
            {
                var sources = this.inputSourcesDictionary;
                lock (sources)
                {
                    var keys = new string[sources.Count];
                    sources.Keys.CopyTo(keys, 0);
                    return keys;
                }
            }
        }

        /// <summary>
        /// Adds the inputSource.
        /// </summary>
        /// <param name="inputSource">The inputSource to add.</param>
        /// <exception cref="System.ArgumentNullException">inputSource</exception>
        /// <exception cref="System.ArgumentException">InputSource name is null or missing.;inputSource</exception>
        /// <remarks>Can not add the same inputSource twice.</remarks>
        public virtual void AddSource(IInputSource inputSource)
        {
            if (inputSource == null)
            {
                throw new ArgumentNullException(nameof(inputSource));
            }

            if (string.IsNullOrWhiteSpace(inputSource.Name))
            {
                throw new ArgumentException("Input source name is null or missing.", nameof(inputSource));
            }

            this.inputSourcesDictionary.Add(inputSource.Name, inputSource);
        }

        /// <summary>
        /// The Action event is raised every time a user inputSource change is detected and it matches a binding.
        /// </summary>
        public event EventHandler<BindingData> Action;

        /// <summary>
        /// Gets the number of inputSourcesDictionary that have been added.
        /// </summary>
        public virtual int NumberOfInputSources
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
            if (e == null)
            {
                throw new ArgumentNullException(nameof(e));
            }

            // only raise if the event has an invocation list.
            var handler = this.Action;
            handler?.Invoke(this, e);
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
        public virtual void Bind(string name, string device, string source, int player)
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
                throw new Exception(string.Format("InputSource '{0}' does not appear to contain the requested source '{1}'.", device, source));
            }

            var data = new BindingData(name, device, source, player);
            this.bindings.Add(name, data);
        }

        /// <summary>
        /// Polls all inputSourcesDictionary that have been added to the InputManager.
        /// </summary>       
        public virtual void Update(TimeSpan totalTime, TimeSpan elapsedTime)
        {
            // Find input sources that match the bindings we have set up 
            var sourcesToPoll = from outer in this.inputSourcesDictionary
                                from inner in this.bindings
                                where outer.Value.Name == inner.Value.InputSource
                                select outer;

            foreach (var inputSource in sourcesToPoll)
            {
                // Get input source data 
                var pollData = inputSource.Value.Poll();

                // Find bindings that match source values 
                var matches = from outer in pollData
                              from inner in this.bindings
                              where outer.Source == inner.Value.Source
                              select new { Binding = inner.Value, PolledValue = outer.Value };


                foreach (var match in matches)
                {
                    // update binding data
                    match.Binding.Value = match.PolledValue;
                    match.Binding.TotalTime = totalTime;
                    match.Binding.ElapsedTime = elapsedTime;

                    this.RaiseActionEvent(this, match.Binding.Clone() as BindingData);
                }
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