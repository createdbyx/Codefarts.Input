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
    /// The type of event weather or not it is a button or value.
    /// </summary>
    public enum EventType
    {
        /// <summary>
        /// Button event.
        /// </summary>
        Button, 

        /// <summary>
        /// Value event.
        /// </summary>
        /// <remarks>This could be a joystick, slider or accelerometer etc.</remarks>
        Value, 

        /// <summary>
        /// Some other type of event.
        /// </summary>
        /// <remarks>For example the Nintendo Wii controller has LED lights that can be in an on or off state.</remarks>
        Other
    }
}