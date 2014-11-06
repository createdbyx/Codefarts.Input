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
    /// Provides an interface for a user action.
    /// </summary>
    public interface IBinder
    {
        #region Public Events

        /// <summary>
        /// Event for raising state change action events.
        /// </summary>
        event EventHandler<ActionArgs> Action;

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets a value indicating whether or not to raise the action event even though a state change has not occurred.
        /// </summary>
        /// <remarks>
        /// If set to true the action will be raised even if there was not a change in the device's state. For example this is useful if you want to 
        /// get the mouse position all the time, rather then only when the mouse position is actually changed.</remarks>
        bool AlwaysRaise { get; set; }

        /// <summary>
        /// Gets the type of device that raised the action.
        /// </summary>
        string Device { get; }

        /// <summary>
        /// Gets or sets the action name. 
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Gets or sets the player that is associated with the action.
        /// </summary>
        int Player { get; set; }

        /// <summary>
        /// Gets or sets the relative value.
        /// </summary>
        float RelativeValue { get; set; }

        /// <summary>
        /// Gets the type of event.
        /// </summary>
        EventType Type { get; }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <remarks><p>For button states a value on 0 could represent a released state. And a value of 1 could represent a pressed state.</p>
        /// <p>Button state values are completely dependent on how the binder implementation is designed to work. For example a play station 2 game pad has pressure sensitive buttons.</p></remarks>
        float Value { get; set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Updates the binder.
        /// </summary>
        /// <param name="totalGameTime">
        /// The total game time.
        /// </param>
        /// <param name="elapsedGameTime">
        /// The elapsed game time.
        /// </param>
        void Update(TimeSpan totalGameTime, TimeSpan elapsedGameTime);

        #endregion
    }
}