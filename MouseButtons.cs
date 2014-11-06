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

    /// <summary>
    /// Provides a list of mouse buttons.
    /// </summary>
    [Flags]
    public enum MouseButtons
    {
        /// <summary>
        /// No mouse buttons
        /// </summary>
        None = 0, 

        /// <summary>
        /// Refers to the left mouse button.
        /// </summary>
        Left = 1, 

        /// <summary>
        /// Refers to the right mouse button.
        /// </summary>
        Right = 2, 

        /// <summary>
        /// Refers to the middle mouse button.
        /// </summary>
        Middle = 4, 

        /// <summary>
        /// Refers to the XButton1 mouse button.
        /// </summary>
        XButton1 = 8, 

        /// <summary>
        /// Refers to the XButton2 mouse button.
        /// </summary>
        XButton2 = 16
    }
}