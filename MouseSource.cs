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
    /// <summary>
    /// Provides a enumeration for specifying the source mouse input.
    /// </summary>
    public enum MouseSource
    {
        /// <summary>
        /// The X value of the mouse position .
        /// </summary>
        X, 

        /// <summary>
        /// The X value of the mouse position .
        /// </summary>
        MouseX = X, 

        /// <summary>
        /// The Y values of the mouse position.
        /// </summary>
        Y, 

        /// <summary>
        /// The Y values of the mouse position.
        /// </summary>
        MouseY = Y, 

        /// <summary>
        /// The value of the mouse wheel.
        /// </summary>
        Wheel, 

        /// <summary>
        /// The left mouse button.
        /// </summary>
        LeftButton, 

        /// <summary>
        /// The right mouse button.
        /// </summary>
        RightButton, 

        /// <summary>
        /// The middle mouse button.
        /// </summary>
        MiddleButton, 

        /// <summary>
        /// The left mouse button.
        /// </summary>
        Left = LeftButton, 

        /// <summary>
        /// The right mouse button.
        /// </summary>
        Right = RightButton, 

        /// <summary>
        /// The middle mouse button.
        /// </summary>
        Middle = MiddleButton, 

        /// <summary>
        /// The XButton1 mouse button.
        /// </summary>
        XButton1, 

        /// <summary>
        /// The XButton1 mouse button.
        /// </summary>
        ButtonX1 = XButton1, 

        /// <summary>
        /// The XButton1 mouse button.
        /// </summary>
        X1Button = XButton1, 

        /// <summary>
        /// The XButton2 mouse button.
        /// </summary>
        XButton2, 

        /// <summary>
        /// The XButton2 mouse button.
        /// </summary>
        ButtonX2 = XButton2, 

        /// <summary>
        /// The XButton2 mouse button.
        /// </summary>
        X2Button = XButton2   
    }
}