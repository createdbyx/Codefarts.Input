using Codefarts.Input.Interfaces;
using Codefarts.Input.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Codefarts.Input.MonoGameSources;

public class GamePadSource : IInputSource
{
    private string name;
    private PollingData[] results = new PollingData [22];
    private List<PollingData> resultsToReturn = new List<PollingData>();

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

    public IEnumerable<PollingData> Poll(bool onlyStateChanges)
    {
        GamePadState state = GamePad.GetState(this.PlayerIndex);
                     
        this.resultsToReturn.Clear();

        ButtonCheck(onlyStateChanges, this.buttonXData, state.Buttons.X);
        ButtonCheck(onlyStateChanges, this.buttonYData, state.Buttons.Y);
        ButtonCheck(onlyStateChanges, this.buttonAData, state.Buttons.A);
        ButtonCheck(onlyStateChanges, this.buttonBData, state.Buttons.B);
        ButtonCheck(onlyStateChanges, this.buttonBigData, state.Buttons.BigButton);
        ButtonCheck(onlyStateChanges, this.buttonBackData, state.Buttons.Back);
        ButtonCheck(onlyStateChanges, this.buttonStartData, state.Buttons.Start);
        ButtonCheck(onlyStateChanges, this.buttonLeftSholderData, state.Buttons.LeftShoulder);
        ButtonCheck(onlyStateChanges, this.buttonRIghtShoulderData, state.Buttons.RightShoulder);
        ButtonCheck(onlyStateChanges, this.buttonLeftStickData, state.Buttons.LeftStick);
        ButtonCheck(onlyStateChanges, this.buttonRightStickData, state.Buttons.RightStick);

        ValueCheck(onlyStateChanges, this.leftTriggerData, state.Triggers.Left);
        ValueCheck(onlyStateChanges, this.rightTriggerData, state.Triggers.Right);

        ValueCheck(onlyStateChanges, this.leftThumbStickYData, state.ThumbSticks.Left.Y);
        ValueCheck(onlyStateChanges, this.rightThumbStickXData, state.ThumbSticks.Right.X);
        ValueCheck(onlyStateChanges, this.leftThumbStickXData, state.ThumbSticks.Left.X);
        ValueCheck(onlyStateChanges, this.rightThumbStickYData, state.ThumbSticks.Right.Y);

        ValueCheck(onlyStateChanges, this.isConnectedData, state.IsConnected ? 1 : 0);

        ButtonCheck(onlyStateChanges, this.dpadRight, state.DPad.Right);
        ButtonCheck(onlyStateChanges, this.dpadLeft, state.DPad.Left);
        ButtonCheck(onlyStateChanges, this.dpadUp, state.DPad.Up);
        ButtonCheck(onlyStateChanges, this.dpadDown, state.DPad.Down);

        return this.resultsToReturn;
    }

    private void ValueCheck(bool onlyStateChanges, PollingData pollingData, float value)
    {
        var oldValue = pollingData.Value;
        var newValue = value;
        if (!onlyStateChanges || (onlyStateChanges && oldValue != newValue))
        {
            pollingData.Value = newValue;
            this.resultsToReturn.Add(pollingData);
        }
    }

    private void ButtonCheck(bool onlyStateChanges, PollingData pollingData, ButtonState state)
    {
        var oldValue = pollingData.Value;
        var newValue = state == ButtonState.Pressed ? 1 : 0;
        if (!onlyStateChanges || (onlyStateChanges && oldValue != newValue))
        {
            pollingData.Value = newValue;
            this.resultsToReturn.Add(pollingData);
        }
    }

    private void InitPollingData()
    {
        // build polling data
        this.buttonXData = new PollingData(this.name, "X", 0, 0, 1, DataType.Button);
        this.buttonYData = new PollingData(this.name, "Y", 0, 0, 1, DataType.Button);
        this.buttonAData = new PollingData(this.name, "A", 0, 0, 1, DataType.Button);
        this.buttonBData = new PollingData(this.name, "B", 0, 0, 1, DataType.Button);
        this.buttonBigData = new PollingData(this.name, "BigButton", 0, 0, 1, DataType.Button);
        this.buttonBackData = new PollingData(this.name, "Back", 0, 0, 1, DataType.Button);
        this.buttonStartData = new PollingData(this.name, "Start", 0, 0, 1, DataType.Button);
        this.buttonLeftSholderData = new PollingData(this.name, "LeftShoulder", 0, 0, 1, DataType.Button);
        this.buttonRIghtShoulderData = new PollingData(this.name, "RightShoulder", 0, 0, 1, DataType.Button);
        this.buttonLeftStickData = new PollingData(this.name, "LeftStick", 0, 0, 1, DataType.Button);
        this.buttonRightStickData = new PollingData(this.name, "RightStick", 0, 0, 1, DataType.Button);

        this.leftTriggerData = new PollingData(this.name, "LeftTrigger", 0, -1, 1, DataType.Value);
        this.rightTriggerData = new PollingData(this.name, "RightTrigger", 0, -1, 1, DataType.Value);

        this.leftThumbStickXData = new PollingData(this.name, "LeftThumbStickX", 0, -1, 1, DataType.Value);
        this.leftThumbStickYData = new PollingData(this.name, "LeftThumbStickY", 0, -1, 1, DataType.Value);
        this.rightThumbStickXData = new PollingData(this.name, "RightThumbStickX", 0, -1, 1, DataType.Value);
        this.rightThumbStickYData = new PollingData(this.name, "RightThumbStickY", 0, -1, 1, DataType.Value);

        this.isConnectedData = new PollingData(this.name, "IsConnected", 0, 0, 1, DataType.Other);

        this.dpadRight = new PollingData(this.name, "Right", 0, 0, 1, DataType.Button);
        this.dpadLeft = new PollingData(this.name, "Left", 0, 0, 1, DataType.Button);
        this.dpadUp = new PollingData(this.name, "Up", 0, 0, 1, DataType.Button);
        this.dpadDown = new PollingData(this.name, "Down", 0, 0, 1, DataType.Button);


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