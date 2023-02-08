namespace Практ_10
{
    internal class Authorization
    {
        public static User? AuthUser()
        {
            ShowMenu();
            User? user = Authorize();
            return user;
        }

        private static void ShowMenu()
        {
            Console.Clear();
            Console.WriteLine("Для использования программы войдите в учетную запись.");
            Console.WriteLine("  Логин: ");
            Console.WriteLine("  Пароль: ");
            Console.WriteLine("  Авторизоваться");
        }

        private static User? Authorize()
        {
            string login = "";
            string password = "";

            Arrow arrow = new Arrow(1, 3);
            User? user = null;
            ConsoleKeyInfo key = Console.ReadKey(true);
            while (key.Key != (ConsoleKey)Keys.Exit)
            {
                switch (key.Key)
                {
                    case (ConsoleKey)Keys.Next:
                        arrow.Next();
                        break;
                    case (ConsoleKey)Keys.Back:
                        arrow.Back();
                        break;
                    case (ConsoleKey)Keys.Submit:
                        int i = arrow.GetIndex();
                        if (i == 0)
                        {
                            login = Input.Read(arrow.current, "Логин", false, login);
                        }
                        else if (i == 1)
                        {
                            password = Input.Read(arrow.current, "Пароль", true, password);
                        }
                        else
                        {
                            user = GetUser(login, password);

                            if (user == null)
                            {
                                Console.SetCursorPosition(0, 5);
                                Console.WriteLine("Неправильный логин или пароль");
                            }
                            else
                            {
                                return user;
                            }
                        }
                        break;
                }

                key = Console.ReadKey(true);
            }

            return user;
        }

        private static User? GetUser(string login, string password)
        {
            List<User>? users = Serializer.Load<List<User>>("users.json");
            if (users == null || users.Count == 0)
            {
                users = new List<User>();
                users.Add(new User(0, "admin", "admin", Roles.Admin));
                Serializer.Save(users, "users.json");
            }

            User? user = users.Find((user) => user.login == login && user.password == password);
            return user;
        }
    }
}
