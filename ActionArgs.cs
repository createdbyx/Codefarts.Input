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

    using Codefarts.Input.Interfaces;

#if XNA
    using Microsoft.Xna.Framework;
#endif

    /// <summary>
    /// Action arguments.
    /// </summary>
    public class ActionArgs : EventArgs
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ActionArgs"/> class.
        /// </summary>
        /// <param name="elapsedGameTime">
        /// The elapsed game time.
        /// </param>
        /// <param name="totalGameTime">
        /// The total game time.
        /// </param>
        public ActionArgs(TimeSpan elapsedGameTime, TimeSpan totalGameTime)
        {
            this.ElapsedGameTime = elapsedGameTime;
            this.TotalGameTime = totalGameTime;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ActionArgs"/> class.
        /// </summary>
        /// <param name="binder">
        /// The binder.
        /// </param>
        public ActionArgs(IBinder binder)
        {
            this.Binder = binder;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ActionArgs"/> class.
        /// </summary>
        /// <param name="binder">
        /// The binder.
        /// </param>
        /// <param name="elapsedGameTime">
        /// The elapsed game time.
        /// </param>
        /// <param name="totalGameTime">
        /// The total game time.
        /// </param>
        public ActionArgs(IBinder binder, TimeSpan elapsedGameTime, TimeSpan totalGameTime)
        {
            this.Binder = binder;
            this.ElapsedGameTime = elapsedGameTime;
            this.TotalGameTime = totalGameTime;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ActionArgs"/> class.
        /// </summary>
        public ActionArgs()
        {
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the binder that is responsible for binding the action name to a device input.
        /// </summary>
        public IBinder Binder { get; set; }

        /// <summary>
        /// Gets or sets the elapsed game time value.
        /// </summary>
        public TimeSpan ElapsedGameTime { get; set; }

        /// <summary>
        /// Gets or sets the name of the action.
        /// </summary>
        public string Name
        {
            get
            {
                return this.Binder != null ? this.Binder.Name : string.Empty;
            }

            set
            {
                if (this.Binder != null)
                {
                    this.Binder.Name = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the player that invoked the action.
        /// </summary>
        public int Player
        {
            get
            {
#if XNA
                return this.Binder != null ? this.Binder.Player : (int)PlayerIndex.One; 
#else
                return this.Binder != null ? this.Binder.Player : 0;
#endif
            }

            set
            {
                if (this.Binder != null)
                {
                    this.Binder.Player = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the relative value for the action.
        /// </summary>
        public float RelativeValue
        {
            get
            {
                return this.Binder.RelativeValue;
            }

            set
            {
                this.Binder.RelativeValue = value;
            }
        }

        /// <summary>
        /// Gets or sets the total game time value.
        /// </summary>
        public TimeSpan TotalGameTime { get; set; }

        /// <summary>
        /// Gets the type of action event that occurred.
        /// </summary>
        public EventType Type
        {
            get
            {
                return this.Binder.Type;
            }
        }

        /// <summary>
        /// Gets or sets the value for the action.
        /// </summary>
        /// <remarks>Some buttons like the Playstation 2 controller are pressure 
        /// sensitive, so any non zero value can be considered to be in a pressed state.</remarks>
        public float Value
        {
            get
            {
                return this.Binder.Value;
            }

            set
            {
                this.Binder.Value = value;
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Gets the normalized relative value.
        /// </summary>
        /// <returns>
        /// Returns the value of the RelativeValue property after it has been normalized.
        /// </returns>
        public float NormalizedRelativeValue()
        {
            var value = this.Binder.RelativeValue;
            var num2 = value * value;
            var num = (float)(1f / Math.Sqrt(num2));
            return num;
        }

        /// <summary>
        /// Gets the normalized value.
        /// </summary>
        /// <returns>
        /// Returns the value of the Value property after it has been normalized.
        /// </returns>
        public float NormalizedValue()
        {
            var value = this.Binder.Value;
            var num2 = value * value;
            var num = 1f / (float)Math.Sqrt(num2);
            return float.IsInfinity(num) ? 0 : num;
        }

        #endregion
    }
}