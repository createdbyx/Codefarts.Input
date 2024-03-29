﻿using System.Drawing;

public class StandardInputMovement
{
    public void Run()
    {
        var pos = new Point(10, 10);
        var prevPos = pos;
        Console.CursorVisible = false;
        Console.Clear();
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