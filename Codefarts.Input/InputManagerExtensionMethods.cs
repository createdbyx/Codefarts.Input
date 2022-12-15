using Codefarts.Input.Interfaces;

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
    /// <param name="device">The inputSource name.</param>
    /// <param name="source">The source name on the inputSource.</param>
    /// <param name="player">The player id associated with the binding.</param>
    /// <remarks>
    /// The name parameter is case sensitive.
    /// </remarks>
    public static void Bind(this InputManager input, string name, IInputSource device, string source, int player)
    {
        input.Bind(name, device, source, player);
    }
}