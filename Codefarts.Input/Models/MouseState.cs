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

        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Object"/> class.
        /// </summary>
        public MouseState(float x, float y, float[] buttons, float scrollWheelValue)
        {
            this.Buttons = buttons;
            this.ScrollWheelValue = scrollWheelValue;
            this.X = x;
            this.Y = y;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Object"/> class.
        /// </summary>
        public MouseState(float x, float y)
            : this()
        {
            this.X = x;
            this.Y = y;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Object"/> class.
        /// </summary>
        public MouseState(float x, float y, float[] buttons)
            : this()
        {
            this.Buttons = buttons;
            this.X = x;
            this.Y = y;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Object"/> class.
        /// </summary>
        public MouseState(float[] buttons)
            : this()
        {
            this.Buttons = buttons;
        }
    }
}