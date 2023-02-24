/*
<copyright>
  Copyright (c) 2012 Codefarts
  All rights reserved.
  contact@codefarts.com
  http://www.codefarts.com
</copyright>
*/

namespace Codefarts.Input;

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
    protected BindingsCollection bindings;

    /// <summary>
    /// Gets or sets a list of registered input sources.
    /// </summary>
    protected InputSourceCollection inputSourcesList;

    private Dictionary<IInputSource, IEnumerable<PollingData>> polledSources = new Dictionary<IInputSource, IEnumerable<PollingData>>();

    /// <summary>
    /// Gets the list of bindings.
    /// </summary>
    public BindingsCollection Bindings
    {
        get
        {
            return this.bindings;
        }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="InputManager"/> class.
    /// </summary>
    public InputManager()
    {
        this.bindings = new BindingsCollection(this);
        this.inputSourcesList = new InputSourceCollection(this);
    }

    /// <summary>
    /// The Action event is raised every time a user inputSource change is detected and it matches a binding.
    /// </summary>
    public event EventHandler<BindingData> Action;

    /// <summary>
    /// Gets the collection os registered input sources.
    /// </summary>
    public InputSourceCollection InputSources
    {
        get
        {
            return this.inputSourcesList;
        }
    }

    /// <summary>
    /// Iterates through all bindings and polls any input sources that need polling.
    /// </summary>
    /// <remarks>Input sources are only polled once per update call.</remarks>
    public virtual void Update(TimeSpan totalTime, TimeSpan elapsedTime)
    {
        // group bindings by their input source
        var handler = this.Action;
        if (handler == null)
        {
            // no need to contunue if no handler
            return;
        }

        // clear previous polling data because we are starting a new polling run
        polledSources.Clear();
        for (var bindingIndex = 0; bindingIndex < this.bindings.Count; bindingIndex++)
        {
            // get the binding
            var binding = this.bindings[bindingIndex];

            // if binding input source not already polled then poll and cache polling data
            IEnumerable<PollingData> polledData = null;
            if (!polledSources.ContainsKey(binding.InputSource))
            {
                // poll binding input source and store the results for this update
                polledData = binding.InputSource.Poll(this.PollOnlyStateChanges);
                polledSources[binding.InputSource] = polledData;
            }

            // if no polling data it was polled previously so fetch the previously polled data from the cache
            polledData ??= polledSources[binding.InputSource];

            // loop thru each polled data
            foreach (var pData in polledData)
            {
                // if the sources do not match skip it
                if (!pData.Source.Equals(binding.Source, StringComparison.InvariantCulture))
                {
                    continue;
                }

                // otherwise update the binding values
                binding.TotalTime = totalTime;
                binding.ElapsedTime = elapsedTime;
                binding.PreviousValue = binding.Value;
                binding.Value = pData.Value;
                binding.DataType = pData.DataType;
                binding.Minimum = pData.Minimum;
                binding.Maximum = pData.Maximum;

                // update binding data
                this.bindings[bindingIndex] = binding;

                // raise action event
                handler.Invoke(this, binding);
            }
        }
    }

    /// <summary>
    /// Gets or sets weather polling input sources should return only state changes since last polling.
    /// </summary>
    /// <remarks>Default is true.</remarks>
    public bool PollOnlyStateChanges { get; set; } = true;
}