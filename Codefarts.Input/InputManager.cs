/*
<copyright>
  Copyright (c) 2012 Codefarts
  All rights reserved.
  contact@codefarts.com
  http://www.codefarts.com
</copyright>
*/

using System.Data;

namespace Codefarts.Input
{
    using System;
    using System.Collections.Generic;
    using Interfaces;
    using Models;

    /// <summary>
    /// The input manager is used to handle inputSource state changes.
    /// </summary>
    public class InputManager
    {
        /// <summary>
        /// Holds a collection of objects the implement the IBinder interface.
        /// </summary>
        protected List<BindingData> bindings;

        /// <summary>
        /// Gets or sets a list of registered inputSourcesDictionary.
        /// </summary>
        protected List<IInputSource> inputSourcesDictionary;

        /// <summary>
        /// Initializes a new instance of the <see cref="InputManager"/> class. 
        /// </summary>
        public InputManager()
        {
            this.bindings = new List<BindingData>();
            this.inputSourcesDictionary = new List<IInputSource>();
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

            for (int i = this.inputSourcesDictionary.Count - 1; i >= 0; i--)
            {
                var entry = this.inputSourcesDictionary[i];
                if (entry.Name.Equals(name, StringComparison.InvariantCulture))
                {
                    this.inputSourcesDictionary.RemoveAt(i);
                }
            }
            // var sources = this.inputSourcesDictionary.Where(x=>x.Name.Equals(name, StringComparison.InvariantCulture)).ToArray();
            // foreach (var s in sources)
            // {
            //     this.inputSourcesDictionary.Remove(s)
            // }
            //this.inputSourcesDictionary.Remove(source.Name);
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
                    return sources.Select(x => x.Name).ToArray();
                    // var keys = new string[sources.Count];
                    // sources.Keys.CopyTo(keys, 0);
//                    return keys;
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

            if (this.inputSourcesDictionary.Any(x => x.Name.Equals(inputSource.Name, StringComparison.InvariantCulture)))
            {
                throw new ArgumentException("Input source with same name already added.");
            }

            this.inputSourcesDictionary.Add(inputSource);

            //this.inputSourcesDictionary.Add(inputSource.Name, inputSource);
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
        /// Unbinds a binding.
        /// </summary>
        /// <param name="name">The name of the binding to unbind. See remarks for more info.</param>
        /// <remarks>
        /// The name parameter is case sensitive.
        /// </remarks>
        public virtual void Unbind(string name)
        {
            BindingData bindingData;
            //var list = this.bindings[name];
            for (int i = this.bindings.Count - 1; i >= 0; i--)
            {
                bindingData = this.bindings[i];
                if (bindingData.Name.Equals(name, StringComparison.InvariantCulture))
                {
                    this.bindings.RemoveAt(i);
                }
            }
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
        public virtual void Bind(string name, IInputSource device, string source, int player)
        {
            if (device == null)
            {
                throw new ArgumentNullException(nameof(device));
            }

            if (!this.inputSourcesDictionary.Any(x => x.Name.Equals(device.Name, StringComparison.InvariantCulture)))
            {
                throw new ArgumentException("InputSource with the name '" + device.Name + "' not found.");
            }

            // var dev = this.inputSourcesDictionary[device];
            // var any = false;
            // foreach (var x in dev.Sources)
            // {
            //     if (x.Equals(source))
            //     {
            //         any = true;
            //         break;
            //     }
            // }
            //
            // if (!any)
            // {
            //     throw new Exception(string.Format("InputSource '{0}' does not appear to contain the requested source '{1}'.", device, source));
            // }

            var data = new BindingData(name, device, source, player);
            // if (!this.bindings.ContainsKey(name))
            // {
            //     this.bindings.Add(name, new List<BindingData>());
            // }
            //
            // var list = this.bindings[name]
            // list.Add(data);
            this.bindings.Add(data);
        }

        /// <summary>
        /// Polls all inputSourcesDictionary that have been added to the InputManager.
        /// </summary>       
        public virtual void Update(TimeSpan totalTime, TimeSpan elapsedTime)
        {
            // group bindings by their input source 
            var handler = this.Action;
            if (handler == null)
            {
                return;
            }
            
            var polledSources = new Dictionary<IInputSource, IEnumerable<PollingData>>();
            for (var bindingIndex = 0; bindingIndex < this.bindings.Count; bindingIndex++)
            {
                // get the binding
                var binding = this.bindings[bindingIndex];

                // check if the binding input source has been polled yet and if not poll it storing the results
                if (!polledSources.ContainsKey(binding.InputSource))
                {
                    polledSources.Add(binding.InputSource, binding.InputSource.Poll());
                }

                // fetch polled data
                var polledData = polledSources[binding.InputSource];

                // loop thru each polled data
                foreach (var data in polledData)
                {
                    // if the sources do not match skip it
                    if (!data.Source.Equals(binding.Source, StringComparison.InvariantCulture))
                    {
                        continue;
                    }

                    // otherwise update the binding values
                    binding.TotalTime = totalTime;
                    binding.ElapsedTime = elapsedTime;
                    binding.PreviousValue = binding.Value;
                    binding.Value = data.Value;

                    // update binding data
                    this.bindings[bindingIndex] = binding;

                    // raise action event
                    handler.Invoke(this, binding);
                }
            }
        } 
    }
}