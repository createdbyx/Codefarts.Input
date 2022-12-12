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
    public class BindingData : PollingData  ,ICloneable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Object" /> class.
        /// </summary>
        /// <param name="name">The binding name.</param>
        /// <param name="inputSource">The inputSource.</param>
        /// <param name="source">The source.</param>
        /// <param name="state">The state.</param>
        /// <param name="player">The player id.</param>
        public BindingData(string name, string inputSource, string source, float value, int player)
            : base(inputSource, source, value)
        {
            this.Name = name;
            this.InputSource = inputSource;
            this.Source = source;
            this.Player = player;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Object" /> class.
        /// </summary>
        /// <param name="name">The binding name.</param>
        /// <param name="inputSource">The inputSource.</param>
        /// <param name="source">The source.</param>
        public BindingData(string name, string inputSource, string source)
            : this(name, inputSource, source, 0)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Object" /> class.
        /// </summary>
        /// <param name="name">The binding name.</param>
        /// <param name="inputSource">The inputSource.</param>
        /// <param name="source">The source.</param>
        /// <param name="player">The player id.</param>
        public BindingData(string name, string inputSource, string source, int player)
            : this(name, inputSource, source, 0f, player)
        {
        }

        public override float Value
        {
            get
            {
                return base.Value;
            }

            set
            {
                this.PreviousValue = base.Value;
                base.Value = value;
            }
        }

        /// <summary>
        /// Gets or sets the action name.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Gets or sets the player id.
        /// </summary>
        public int Player { get; private set; }

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

        public TimeSpan TotalTime { get; set; }
        public TimeSpan ElapsedTime { get; set; }
        
        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}