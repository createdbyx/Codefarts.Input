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
    /// Provides a interface for devices.
    /// </summary>
    public interface IDevice
    {
        #region Public Properties

        /// <summary>
        /// Gets the name or id of the device.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets an array of available device sources.
        /// </summary>
        string[] Sources { get; }

        #endregion

        /// <summary>
        /// Occurs when a device state changes.
        /// </summary>
        event EventHandler<DeviceArgs> Changed;

        #region Public Methods and Operators

        /// <summary>
        /// Polls the device for changes and raises <see cref="Changed"/> event is a change occoured.
        /// </summary>     
        void Poll();

        #endregion    
    }
}