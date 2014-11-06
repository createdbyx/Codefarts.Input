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

        #region Public Methods and Operators

        /// <summary>
        /// Creates a binder object.
        /// </summary>
        /// <param name="name">
        /// The name of the action.
        /// </param>
        /// <param name="player">
        /// The player id associated with the action name.
        /// </param>
        /// <param name="source">
        /// The source name or id on the device where data will be retrieved from.
        /// </param>
        /// <param name="alwaysRaise">
        /// If true every time the <see cref="ActionManager"/> updates the action will be fired regardless of it's state.
        /// </param>
        /// <param name="ignoreCase">
        /// If true the device name comparison will not be case sensitive.
        /// </param>
        /// <returns>
        /// Return a new <see cref="IBinder"/> implementation.
        /// </returns>
        IBinder CreateBinder(string name, int player, string source, bool alwaysRaise, bool ignoreCase);

        /// <summary>
        /// Creates a binder object.
        /// </summary>
        /// <param name="name">
        /// The name of the action.
        /// </param>
        /// <param name="player">
        /// The player id associated with the action name.
        /// </param>
        /// <param name="source">
        /// The source name or id on the device where data will be retrieved from.
        /// </param>
        /// <param name="state">
        /// The state of the button or other toggle state.
        /// </param>
        /// <param name="alwaysRaise">
        /// If true every time the <see cref="ActionManager"/> updates the action will be fired regardless of it's state.
        /// </param>
        /// <param name="ignoreCase">
        /// If true the device name comparison will not be case sensitive.
        /// </param>
        /// <returns>
        /// Return a new <see cref="IBinder"/> implementation.
        /// </returns>
        IBinder CreateBinder(string name, int player, string source, PressedState state, bool alwaysRaise, bool ignoreCase);

        #endregion
    }
}