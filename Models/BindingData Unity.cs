#if UNITY_5 || PORTABLE
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
    /// Provides a class for binding information.
    /// </summary>
    public partial class BindingData
    {
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
    }
}
#endif