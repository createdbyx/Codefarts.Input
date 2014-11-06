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
    using System;
    using System.Collections.Generic;

#if WINDOWS
    using Microsoft.Xna.Framework.Input;
#endif
#if SILVERLIGHT
    using Keys = System.Windows.Input.Key;
#endif
#if UNITY3D
    using Keys = UnityEngine.KeyCode;
#endif
#if PORTABLE
    using Keys = System.Int32;
#endif

    /// <summary>
    /// Provides a class for keyboard state information.
    /// </summary>
    internal class KeyboardStateData : Dictionary<Keys, bool>
    {
        #region Constants and Fields

        /// <summary>
        /// Holds the number of keys. 
        /// </summary>
        internal readonly int KeyCount;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="KeyboardStateData"/> class.
        /// </summary>
        public KeyboardStateData()
        {
            // GetValues is custom made because windows phone 7 has no Enum.GetValues method
            this.KeyItems = (Keys[])Enum.GetValues(typeof(Keys));
            this.KeyCount = this.KeyItems.Length;
            foreach (var k in this.KeyItems)
            {
                this.Add(k, false);
            }
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets a reference to an array of keys from the <see cref="Keys"/> type.
        /// </summary>
        public Keys[] KeyItems { get; internal set; }

        #endregion
    }
}