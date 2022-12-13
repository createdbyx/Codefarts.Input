using Codefarts.Input.Interfaces;
using Codefarts.Input.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Codefarts.Input.MonoGameSources;

public class GamePadSource : IInputSource
{
    public GamePadSource()
    {
    }

    public GamePadSource(PlayerIndex playerIndex)
    {
        this.PlayerIndex = playerIndex;
    }

    public PlayerIndex PlayerIndex { get; set; } = PlayerIndex.One;

    public string Name
    {
        get
        {
            return $"GamePad - {this.PlayerIndex}";
        }
    }

    public string[] Sources
    {
        get
        {
            return new[]
            {
                "X",
                "Y",
                "A",
                "B",
                "BigButton",
                "Back",
                "Start",
                "LeftShoulder",
                "RightShoulder",
                "LeftStick",
                "RightStick",
                "LeftTrigger",
                "RightTrigger",
                "LeftThumbStickX",
                "LeftThumbStickY",
                "RightThumbStickX",
                "RightThumbStickY",
                "IsConnected",
                "Right",
                "Left",
                "Up",
                "Down"
            };
        }
    }

    public IEnumerable<PollingData> Poll()
    {
        var state = GamePad.GetState(this.PlayerIndex);

        var results = new List<PollingData>();

        // buttons
        results.Add(new PollingData(this.Name, "X", state.Buttons.X == ButtonState.Pressed ? 1 : 0));
        results.Add(new PollingData(this.Name, "Y", state.Buttons.Y == ButtonState.Pressed ? 1 : 0));
        results.Add(new PollingData(this.Name, "A", state.Buttons.A == ButtonState.Pressed ? 1 : 0));
        results.Add(new PollingData(this.Name, "B", state.Buttons.B == ButtonState.Pressed ? 1 : 0));
        results.Add(new PollingData(this.Name, "BigButton", state.Buttons.BigButton == ButtonState.Pressed ? 1 : 0));
        results.Add(new PollingData(this.Name, "Back", state.Buttons.Back == ButtonState.Pressed ? 1 : 0));
        results.Add(new PollingData(this.Name, "Start", state.Buttons.Start == ButtonState.Pressed ? 1 : 0));
        results.Add(new PollingData(this.Name, "LeftShoulder", state.Buttons.LeftShoulder == ButtonState.Pressed ? 1 : 0));
        results.Add(new PollingData(this.Name, "RightShoulder", state.Buttons.RightShoulder == ButtonState.Pressed ? 1 : 0));
        results.Add(new PollingData(this.Name, "LeftStick", state.Buttons.LeftStick == ButtonState.Pressed ? 1 : 0));
        results.Add(new PollingData(this.Name, "RightStick", state.Buttons.RightStick == ButtonState.Pressed ? 1 : 0));

        results.Add(new PollingData(this.Name, "LeftTrigger", state.Triggers.Left));
        results.Add(new PollingData(this.Name, "RightTrigger", state.Triggers.Right));

        results.Add(new PollingData(this.Name, "LeftThumbStickX", state.ThumbSticks.Left.X));
        results.Add(new PollingData(this.Name, "LeftThumbStickY", state.ThumbSticks.Left.Y));
        results.Add(new PollingData(this.Name, "RightThumbStickX", state.ThumbSticks.Right.X));
        results.Add(new PollingData(this.Name, "RightThumbStickY", state.ThumbSticks.Right.Y));

        results.Add(new PollingData(this.Name, "IsConnected", state.IsConnected ? 1 : 0));

        results.Add(new PollingData(this.Name, "Right", state.DPad.Right == ButtonState.Pressed ? 1 : 0));
        results.Add(new PollingData(this.Name, "Left", state.DPad.Left == ButtonState.Pressed ? 1 : 0));
        results.Add(new PollingData(this.Name, "Up", state.DPad.Up == ButtonState.Pressed ? 1 : 0));
        results.Add(new PollingData(this.Name, "Down", state.DPad.Down == ButtonState.Pressed ? 1 : 0));

        return results;
    }
}