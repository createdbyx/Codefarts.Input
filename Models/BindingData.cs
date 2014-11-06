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
#if !UNITY3D && !PORTABLE
    using Microsoft.Xna.Framework.Content;
#endif

    /// <summary>
    /// Provides a class for binding information.
    /// </summary>
    public class BindingData
    {
        /// <summary>
        /// Gets or sets the action name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the device name or id.
        /// </summary>
        public string Device { get; set; }

        /// <summary>
        /// Gets or sets the source axis or button on the device.
        /// </summary>
        /// <remarks>This could also point to a device state such as a led light or gyroscope etc.</remarks>
        public string Source { get; set; }

#if !UNITY3D && !PORTABLE

        /// <summary>
        /// Gets or sets the player id.
        /// </summary>
        [ContentSerializer(Optional = true)]
        public int Player { get; set; }

        /// <summary>
        /// Gets or sets the state to compare against.
        /// </summary>
        /// <remarks>This is only used for non axis sources like buttons or led states.</remarks>
        [ContentSerializer(Optional = true)]
        public PressedState State { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the <see cref="ActionManager"/> should raise an action event on every update.
        /// </summary>
        [ContentSerializer(Optional = true)]
        public bool AlwaysRaise { get; set; }

#else

        /// <summary>
        /// Gets or sets the player id.
        /// </summary>
        public int Player { get; set; }
        
        /// <summary>
        /// Gets or sets the state to compare against.
        /// </summary>
        /// <remarks>This is only used for non axis sources like buttons or led states.</remarks>
        public PressedState State { get; set; }
      
        /// <summary>
        /// Gets or sets a value indicating whether the <see cref="ActionManager"/> should raise an action event on every update.
        /// </summary>
        public bool AlwaysRaise { get; set; }
#endif
    }
}