using Codefarts.Input.Interfaces;
using Codefarts.Input.Models;

namespace Codefarts.Input;

public static class InputManagerExtensionMethods
{
    /// <summary>
    /// Binds the specified name to a inputSource and inputSource source.
    /// </summary>
    /// <param name="name">The name of the binder.</param>
    /// <param name="device">The inputSource name.</param>
    /// <param name="source">The source name on the inputSource.</param>
    /// <remarks>
    /// The name parameter is case sensitive.
    /// </remarks>
    public static void Bind(this InputManager input, string name, IInputSource device, string source)
    {
        input.Bind(name, device, source, 0);
    }

    /// <summary>
    /// Binds the specified name to a inputSource and inputSource source.
    /// </summary>
    /// <param name="name">The name of the binder.</param>
    /// <param name="inputSource">The inputSource reference.</param>
    /// <param name="source">The source name on the inputSource.</param>
    /// <param name="pressState">The pressed state if the inputSource source is a button.</param>
    /// <param name="player">The player id associated with the binding.</param>
    /// <remarks>
    /// The name parameter is case sensitive.
    /// </remarks>
    public static void Bind(this InputManager input, string name, IInputSource inputSource, string source, int player)
    {
        if (inputSource == null)
        {
            throw new ArgumentNullException(nameof(inputSource));
        }

        var data = new BindingData(name, inputSource, source, player);
        input.Bindings.Add(data);
    }

    /// <summary>
    /// Unbinds a binding.
    /// </summary>
    /// <param name="name">The name of the binding to unbind. See remarks for more info.</param>
    /// <remarks>
    /// The name parameter is case sensitive.
    /// </remarks>
    public static void Unbind(this InputManager input, string name)
    {
        for (int i = input.Bindings.Count - 1; i >= 0; i--)
        {
            var bindingData = input.Bindings[i];
            if (bindingData.Name.Equals(name, StringComparison.InvariantCulture))
            {
                input.Bindings.RemoveAt(i);
            }
        }
    }
}