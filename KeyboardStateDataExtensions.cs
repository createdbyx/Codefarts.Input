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
    using System.Collections.Generic;

    using Codefarts.Input.Models;

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
    /// Provides extension methods for the <see cref="KeyboardStateData"/> type.
    /// </summary>
    internal static class KeyboardStateDataExtensions
    {
        #region Methods

        /// <summary>
        /// Copies state data from the <see cref="KeyboardStateData"/> to a dictionary.
        /// </summary>
        /// <param name="data">
        /// The source data that will be copied.
        /// </param>
        /// <param name="items">
        /// The destination dictionary.
        /// </param>
        internal static void CopyTo(this KeyboardStateData data, IDictionary<Keys, bool> items)
        {
            foreach (var key in data.Keys)
            {
                items[key] = data[key];
            }
        }

        /// <summary>
        /// Gets an array of keys that match a state.
        /// </summary>
        /// <param name="data">
        /// The source <see cref="KeyboardStateData"/>.
        /// </param>
        /// <param name="state">
        /// The state to match against.
        /// </param>
        /// <returns>
        /// Returns the array of matching <see cref="Keys"/> types.
        /// </returns>
        internal static Keys[] GetKeys(this KeyboardStateData data, bool state)
        {
            var items = new Keys[data.KeyCount];
            int index = 0;
            foreach (var k in data.Keys)
            {
                if (data[k] == state)
                {
                    items[index++] = k;
                }
            }

            Array.Resize(ref items, index);
            return items;
        }

        /// <summary>
        /// Updates the state data for the <see cref="KeyboardStateData"/> type.
        /// </summary>
        /// <param name="data">
        /// The destination for the state data.
        /// </param>
        /// <param name="keys">
        /// The array of pressed <see cref="Keys"/> types that will be copied.
        /// </param>
        internal static void Update(this KeyboardStateData data, Keys[] keys)
        {
            foreach (var key in data.KeyItems)
            {
                data[key] = Array.IndexOf(keys, key) != -1;
            }
        }

        #endregion
    }
}