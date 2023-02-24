/*
<copyright>
  Copyright (c) 2012 Codefarts
  All rights reserved.
  contact@codefarts.com
  http://www.codefarts.com
</copyright>
*/

using Codefarts.Input.Models;

namespace Codefarts.Input.Interfaces
{
    /// <summary>
    /// Provides a interface for input sources.
    /// </summary>
    public interface IInputSource
    {
        /// <summary>
        /// Gets the name or id of the input source.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets an array of available input sources.
        /// </summary>
        string[] Sources { get; }
        
        /// <summary>
        /// Polls the inputSource for changes and returns <see cref="Changed"/> event if a change occoured.
        /// </summary>     
        ///<param name="onlyStateChanges">If true only those values whoose state has changed since the last call to this method will be returned.</param>
        IEnumerable< PollingData> Poll(bool onlyStateChanges);
    }
}