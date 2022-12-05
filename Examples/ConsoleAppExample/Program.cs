using System.Drawing;

class Program
{
    public static void Main(string[] args)
    {
        while (true)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"1: {nameof(InputManagerMovement)}");
            Console.WriteLine($"2: {nameof(StandardInputMovement)}");
            Console.WriteLine($"3: quit");

            var key = Console.ReadKey();
            switch (key.Key)
            {
                case ConsoleKey.D1:
                    {
                        var movement = new InputManagerMovement();
                        movement.Run();
                    }
                    break;

                case ConsoleKey.D2:
                    {
                        var movement = new StandardInputMovement();
                        movement.Run();
                    }
                    break;

                case ConsoleKey.D3:
                    return;
            }
        }
    }
}