// See https://aka.ms/new-console-template for more information

using System.Diagnostics;
using System.Drawing;
using System.Numerics;
using System.Runtime.InteropServices;
using Codefarts.Input;
using Codefarts.Input.ConsoleInputSource;
using Codefarts.Input.Models;

class InputManagerMovement
{
    Point pos = new Point(10, 10);
    Point prevPos = new Point(10, 10);
    bool isRunning;

    public void Run()
    {
        this.isRunning = true;
        Console.CursorVisible = false;

        var manager = new InputManager();
        var inputSource = new ConsoleInputSource();

        manager.AddDevice(inputSource);
        manager.Bind("MoveUp", "Console", "W");
        manager.Bind("MoveDown", "Console", "S");
        manager.Bind("MoveLeft", "Console", "A");
        manager.Bind("MoveRight", "Console", "D");
        manager.Bind("Quit", "Console", "Q");
        manager.Action += this.ManagerOnAction;

        // var cb=new BindingCallbacksManager(manager);
        // cb.Bind("Quit", (s,e)=>{});
        
        while (isRunning)
        {
            Console.SetCursorPosition(0, 0);
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Use WSAD keys to move star or Q to quit. Position: " + pos.ToString());
            Console.SetCursorPosition(pos.X, pos.Y);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("*");

            manager.Update();
        }
    }

    private void ManagerOnAction(object? sender, BindingData e)
    {
        this.prevPos = this.pos;
        switch (e.Name)              
        {
            case "MoveUp":
                pos.Y = Math.Max(0, pos.Y - 1);
                break;

            case "MoveDown":
                pos.Y = Math.Min(16, pos.Y + 1);
                break;

            case "MoveLeft":
                pos.X = Math.Max(0, pos.X - 1);
                break;

            case "MoveRight":
                pos.X = Math.Min(50, pos.X + 1);
                break;

            case "Quit":
                isRunning = false;
                break;
        }

        Console.SetCursorPosition(prevPos.X, prevPos.Y);
        Console.Write("  ");
    }
}

class Program
{
    public static void Main(string[] args)
    {
        var movement = new InputManagerMovement();
        movement.Run();
        //StandardInputExample();
    }

    private static void StandardInputExample()
    {
        var pos = new Point(10, 10);
        var prevPos = pos;
        Console.CursorVisible = false;
        var isRunning = true;

        while (isRunning)
        {
            Console.SetCursorPosition(0, 0);
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Use WSAD keys to move star or Q to quit. Position: " + pos.ToString());
            Console.SetCursorPosition(pos.X, pos.Y);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("*");


            if (Console.KeyAvailable)
            {
                prevPos = pos;
                var key = Console.ReadKey();
                switch (key.Key)
                {
                    case ConsoleKey.W:
                        pos.Y = Math.Max(0, pos.Y - 1);
                        break;

                    case ConsoleKey.S:
                        pos.Y = Math.Min(16, pos.Y + 1);
                        break;

                    case ConsoleKey.A:
                        pos.X = Math.Max(0, pos.X - 1);
                        break;

                    case ConsoleKey.D:
                        pos.X = Math.Min(50, pos.X + 1);
                        break;

                    case ConsoleKey.Q:
                        isRunning = false;
                        break;
                }

                Console.SetCursorPosition(prevPos.X, prevPos.Y);
                Console.Write("  ");
            }
        }
    }
}