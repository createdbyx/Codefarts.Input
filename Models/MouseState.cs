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
    internal struct MouseState
    {
        #region Constants and Fields

        /// <summary>
        /// State of the mouse buttons.
        /// </summary>
        public float[] Buttons;
        
        /// <summary>
        /// The scroll wheel position of the mouse.
        /// </summary>
        public float ScrollWheelValue;

        /// <summary>
        /// The x position of the mouse.
        /// </summary>
        public float X;

        /// <summary>
        /// The y position of the mouse.
        /// </summary>
        public float Y;

        #endregion
    }
}