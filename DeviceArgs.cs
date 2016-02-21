namespace Codefarts.Input
{
    using System;

    using Codefarts.Input.Interfaces;

    /// <summary>
    /// Provides device arguments for <see cref="IDevice"/> implementations.
    /// </summary>
    /// <seealso cref="System.EventArgs" />
    public class DeviceArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.EventArgs" /> class.
        /// </summary>
        public DeviceArgs()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.EventArgs" /> class.
        /// </summary>
        /// <param name="device">The name of the device.</param>
        /// <param name="source">The device source.</param>
        /// <param name="value">The value from the device.</param>
        /// <param name="type">The type of value.</param>
        public DeviceArgs(string device, string source, float value, EventType type) : this()
        {
            this.Device = device;
            this.Source = source;
            this.Value = value;
            this.Type = type;
        }

        /// <summary>
        /// Gets or sets the name or id of the device.
        /// </summary>
        public string Device { get; set; }

        /// <summary>
        /// Gets or sets an array of available device sources.
        /// </summary>
        public string Source { get; set; }

        /// <summary>
        /// Gets or sets the value return from the device.
        /// </summary>
        /// <remarks>This could also refer to a device state such as a led light or gyroscope etc.</remarks>
        public float Value { get; set; }
                                    
        /// <summary>
        /// Gets or sets the type of <see cref="Value"/>.
        /// </summary>
        public EventType Type { get; set; }
    }
}