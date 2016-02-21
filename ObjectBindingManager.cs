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


    /// <summary>
    /// Used to pair actions with classes.
    /// </summary>
    public class ObjectBindingManager : Dictionary<ActionKey, List<EventHandler<BindingData>>>
    {
        #region Constants and Fields

        /// <summary>
        /// Holds a reference to the object binding manager singleton.
        /// </summary>
        private static ObjectBindingManager singleton;

        /// <summary>
        /// Holds a reference to a <see cref="InputManager"/>.
        /// </summary>
        private InputManager manager;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ObjectBindingManager"/> class.
        /// </summary>
        /// <param name="manager">
        /// A reference to a action manager.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Raised if manager parameter is null.
        /// </exception>
        public ObjectBindingManager(InputManager manager)
        {
            if (manager == null)
            {
                throw new ArgumentNullException("manager");
            }

            this.Manager = manager;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets a singleton instance of the <see cref="ObjectBindingManager"/>.
        /// </summary>
        /// <remarks>The singleton is created on the first call to this property.</remarks>
        public static ObjectBindingManager Instance
        {
            get
            {
                return singleton != null ? singleton : (singleton = new ObjectBindingManager(InputManager.Instance));
            }
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

            set
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

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Binds an array of action names to a <see cref="EventHandler{TEventArgs}"/>.
        /// </summary>
        /// <param name="actionNames">
        /// The array of action names to bind.
        /// </param>
        /// <param name="value">
        /// A reference to a <see cref="EventHandler{TEventArgs}"/> type.
        /// </param>
        public void Bind(string[] actionNames, EventHandler<BindingData> value)
        {
            foreach (var name in actionNames)
            {
                this.Bind(name, value);
            }
        }

        /// <summary>
        /// Binds an array of action names to a <see cref="EventHandler{TEventArgs}"/>.
        /// </summary>
        /// <param name="actionNames">
        /// The array of action names to bind.
        /// </param>
        /// <param name="player">
        /// The id of the player that will be associated with the action name.
        /// </param>
        /// <param name="value">
        /// A reference to a <see cref="EventHandler{TEventArgs}"/> type.
        /// </param>
        public void Bind(string[] actionNames, int player, EventHandler<BindingData> value)
        {
            foreach (var name in actionNames)
            {
                this.Bind(name, player, value);
            }
        }

        /// <summary>
        /// Binds an array of action names to a <see cref="EventHandler{TEventArgs}"/>.
        /// </summary>
        /// <param name="actionName">
        /// The action name to bind.
        /// </param>
        /// <param name="value">
        /// A reference to a <see cref="EventHandler{TEventArgs}"/> type.
        /// </param>
        public void Bind(string actionName, EventHandler<BindingData> value)
        {
            this.Bind(actionName, 0, value);
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
                throw new ArgumentNullException("value");
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

            list.Add(value);
        }

        /// <summary>
        /// Unbinds an array of action names from a <see cref="EventHandler{TEventArgs}"/>.
        /// </summary>
        /// <param name="actionNames">
        /// The array of action names to unbind.
        /// </param>
        /// <param name="value">
        /// A reference to a <see cref="EventHandler{TEventArgs}"/> type.
        /// </param>
        public void Unbind(string[] actionNames, EventHandler<BindingData> value)
        {
            foreach (var name in actionNames)
            {
                this.Unbind(name, value);
            }
        }

        /// <summary>
        /// Unbinds an array of action names from a <see cref="EventHandler{TEventArgs}"/>.
        /// </summary>
        /// <param name="actionNames">
        /// The array of action names to unbind.
        /// </param>
        /// <param name="player">
        /// The id of the player that will be associated with the action name.
        /// </param>
        /// <param name="value">
        /// A reference to a <see cref="EventHandler{TEventArgs}"/> type.
        /// </param>
        public void Unbind(string[] actionNames, int player, EventHandler<BindingData> value)
        {
            foreach (var name in actionNames)
            {
                this.Unbind(name, player, value);
            }
        }

        /// <summary>
        /// Unbinds an action name from a <see cref="EventHandler{TEventArgs}"/>.
        /// </summary>
        /// <param name="actionName">
        /// The array of action name to unbind.
        /// </param>
        /// <param name="value">
        /// A reference to a <see cref="EventHandler{TEventArgs}"/> type.
        /// </param>
        public void Unbind(string actionName, EventHandler<BindingData> value)
        {
            this.Unbind(actionName, 0, value);
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
        /// Unbinds all objects that match a <see cref="ActionKey"/>.
        /// </summary>
        /// <param name="key">
        /// The key.
        /// </param>
        public void UnbindAll(ActionKey key)
        {
            this[key].Clear();
        }

        /// <summary>
        /// Unbinds all objects that are bound to the action names.
        /// </summary>
        /// <param name="actionNames">
        /// The action Names.
        /// </param>
        public void UnbindAll(string[] actionNames)
        {
            foreach (var name in actionNames)
            {
                this.UnbindAll(name);
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
                throw new ArgumentNullException("boundObject");
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

        #endregion

        #region Methods

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
            var key = new ActionKey { ActionName = e.Name, Player = e.Player };
            if (!this.ContainsKey(key))
            {
                return;
            }

            var list = this[key];
            foreach (var handler in list)
            {
                handler.Invoke(this, e);
            }
        }

        #endregion
    }
}