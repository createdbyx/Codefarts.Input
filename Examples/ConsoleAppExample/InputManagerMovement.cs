using System.Drawing;
using Codefarts.Input;
using Codefarts.Input.ConsoleInputSource;

class InputManagerMovement
{
    Point pos = new Point(10, 10);
    Point prevPos = new Point(10, 10);
    bool isRunning;

    public void Run()
    {
        this.isRunning = true;
        Console.CursorVisible = false;

        var inputManager = new InputManager();
        var inputSource = new ConsoleInputSource();

        inputManager.AddDevice(inputSource);
        inputManager.Bind("MoveUp", "Console", "W");
        inputManager.Bind("MoveDown", "Console", "S");
        inputManager.Bind("MoveLeft", "Console", "A");
        inputManager.Bind("MoveRight", "Console", "D");
        inputManager.Bind("Quit", "Console", "Q");

        var callbacksManager = new BindingCallbacksManager(inputManager);
        callbacksManager.Bind("MoveUp", (_, _) => this.MovePosition(new Point(0, -1)));
        callbacksManager.Bind("MoveDown", this.MoveDown);
        callbacksManager.Bind("MoveLeft", (_, _) => this.MovePosition(new Point(-1, 0)));
        callbacksManager.Bind("MoveRight", (_, _) => this.MovePosition(new Point(1, 0)));
        callbacksManager.Bind("Quit", (_, _) => this.isRunning = false);

        while (this.isRunning)
        {
            Console.SetCursorPosition(0, 0);
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Use WSAD keys to move star or Q to quit. Position: " + this.pos);
            Console.SetCursorPosition(this.pos.X, this.pos.Y);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("*");

            inputManager.Update();
        }
    }

    private void MovePosition(Point relative)
    {
        this.prevPos = this.pos;
        this.pos.X = Math.Min(50, Math.Max(0, this.pos.X + relative.X));
        this.pos.Y = Math.Min(16, Math.Max(0, this.pos.Y + relative.Y));

        if (this.prevPos != this.pos)
        {
            Console.SetCursorPosition(this.prevPos.X, this.prevPos.Y);
            Console.Write(" ");
        }
    }

    private void MoveDown()
    {
        this.MovePosition(new Point(0, 1));
    }
}