using System;

namespace Практ_10
{
    internal class Accountant
    {
        User user;

        public Accountant(User user)
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
            Console.WriteLine($"Добро пожаловать, {user.login}! Вы - бухгалтер.");
            Console.WriteLine();
            Console.WriteLine("Эта роль не реализована.");
        }
    }
}
