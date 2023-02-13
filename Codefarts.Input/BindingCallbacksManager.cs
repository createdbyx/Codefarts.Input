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
    using Models;


    /// <summary>
    /// Used to pair bindings with callbacks.
    /// </summary>
    public class BindingCallbacksManager : Dictionary<ActionKey, List<EventHandler<BindingData>>>
    {
        /// <summary>
        /// Holds a reference to a <see cref="InputManager"/>.
        /// </summary>
        private InputManager manager;

        /// <summary>
        /// Initializes a new instance of the <see cref="BindingCallbacksManager"/> class.
        /// </summary>
        /// <param name="manager">
        /// A reference to a action manager.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Raised if manager parameter is null.
        /// </exception>
        public BindingCallbacksManager(InputManager manager)
        {
            if (manager == null)
            {
                throw new ArgumentNullException(nameof(manager));
            }

            this.Manager = manager;
        }

        /// <summary>
        /// Gets or sets a reference to an action manager that is used to handle user input.
        /// </summary>
        public InputManager Manager
        {
            get
            {
                return this.manager;
            }

            private set
            {
                if (this.manager != null)
                {
                    this.manager.Action -= this.HandleActionEvent;
                }

                this.manager = value;
                if (this.manager != null)
                {
                    this.manager.Action += this.HandleActionEvent;
                }
            }
        }

        /// <summary>
        /// Binds a action name to a <see cref="EventHandler{TEventArgs}"/>.
        /// </summary>
        /// <param name="actionName">
        /// The name of the action.
        /// </param>
        /// <param name="player">
        /// Specifies a player index to associate the object with.
        /// </param>
        /// <param name="value">
        /// A reference to an <see cref="EventHandler{TEventArgs}"/>.
        /// </param>
        public void Bind(string actionName, int player, EventHandler<BindingData> value)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            List<EventHandler<BindingData>> list;
            var key = new ActionKey { ActionName = actionName, Player = player };

            if (this.ContainsKey(key))
            {
                list = this[key];
            }
            else
            {
                list = new List<EventHandler<BindingData>>();
                this.Add(key, list);
            }

            // TODO: Check if value already added ?
            list.Add(value);
        }

        /// <summary>
        /// Unbinds a action name from a <see cref="EventHandler{TEventArgs}"/>.
        /// </summary>
        /// <param name="actionName">
        /// The name of the action.
        /// </param>
        /// <param name="player">
        /// Specifies a player index to associate the object with.
        /// </param>
        /// <param name="value">
        /// A reference to an <see cref="EventHandler{TEventArgs}"/> type.
        /// </param>
        /// <exception cref="ArgumentNullException">If <see cref="value"/> is null. (Nothing in Visual Basic)</exception>
        public void Unbind(string actionName, int player, EventHandler<BindingData> value)
        {
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }

            var key = new ActionKey { ActionName = actionName, Player = player };

            if (!this.ContainsKey(key))
            {
                return;
            }

            this[key].Remove(value);
        }

        /// <summary>
        /// Unbinds all objects from a action name.
        /// </summary>
        /// <param name="actionName">
        /// The name of the action name to compare to.
        /// </param>
        public void UnbindAll(string actionName)
        {
            foreach (var pair in this)
            {
                if (pair.Key.ActionName == actionName)
                {
                    pair.Value.Clear();
                    return;
                }
            }
        }

        /// <summary>
        /// Unbinds all objects that match an player.
        /// </summary>
        /// <param name="player">
        /// The player to compare to.
        /// </param>
        public void UnbindAll(int player)
        {
            foreach (var pair in this)
            {
                if (pair.Key.Player == player)
                {
                    pair.Value.Clear();
                    return;
                }
            }
        }

        /// <summary>
        /// Unbinds all objects that match an player and action name.
        /// </summary>
        /// <param name="actionName">
        /// The name of the action name to compare to.
        /// </param>
        /// <param name="player">
        /// The player to compare to.
        /// </param>
        public void UnbindAll(string actionName, int player)
        {
            foreach (var pair in this)
            {
                if (pair.Key.ActionName == actionName & pair.Key.Player == player)
                {
                    pair.Value.Clear();
                    return;
                }
            }
        }

        /// <summary>
        /// Unbinds the specified object if it is associated with any actions.
        /// </summary>
        /// <param name="boundObject">
        /// A reference to a previously bound object.
        /// </param>
        /// <remarks>
        /// If the specified object is the last remaining object to be bound to an action the action will also be removed.
        /// </remarks>
        public void UnbindAll(EventHandler<BindingData> boundObject)
        {
            if (boundObject == null)
            {
                throw new ArgumentNullException(nameof(boundObject));
            }

            var keys = new ActionKey[this.Keys.Count];
            this.Keys.CopyTo(keys, 0);
            foreach (var key in keys)
            {
                var list = this[key];
                list.RemoveAll(e => e == boundObject);
                if (list.Count == 0)
                {
                    this.Remove(key);
                }
            }
        }

        /// <summary>
        /// Handles actions that are raised by the <see cref="InputManager"/>.
        /// </summary>
        /// <param name="sender">
        /// Typically a reference to the <see cref="InputManager"/> that raised the event.
        /// </param>
        /// <param name="e">
        /// Contains data related to the action event.
        /// </param>
        private void HandleActionEvent(object sender, BindingData e)
        {
            var key = new ActionKey(e.Name, e.Player);
            if (!this.ContainsKey(key))
            {
                return;
            }

            var list = this[key];
            foreach (var handler in list)
            {
                handler?.Invoke(this, e);
            }
        }
    }
}