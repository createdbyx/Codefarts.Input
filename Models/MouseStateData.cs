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
    /// <summary>
    /// Provides a class for mouse state information.
    /// </summary>
    internal struct MouseStateData
    {
        #region Constants and Fields

        /// <summary>
        /// State of the left mouse button.
        /// </summary>
        public ButtonState LeftButton;

        /// <summary>
        /// State of the middle mouse button.
        /// </summary>
        public ButtonState MiddleButton;

        /// <summary>
        /// The relative scroll wheel position of the mouse.
        /// </summary>
        public float RelativeScrollWheelValue;

        /// <summary>
        /// State of the right mouse button.
        /// </summary>
        public ButtonState RightButton;

        /// <summary>
        /// The scroll wheel position of the mouse.
        /// </summary>
        public float ScrollWheelValue;

        /// <summary>
        /// The x position of the mouse.
        /// </summary>
        public float X;

        /// <summary>
        /// State of the X1 mouse button.
        /// </summary>
        public ButtonState XButton1;

        /// <summary>
        /// State of the X2 mouse button.
        /// </summary>
        public ButtonState XButton2;

        /// <summary>
        /// The y position of the mouse.
        /// </summary>
        public float Y;

        #endregion
    }
}