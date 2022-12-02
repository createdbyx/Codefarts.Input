/*
<copyright>
  Copyright (c) 2012 Codefarts
  All rights reserved.
  contact@codefarts.com
  http://www.codefarts.com
</copyright>
*/

namespace Codefarts.Input.Interfaces
{
    using System;

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
        /// Occurs when a inputSource state changes.
        /// </summary>
        event EventHandler<InputSourceArgs> Changed;

        /// <summary>
        /// Polls the inputSource for changes and raises <see cref="Changed"/> event if a change occoured.
        /// </summary>     
        void Poll();
    }
}