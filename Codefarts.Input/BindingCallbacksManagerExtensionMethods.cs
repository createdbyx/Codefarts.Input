using Codefarts.Input.Models;

namespace Codefarts.Input;

public static class BindingCallbacksManagerExtensionMethods
{
    /// <summary>
    /// Binds an array of action names to a <see cref="EventHandler{TEventArgs}"/>.
    /// </summary>
    /// <param name="actionNames">
    /// The array of action names to bind.
    /// </param>
    /// <param name="value">
    /// A reference to a <see cref="EventHandler{TEventArgs}"/> type.
    /// </param>
    public static void Bind(this BindingCallbacksManager manager, string[] actionNames, EventHandler<BindingData> value)
    {
        foreach (var name in actionNames)
        {
            manager.Bind(name, value);
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
    public static void Bind(this BindingCallbacksManager manager, string[] actionNames, int player, EventHandler<BindingData> value)
    {
        foreach (var name in actionNames)
        {
            manager.Bind(name, player, value);
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
    public static void Bind(this BindingCallbacksManager manager, string actionName, EventHandler<BindingData> value)
    {
        manager.Bind(actionName, 0, value);
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
    public static void Bind(this BindingCallbacksManager manager, string actionName, Action value)
    {
        manager.Bind(actionName, 0, (_, _) => value());
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
    public static void Unbind(this BindingCallbacksManager manager, string[] actionNames, EventHandler<BindingData> value)
    {
        foreach (var name in actionNames)
        {
            manager.Unbind(name, value);
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
    public static void Unbind(this BindingCallbacksManager manager, string[] actionNames, int player, EventHandler<BindingData> value)
    {
        foreach (var name in actionNames)
        {
            manager.Unbind(name, player, value);
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
    public static void Unbind(this BindingCallbacksManager manager, string actionName, EventHandler<BindingData> value)
    {
        manager.Unbind(actionName, 0, value);
    }
}