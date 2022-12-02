/*
<copyright>
  Copyright (c) 2012 Codefarts
  All rights reserved.
  contact@codefarts.com
  http://www.codefarts.com
</copyright>
*/

namespace Codefarts.Input.Models
{
    using System;

    /// <summary>
    /// Provides a class for binding information.
    /// </summary>
    public class BindingData : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Object" /> class.
        /// </summary>
        /// <param name="name">The binding name.</param>
        /// <param name="device">The inputSource.</param>
        /// <param name="source">The source.</param>
        /// <param name="state">The state.</param>
        /// <param name="player">The player id.</param>
        public BindingData(string name, string device, string source, PressedState state, int player)
        {
            this.Name = name;
            this.Device = device;
            this.Source = source;
            this.State = state;
            this.Player = player;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Object" /> class.
        /// </summary>
        /// <param name="name">The binding name.</param>
        /// <param name="device">The inputSource.</param>
        /// <param name="source">The source.</param>
        public BindingData(string name, string device, string source)
            : this(name, device, source, PressedState.Toggle, 0)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Object" /> class.
        /// </summary>
        /// <param name="name">The binding name.</param>
        /// <param name="device">The inputSource.</param>
        /// <param name="source">The source.</param>
        /// <param name="player">The player id.</param>
        public BindingData(string name, string device, string source, int player)
            : this(name, device, source, PressedState.Toggle, player)
        {
        }

        /// <summary>
        /// The backing field for the <see cref="Value"/> property.
        /// </summary>
        private float value;

        /// <summary>
        /// Gets or sets the action name.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Gets or sets the inputSource name or id.
        /// </summary>
        public string Device { get; private set; }

        /// <summary>
        /// Gets or sets the source axis or button on the inputSource.
        /// </summary>
        /// <remarks>This could also point to a inputSource state such as a led light or gyroscope etc.</remarks>
        public string Source { get; private set; }

        /// <summary>
        /// Gets or sets the player id.
        /// </summary>
        public int Player { get; private set; }

        /// <summary>
        /// Gets or sets the state to compare against.
        /// </summary>
        /// <remarks>This is only used for non axis sources like buttons or led states.</remarks>
        public PressedState State { get; private set; }

        /// <summary>
        /// Gets or sets the value return from the inputSource.
        /// </summary>
        public float Value
        {
            get
            {
                return this.value;
            }

            set
            {
                this.PreviousValue = this.value;
                this.value = value;
            }
        }

        /// <summary>
        /// Gets or sets the previous value return from the inputSource.
        /// </summary>
        public float PreviousValue { get; private set; }

        /// <summary>
        /// Gets or sets the relative value.
        /// </summary>
        public float RelativeValue
        {
            get
            {
                return this.Value - this.PreviousValue;
            }
        }
    }
}