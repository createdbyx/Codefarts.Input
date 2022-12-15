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
        Console.Clear();
        var startTime = DateTime.Now;
        var lastUpdate = startTime;
        
        var inputManager = new InputManager();
        var inputSource = new ConsoleInputSource();

        inputManager.AddSource(inputSource);
        inputManager.Bind("MoveUp", inputSource, "W");
        inputManager.Bind("MoveDown", inputSource, "S");
        inputManager.Bind("MoveLeft", inputSource, "A");
        inputManager.Bind("MoveRight", inputSource, "D");
        inputManager.Bind("Quit", inputSource, "Q");

        var callbacksManager = new BindingCallbacksManager(inputManager);
        callbacksManager.Bind("MoveUp", (_, e) => this.MovePosition(new Point(0, -1)));
        callbacksManager.Bind("MoveDown", this.MoveDown);
        callbacksManager.Bind("MoveLeft", (_, e) => this.MovePosition(new Point(-1, 0)));
        callbacksManager.Bind("MoveRight", (_, e) => this.MovePosition(new Point(1, 0)));
        callbacksManager.Bind("Quit", (_, _) => this.isRunning = false);

        while (this.isRunning)
        {
            Console.SetCursorPosition(0, 0);
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Use WSAD keys to move star or Q to quit. Position: " + this.pos);
            Console.SetCursorPosition(this.pos.X, this.pos.Y);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("*");

            var currentTime = DateTime.Now;
            var totalTime = currentTime.Subtract(startTime);
            var elapsedTime =  currentTime.Subtract(lastUpdate);
            inputManager.Update(totalTime, elapsedTime);
            lastUpdate = currentTime;
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