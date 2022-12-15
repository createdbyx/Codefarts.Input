namespace Codefarts.Input.Models;

/// <summary>
/// The type of state weather or not it is a button or value.
/// </summary>
public enum DataType
{
    /// <summary>
    /// Button event.
    /// </summary>
    Button, 

    /// <summary>
    /// Value event.
    /// </summary>
    /// <remarks>This could be a joystick, slider or accelerometer etc.</remarks>
    Value, 

    /// <summary>
    /// Some other type of event.
    /// </summary>
    /// <remarks>For example the Nintendo Wii controller has LED lights that can be in an on or off state.</remarks>
    Other
}