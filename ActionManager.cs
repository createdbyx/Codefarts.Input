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

#if UNITY_5
    using UnityEngine;
    using Codefarts.Core;         
    using Codefarts.Input.Code;
#endif

#if XNA
    using Microsoft.Xna.Framework;
#endif

    /// <summary>
    /// The action manager is used to handle user actions.
    /// </summary>
    public class ActionManager
    {
        /// <summary>
        /// Reference to a singleton for the <see cref="Instance"/> property.
        /// </summary>
        private static ActionManager singleton;

        /// <summary>
        /// Holds a collection of objects the implement the IBinder interface.
        /// </summary>
        private readonly BindingsCollection bindings;

        private static object lockObject = new object();

        /// <summary>
        /// Initializes a new instance of the <see cref="ActionManager"/> class. 
        /// </summary>
        public ActionManager()
        {
            this.bindings = new BindingsCollection(this);
            this.Devices = new DeviceCollection();
        }

        /// <summary>
        /// The Action event is raised every time a user action is detected.
        /// </summary>
        public event EventHandler<ActionArgs> Action;

        /// <summary>
        /// Gets singleton instance of the <see cref="ActionManager"/> type.
        /// </summary>
        /// <remarks>THe singleton is created the first time the property is accessed.</remarks>
        public static ActionManager Instance
        {
            get
            {
                if (singleton == null)
                {
                    lock (lockObject)
                    {
                        singleton = new ActionManager();
#if UNITY_5
                        // TODO: need setting here to specify the object name

                        // setup hidden game object that will be used for updating agent behaviors
                        var newGameObj = new GameObject("Codefarts.Input.InputManager");
                        newGameObj.transform.parent = CodefartsContainerObject.Instance.GameObject.transform;

                        // TODO: need setting here whether the object should be hidden in hierarchy
                        // newGameObj.hideFlags = HideFlags.HideAndDontSave | HideFlags.NotEditable;
                        var man = newGameObj.AddComponent<InputManagerMonoBehavior>();
                        man.ManagerReference = singleton;
#endif
                    }
                }

                return singleton;
            }
        }

        /// <summary>
        /// Gets or sets a list of registered devices.
        /// </summary>
        public DeviceCollection Devices { get; set; }

        /// <summary>
        /// Gets a reference to the collection of objects the implements the IBinder interface.
        /// </summary>
        public BindingsCollection Bindings
        {
            get { return this.bindings; }
        }

        /// <summary>
        /// Raises the action event.
        /// </summary>
        /// <param name="sender">The object sending the event.</param>
        /// <param name="e">A reference to an ActionArgs object containing information about the event.</param>
        public void RaiseActionEvent(object sender, ActionArgs e)
        {
            // only raise if the event has an invocation list.
            if (this.Action != null)
            {
                this.Action(this, e);
            }
        }

        /// <summary>
        /// Unbinds a action.
        /// </summary>
        /// <param name="name">The name of the action to unbind. See remarks for more info.</param>
        /// <remarks>
        /// The name parameter is case sensitive.
        /// </remarks>
        public void Unbind(string name)
        {
            this.Unbind(name, false, true);
        }

        /// <summary>
        /// Unbinds a action.
        /// </summary>
        /// <param name="name">The name of the action to unbind.</param>
        /// <param name="ignoreCase">If true the case sensitivity of the name parameter will be ignored.</param>
        public void Unbind(string name, bool ignoreCase)
        {
            this.Unbind(name, ignoreCase, true);
        }

        /// <summary>
        /// Unbinds a action.
        /// </summary>
        /// <param name="name">The name of the action to unbind.</param>
        /// <param name="ignoreCase">If true the case sensitivity of the name parameter will be ignored.</param>
        /// <param name="removeDuplicates">If true all duplicate bindings will be removed.</param>
        public void Unbind(string name, bool ignoreCase, bool removeDuplicates)
        {
            for (var i = 0; i < this.bindings.Count; i++)
            {
                if (this.bindings[i].Name != name && (!(ignoreCase & (this.bindings[i].Name.ToLower() == name.ToLower()))))
                {
                    continue;
                }

                this.bindings.RemoveAt(i);
                if (removeDuplicates == false)
                {
                    return;
                }
            }
        }

        /// <summary>
        /// Allows the ActionManager to update itself.
        /// </summary>
        /// <param name="totalGameTime">
        /// The total game time.
        /// </param>
        /// <param name="elapsedGameTime">
        /// The elapsed Game Time.
        /// </param>
        public virtual void Update(TimeSpan totalGameTime, TimeSpan elapsedGameTime)
        {
            // update each binding
            for (var i = 0; i < this.bindings.Count; i++)
            {
                var b = this.bindings[i];
                b.Update(totalGameTime, elapsedGameTime);
            }
        }

        /// <summary>
        /// Gets the value of a action binding.
        /// </summary>
        /// <param name="name">The action name.</param>
        /// <param name="value">Will contain the value of the action.</param>
        /// <returns>Returns true if the action was found.</returns>
        /// <remarks>Keep in mind that the value may not represent the actual state of the hardware. 
        /// Some binders may only update there state during a call to Update.</remarks>
        public bool GetValue(string name, out float value)
        {
            return this.GetValue(name, true, out value);
        }

        /// <summary>
        /// Gets the value of a action binding.
        /// </summary>
        /// <param name="name">The action name.</param>
        /// <param name="ignoreCase">Determines weather to ignore case sensitivity.</param>
        /// <param name="value">Will contain the value of the action.</param>
        /// <returns>Returns true if the action was found.</returns>
        /// <remarks>Keep in mind that the value may not represent the actual state of the hardware. 
        /// Some binders may only update there state during a call to Update.</remarks>
        public bool GetValue(string name, bool ignoreCase, out float value)
        {
            var list = new List<IBinder>();
            foreach (var i in this.bindings)
            {
                if (i.Name == name || (ignoreCase & (i.Name.ToLower() == name.ToLower())))
                {
                    list.Add(i);
                }
            }

            if (list.Count == 0)
            {
                value = default(float);
                return false;
            }

            var ret = list[0];
            value = ret.Value;
            return true;
        }

        /// <summary>
        /// Gets the values of action bindings.
        /// </summary>
        /// <param name="name">The action name.</param>
        /// <param name="value">Will contain the values of all actions with the given name.</param>
        /// <returns>Returns true if the action(s) were found.</returns>
        /// <remarks>Keep in mind that the value may not represent the actual state of the hardware. 
        /// Some binders may only update there state during a call to Update.</remarks>
        public bool GetValue(string name, out float[] value)
        {
            return this.GetValue(name, true, out value);
        }

        /// <summary>
        /// Gets the values of action bindings.
        /// </summary>
        /// <param name="name">The action name.</param>
        /// <param name="ignoreCase">Determines weather to ignore case sensitivity.</param>
        /// <param name="value">Will contain the values of all actions with the given name.</param>
        /// <returns>Returns true if the action(s) were found.</returns>
        /// <remarks>Keep in mind that the value may not represent the actual state of the hardware. 
        /// Some binders may only update there state during a call to Update.</remarks>
        public bool GetValue(string name, bool ignoreCase, out float[] value)
        {
            var list = new List<IBinder>();
            foreach (var i in this.bindings)
            {
                if (i.Name == name || (ignoreCase & (i.Name.ToLower() == name.ToLower())))
                {
                    list.Add(i);
                }
            }

            if (list.Count == 0)
            {
                value = new float[0];
                return false;
            }

            value = new float[list.Count];

            for (var i = 0; i < list.Count; i++)
            {
                var binder = list[i];
                value[i] = binder.Value;
            }

            return true;
        }

        /// <summary>
        /// Gets the relative value of a action binding.
        /// </summary>
        /// <param name="name">The action name.</param>
        /// <param name="value">Will contain the relative value of the action.</param>
        /// <returns>Returns true if the action was found.</returns>
        /// <remarks>Keep in mind that the value may not represent the actual state of the hardware. 
        /// Some binders may only update there state during a call to Update.</remarks>
        public bool GetRelativeValue(string name, out float value)
        {
            return this.GetRelativeValue(name, true, out value);
        }

        /// <summary>
        /// Gets the relative value of a action binding.
        /// </summary>
        /// <param name="name">The action name.</param>
        /// <param name="ignoreCase">Determines weather to ignore case sensitivity.</param>
        /// <param name="value">Will contain the relative value of the action.</param>
        /// <returns>Returns true if the action was found.</returns>
        /// <remarks>Keep in mind that the value may not represent the actual state of the hardware. 
        /// Some binders may only update there state during a call to Update. 
        /// Also if there are more then one action with the same name only the value of the first action that was found will be returned.</remarks>
        public bool GetRelativeValue(string name, bool ignoreCase, out float value)
        {
            var list = new List<IBinder>();
            foreach (var i in this.bindings)
            {
                if (i.Name == name || (ignoreCase & (i.Name.ToLower() == name.ToLower())))
                {
                    list.Add(i);
                }
            }

            if (list.Count == 0)
            {
                value = default(float);
                return false;
            }

            var ret = list[0];
            value = ret.RelativeValue;
            return true;
        }

        /// <summary>
        /// Gets the relative values of action bindings.
        /// </summary>
        /// <param name="name">The action name.</param>
        /// <param name="value">Will contain the relative values of all actions with the given name.</param>
        /// <returns>Returns true if the action(s) were found.</returns>
        /// <remarks>Keep in mind that the value may not represent the actual state of the hardware. 
        /// Some binders may only update there state during a call to Update.</remarks>
        public bool GetRelativeValue(string name, out float[] value)
        {
            return this.GetRelativeValue(name, true, out value);
        }

        /// <summary>
        /// Gets the relative values of action bindings.
        /// </summary>
        /// <param name="name">The action name.</param>
        /// <param name="ignoreCase">Determines weather to ignore case sensitivity.</param>
        /// <param name="value">Will contain the relative values of all actions with the given name.</param>
        /// <returns>Returns true if the action(s) were found.</returns>
        /// <remarks>Keep in mind that the value may not represent the actual state of the hardware. 
        /// Some binders may only update there state during a call to Update.</remarks>
        public bool GetRelativeValue(string name, bool ignoreCase, out float[] value)
        {
            var list = new List<IBinder>();
            foreach (var i in this.bindings)
            {
                if (i.Name == name || (ignoreCase & (i.Name.ToLower() == name.ToLower())))
                {
                    list.Add(i);
                }
            }

            if (list.Count == 0)
            {
                value = new float[0];
                return false;
            }

            value = new float[list.Count];

            for (var i = 0; i < list.Count; i++)
            {
                var binder = list[i];
                value[i] = binder.RelativeValue;
            }

            return true;
        }
    }
}
