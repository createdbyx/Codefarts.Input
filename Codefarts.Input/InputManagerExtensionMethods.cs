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
    public static void Bind(this InputManager input, string name, string device, string source)
    {
        input.Bind(name, device, source,  0);
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
    public static void Bind(this InputManager input, string name, string device, string source, int player)
    {
        input.Bind(name, device, source,  player);
    }

     /*  /// <summary>
    /// Binds the specified name to a inputSource and inputSource source.
    /// </summary>
    /// <param name="name">The name of the binder.</param>
    /// <param name="device">The inputSource name.</param>
    /// <param name="source">The source name on the inputSource.</param>
    /// <param name="pressState">The pressed state if the inputSource source is a button.</param>
    /// <remarks>
    /// The name parameter is case sensitive.
    /// </remarks>
    public static void Bind(this InputManager input, string name, string device, string source, PressedState pressState)
    {
        input.Bind(name, device, source, pressState, 0);
    }
   */

    /// <summary>
    /// Gets the value of a binding.
    /// </summary>
    /// <param name="name">The binding name.</param>
    /// <param name="value">Will contain the value of the binding.</param>
    /// <returns>Returns true if the binding was found.</returns>
    /// <remarks>
    /// Keep in mind that the value may not represent the actual state of the hardware.
    /// The name parameter is case sensitive.
    /// </remarks>   
    public static bool GetValue(this InputManager input, string name, out float value)
    {
        return input.GetValue(name, false, out value);
    }
}