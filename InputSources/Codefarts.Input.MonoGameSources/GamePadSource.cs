using Codefarts.Input.Interfaces;
using Codefarts.Input.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Codefarts.Input.MonoGameSources;

public class GamePadSource : IInputSource
{
    private string name;
    private PollingData[] results = new PollingData [22];

    private PollingData buttonXData;
    private PollingData buttonYData;
    private PollingData buttonAData;
    private PollingData buttonBData;
    private PollingData buttonBigData;
    private PollingData buttonBackData;
    private PollingData buttonStartData;
    private PollingData buttonLeftSholderData;
    private PollingData buttonRIghtShoulderData;
    private PollingData buttonLeftStickData;
    private PollingData buttonRightStickData;

    private PollingData leftTriggerData;
    private PollingData rightTriggerData;

    private PollingData leftThumbStickXData;
    private PollingData leftThumbStickYData;
    private PollingData rightThumbStickXData;
    private PollingData rightThumbStickYData;

    private PollingData isConnectedData;

    private PollingData dpadRight;
    private PollingData dpadLeft;
    private PollingData dpadUp;
    private PollingData dpadDown;


    public GamePadSource()
    {
        this.name = $"GamePad";
        this.InitPollingData();
    }

    public GamePadSource(PlayerIndex playerIndex)
    {
        this.PlayerIndex = playerIndex;
        this.name = $"GamePad - {this.PlayerIndex}";
        this.InitPollingData();
    }

    public PlayerIndex PlayerIndex { get; } = PlayerIndex.One;

    public string Name
    {
        get
        {
            return this.name;
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
        GamePadState state = GamePad.GetState(this.PlayerIndex);

        // buttons
        this.buttonXData.Value = state.Buttons.X == ButtonState.Pressed ? 1 : 0;
        this.buttonYData.Value = state.Buttons.Y == ButtonState.Pressed ? 1 : 0;
        this.buttonAData.Value = state.Buttons.A == ButtonState.Pressed ? 1 : 0;
        this.buttonBData.Value = state.Buttons.B == ButtonState.Pressed ? 1 : 0;
        this.buttonBigData.Value = state.Buttons.BigButton == ButtonState.Pressed ? 1 : 0;
        this.buttonBackData.Value = state.Buttons.Back == ButtonState.Pressed ? 1 : 0;
        this.buttonStartData.Value = state.Buttons.Start == ButtonState.Pressed ? 1 : 0;
        this.buttonLeftSholderData.Value = state.Buttons.LeftShoulder == ButtonState.Pressed ? 1 : 0;
        this.buttonRIghtShoulderData.Value = state.Buttons.RightShoulder == ButtonState.Pressed ? 1 : 0;
        this.buttonLeftStickData.Value = state.Buttons.LeftStick == ButtonState.Pressed ? 1 : 0;
        this.buttonRightStickData.Value = state.Buttons.RightStick == ButtonState.Pressed ? 1 : 0;

        this.leftTriggerData.Value = state.Triggers.Left;
        this.rightTriggerData.Value = state.Triggers.Right;

        this.leftThumbStickXData.Value = state.ThumbSticks.Left.X;
        this.leftThumbStickYData.Value = state.ThumbSticks.Left.Y;
        this.rightThumbStickXData.Value = state.ThumbSticks.Right.X;
        this.rightThumbStickYData.Value = state.ThumbSticks.Right.Y;

        this.isConnectedData.Value = state.IsConnected ? 1 : 0;

        this.dpadRight.Value = state.DPad.Right == ButtonState.Pressed ? 1 : 0;
        this.dpadLeft.Value = state.DPad.Left == ButtonState.Pressed ? 1 : 0;
        this.dpadUp.Value = state.DPad.Up == ButtonState.Pressed ? 1 : 0;
        this.dpadDown.Value = state.DPad.Down == ButtonState.Pressed ? 1 : 0;

        return this.results;
    }

    private void InitPollingData()
    {
        // build polling data
        this.buttonXData = new PollingData(this.name, "X", 0, DataType.Button);
        this.buttonYData = new PollingData(this.name, "Y", 0, DataType.Button);
        this.buttonAData = new PollingData(this.name, "A", 0, DataType.Button);
        this.buttonBData = new PollingData(this.name, "B", 0, DataType.Button);
        this.buttonBigData = new PollingData(this.name, "BigButton", 0, DataType.Button);
        this.buttonBackData = new PollingData(this.name, "Back", 0, DataType.Button);
        this.buttonStartData = new PollingData(this.name, "Start", 0, DataType.Button);
        this.buttonLeftSholderData = new PollingData(this.name, "LeftShoulder", 0, DataType.Button);
        this.buttonRIghtShoulderData = new PollingData(this.name, "RightShoulder", 0, DataType.Button);
        this.buttonLeftStickData = new PollingData(this.name, "LeftStick", 0, DataType.Button);
        this.buttonRightStickData = new PollingData(this.name, "RightStick", 0, DataType.Button);

        this.leftTriggerData = new PollingData(this.name, "LeftTrigger", 0, DataType.Value);
        this.rightTriggerData = new PollingData(this.name, "RightTrigger", 0, DataType.Value);

        this.leftThumbStickXData = new PollingData(this.name, "LeftThumbStickX", 0, DataType.Value);
        this.leftThumbStickYData = new PollingData(this.name, "LeftThumbStickY", 0, DataType.Value);
        this.rightThumbStickXData = new PollingData(this.name, "RightThumbStickX", 0, DataType.Value);
        this.rightThumbStickYData = new PollingData(this.name, "RightThumbStickY", 0, DataType.Value);

        this.isConnectedData = new PollingData(this.name, "IsConnected", 0, DataType.Other);

        this.dpadRight = new PollingData(this.name, "Right", 0, DataType.Button);
        this.dpadLeft = new PollingData(this.name, "Left", 0, DataType.Button);
        this.dpadUp = new PollingData(this.name, "Up", 0, DataType.Button);
        this.dpadDown = new PollingData(this.name, "Down", 0, DataType.Button);


        // store results cache
        this.results[0] = this.buttonXData;
        this.results[1] = this.buttonYData;
        this.results[2] = this.buttonAData;
        this.results[3] = this.buttonBData;
        this.results[4] = this.buttonBigData;
        this.results[5] = this.buttonBackData;
        this.results[6] = this.buttonStartData;
        this.results[7] = this.buttonLeftSholderData;
        this.results[8] = this.buttonRIghtShoulderData;
        this.results[9] = this.buttonLeftStickData;
        this.results[10] = this.buttonRightStickData;

        this.results[11] = this.leftTriggerData;
        this.results[12] = this.rightTriggerData;

        this.results[13] = this.leftThumbStickXData;
        this.results[14] = this.leftThumbStickYData;
        this.results[15] = this.rightThumbStickXData;
        this.results[16] = this.rightThumbStickYData;

        this.results[17] = this.isConnectedData;

        this.results[18] = this.dpadRight;
        this.results[19] = this.dpadLeft;
        this.results[20] = this.dpadUp;
        this.results[21] = this.dpadDown;
    }
}