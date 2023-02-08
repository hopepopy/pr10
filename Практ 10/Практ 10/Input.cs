namespace Практ_10
{
    internal class Input
    {
        public static string Read(int lineNumber, string title, bool stars, string old)
        {
            Print(lineNumber, title, stars, old);

            string result = old;
            ConsoleKeyInfo key = Console.ReadKey(true);
            while (key.Key != (ConsoleKey)Keys.Submit)
            {
                if (key.Key == ConsoleKey.Backspace && result.Length > 0)
                {
                    result = result.Remove(result.Length - 1, 1);
                    Print(lineNumber, title, stars, result);
                }
                else if (key.Key != ConsoleKey.Backspace)
                {
                    result += key.KeyChar;
                    Print(lineNumber, title, stars, result);
                }

                key = Console.ReadKey(true);
            }

            return result;
        }

        private static void Print(int lineNumber, string title, bool stars, string value)
        {
            string printValue = "";
            if (stars)
            {
                for (int i = 0; i < value.Length; i++)
                {
                    printValue += "*";
                }
            }
            else
            {
                printValue = value;
            }

            Console.SetCursorPosition(0, lineNumber);
            Console.Write("                                                                               ");
            Console.SetCursorPosition(0, lineNumber);
            Console.WriteLine($"->{title}: {printValue}");
            Console.SetCursorPosition(value.Length, lineNumber);
        }
    }
}
