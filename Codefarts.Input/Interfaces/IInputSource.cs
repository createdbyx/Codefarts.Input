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
    /// Provides a interface for inputSourcesDictionary.
    /// </summary>
    public interface IInputSource
    {
        /// <summary>
        /// Gets the name or id of the inputSource.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets an array of available inputSource sources.
        /// </summary>
        string[] Sources { get; }
 
        /// <summary>
        /// Polls the inputSource for changes and returns <see cref="Changed"/> event if a change occoured.
        /// </summary>     
        IEnumerable< PollingData> Poll();
    }
}