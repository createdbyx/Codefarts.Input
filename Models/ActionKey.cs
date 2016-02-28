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
    /// Provides a key for the <see cref="BindingCallbacksManager"/>.
    /// </summary>
    public struct ActionKey
    {
        #region Constants and Fields

        /// <summary>
        /// Gets or sets the name of the action.
        /// </summary>
        public string ActionName;

        /// <summary>
        /// Gets or sets the player index.
        /// </summary>
        public int Player;

        #endregion
    }
}