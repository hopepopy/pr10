using System;

namespace Практ_10
{
    internal class Hr
    {
        User user;

        public Hr(User user)
        {
            this.user = user;
            ShowMenu();
        }

        public void Start()
        {
            ConsoleKeyInfo key = Console.ReadKey(true);
            while (key.Key != (ConsoleKey)Keys.Exit)
            {
                key = Console.ReadKey(true);
            }
        }

        private void ShowMenu()
        {
            Console.Clear();
            Console.WriteLine($"Добро пожаловать, {user.login}! Вы - HR.");
            Console.WriteLine();
            Console.WriteLine("Эта роль не реализована.");
        }
    }
}
