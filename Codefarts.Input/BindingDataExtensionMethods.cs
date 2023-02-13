using Codefarts.Input.Models;

namespace Codefarts.Input;

public static class BindingDataExtensionMethods
{
    public static bool ButtonReleased(this BindingData data)
    {
        return data.PreviousValue == 1 && data.Value == 0;
    }

    public static bool ButtonPressed(this BindingData data)
    {
        return data.PreviousValue == 0 && data.Value == 1;
    }
}