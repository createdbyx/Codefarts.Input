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

    using Codefarts.Input.Interfaces;
    using Codefarts.Input.Models;

    /// <summary>
    /// Provides a collection for <see cref="IBinder"/> implementations.
    /// </summary>
    public class BindingsCollection : List<IBinder>
    {
        #region Constants and Fields

        /// <summary>
        /// The action handler.
        /// </summary>
        private readonly EventHandler<ActionArgs> actionHandler;

        /// <summary>
        /// Holds a reference to a <see cref="ActionManager"/> type.
        /// </summary>
        private readonly ActionManager manager;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="BindingsCollection"/> class. 
        /// </summary>
        public BindingsCollection()
        {
            // setup a default action handler event
            this.actionHandler = (sender, e) =>
                {
                    if (this.manager != null)
                    {
                        this.manager.RaiseActionEvent(this.manager, e);
                    }
                };
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BindingsCollection"/> class. 
        /// </summary>
        /// <param name="manager">
        /// A reference to a <see cref="ActionManager"/> type.
        /// </param>
        internal BindingsCollection(ActionManager manager)
            : this()
        {
            if (manager == null)
            {
                throw new ArgumentNullException("manager");
            }

            this.manager = manager;
        }

        #endregion

        #region Public Indexers

        /// <summary>
        ///  Gets or sets the element at the specified index.
        /// </summary>
        /// <returns>
        ///  The element at the specified index.
        /// </returns>
        /// <param name="index">The zero-based index of the element to get or set.</param>
        /// <exception cref="T:System.ArgumentOutOfRangeException"><paramref name="index" /> is not a valid index in the <see cref="T:System.Collections.Generic.IList`1" />.
        ///  </exception>
        /// <exception cref="T:System.NotSupportedException">
        /// The property is set and the <see cref="T:System.Collections.Generic.IList`1" /> is read-only.
        /// </exception>
        public new IBinder this[int index]
        {
            get
            {
                return base[index];
            }

            set
            {
                base[index].Action -= this.actionHandler;
                base[index] = value;
                base[index].Action += this.actionHandler;
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Adds an item to the collection.
        /// </summary>
        /// <param name="item">
        /// The item to be added.
        /// </param>
        public new void Add(IBinder item)
        {
            item.Action += this.actionHandler;
            base.Add(item);
        }

        /// <summary>
        /// Adds a new binding enter to the collection.
        /// </summary>
        /// <param name="item">
        /// The <see cref="IBinder"/> implementation to use for binding information.
        /// </param>
        /// <param name="actionName">
        /// The name of the action.
        /// </param>
        public void Add(IBinder item, string actionName)
        {
            item.Name = actionName;
            this.Add(item);
        }

        /// <summary>
        /// Adds a new binding enter to the collection.
        /// </summary>
        /// <param name="item">
        /// The <see cref="IBinder"/> implementation to use for binding information.
        /// </param>
        /// <param name="actionName">
        /// The name of the action.
        /// </param>
        /// <param name="player">
        /// The player id associated with the action name.
        /// </param>
        public void Add(IBinder item, string actionName, int player)
        {
            item.Name = actionName;
            item.Player = player;
            this.Add(item);
        }

        /// <summary>
        /// Adds a new binding enter to the collection.
        /// </summary>
        /// <param name="items">
        /// The array of binding data items that will be added.
        /// </param>
        public void Add(BindingData[] items)
        {
            this.Add(items, true);
        }

        /// <summary>
        /// Adds a new binding enter to the collection.
        /// </summary>
        /// <param name="items">
        /// The array of binding data items that will be added.
        /// </param>
        /// <param name="ignoreCase">
        /// If true the device name comparison will not be case sensitive.
        /// </param>
        public void Add(BindingData[] items, bool ignoreCase)
        {
            foreach (BindingData item in items)
            {
                this.Add(item);
            }
        }

        /// <summary>
        /// Adds a new binding enter to the collection.
        /// </summary>
        /// <param name="item">
        /// The item containing the binding data.
        /// </param>
        public void Add(BindingData item)
        {
            this.Add(item, true);
        }

        /// <summary>
        /// Adds a new binding enter to the collection.
        /// </summary>
        /// <param name="item">
        /// The item containing the binding data.
        /// </param>
        /// <param name="ignoreCase">
        /// If true the device name comparison will not be case sensitive.
        /// </param>
        public void Add(BindingData item, bool ignoreCase)
        {
            this.Add(item.Name, item.Device, item.Source, item.State, item.Player, item.AlwaysRaise, ignoreCase);
        }

        /// <summary>
        /// Adds a new binding enter to the collection.
        /// </summary>
        /// <param name="actionName">
        /// The name of the action.
        /// </param>
        /// <param name="device">
        /// The name or id of the device that the action name is associated with.
        /// </param>
        /// <param name="source">
        /// The source name or id on the device where data will be retrieved from.
        /// </param>
        /// <param name="state">
        /// The state of the source if it is a button.
        /// </param>
        /// <param name="player">
        /// The player id associated with the action name.
        /// </param>
        /// <param name="alwaysRaise">
        /// If true every time the <see cref="ActionManager"/> updates the action will be fired regardless of it's state.
        /// </param>
        public void Add(string actionName, string device, string source, PressedState state, int player, bool alwaysRaise)
        {
            this.Add(actionName, device, source, state, player, alwaysRaise, true);
        }

        /// <summary>
        /// Adds a new binding enter to the collection.
        /// </summary>
        /// <param name="actionName">
        /// The name of the action.
        /// </param>
        /// <param name="device">
        /// The name or id of the device that the action name is associated with.
        /// </param>
        /// <param name="source">
        /// The source name or id on the device where data will be retrieved from.
        /// </param>
        /// <param name="state">
        /// The state of the source if it is a button.
        /// </param>
        /// <param name="player">
        /// The player id associated with the action name.
        /// </param>
        /// <param name="alwaysRaise">
        /// If true every time the <see cref="ActionManager"/> updates the action will be fired regardless of it's state.
        /// </param>
        /// <param name="ignoreCase">
        /// If true the device name comparison will not be case sensitive.
        /// </param>
        public void Add(string actionName, string device, string source, PressedState state, int player, bool alwaysRaise, bool ignoreCase)
        {
            if (this.manager == null)
            {
                return;
            }

            foreach (var dev in this.manager.Devices)
            {
                if (dev == null || (dev.Name != device && (!(ignoreCase & (dev.Name.ToLower() == device.ToLower())))))
                {
                    continue;
                }

                var b = dev.CreateBinder(actionName, player, source, state, alwaysRaise, ignoreCase);
                if (b == null)
                {
                    continue;
                }

                this.Add(b, actionName);
                return;
            }

            throw new Exception("No matching device or source was found.");
        }

        /// <summary>
        /// Removes all items from the collection.
        /// </summary>
        /// <exception cref="T:System.NotSupportedException">
        /// The collection is read-only. 
        /// </exception>
        public new void Clear()
        {
            foreach (var item in this)
            {
                item.Action -= this.actionHandler;
            }

            base.Clear();
        }

        /// <summary>
        /// Inserts an item to the <see cref="T:System.Collections.Generic.IList`1"/> at the specified index.
        /// </summary>
        /// <param name="index">
        /// The zero-based index at which <paramref name="item"/> should be inserted.
        /// </param>
        /// <param name="item">
        /// The object to insert into the <see cref="T:System.Collections.Generic.IList`1"/>.
        /// </param>
        /// <exception cref="T:System.ArgumentOutOfRangeException">
        /// <paramref name="index"/> is not a valid index in the <see cref="T:System.Collections.Generic.IList`1"/>.
        /// </exception>
        /// <exception cref="T:System.NotSupportedException">
        /// The <see cref="T:System.Collections.Generic.IList`1"/> is read-only.
        /// </exception>
        public new void Insert(int index, IBinder item)
        {
            base[index].Action += this.actionHandler;
            base.Insert(index, item);
        }

        /// <summary>
        /// Removes the first occurrence of a specific object from the collection.
        /// </summary>
        /// <returns>
        /// true if item was successfully removed from the collection; otherwise, false. This method also returns false if item is not found in the original <see cref="T:System.Collections.Generic.ICollection`1"/>.
        /// </returns>
        /// <param name="item">
        /// The object to remove from the collection.
        /// </param>
        /// <exception cref="T:System.NotSupportedException">
        /// The <see cref="T:System.Collections.Generic.ICollection`1"/> is read-only.
        /// </exception>
        public new bool Remove(IBinder item)
        {
            item.Action -= this.actionHandler;
            return base.Remove(item);
        }

        /// <summary>
        /// Removes a item from the collection.
        /// </summary>
        /// <param name="index">
        /// The index of the item to remove.
        /// </param>
        public new void RemoveAt(int index)
        {
            base[index].Action -= this.actionHandler;
            base.RemoveAt(index);
        }

        #endregion
    }
}