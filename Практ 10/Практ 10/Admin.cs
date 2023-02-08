using System;
using System.Reflection;

namespace Практ_10
{
    public class Admin : ICrud
    {
        private List<User> allUsers;
        private List<User>? searchResult;
        private User user;

        private int searchField = -1;
        private string searchValue = "";

        private Arrow arrow;

        public Admin(User user)
        {
            this.user = user;
            allUsers = Serializer.Load<List<User>>("users.json");
            searchResult = null;
            arrow = new Arrow(2, allUsers.Count + 1);
            ShowMenu();
        }

        public void Start()
        {
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
                        int index = arrow.GetIndex();
                        Read(index);
                        SearchUsers();
                        ShowMenu();
                        break;
                    case (ConsoleKey)Keys.Create:
                        Create();
                        SearchUsers();
                        ShowMenu();
                        break;
                    case (ConsoleKey)Keys.Search:
                        if (searchResult == null)
                        {
                            Search();
                        }
                        else
                        {
                            searchField = -1;
                            searchValue = "";
                        }
                        SearchUsers();
                        ShowMenu();
                        break;
                }

                key = Console.ReadKey(true);
            }

            Console.Clear();
        }
        
        private void ShowMenu()
        {
            Console.Clear();
            Console.WriteLine($"Добро пожаловать, {user.login}! Вы - администратор. (F1 - создать, F - поиск/сбросить)");
            Console.WriteLine();
            List<User> users = searchResult == null ? allUsers : searchResult;
            foreach (var user in users)
            {
                Console.WriteLine($"  {user.id} | {user.login}, {RoleName(user.role)}");
            }
            arrow.UpdateMax(users.Count + 1);
            arrow.Print();
        }

        private void SearchUsers()
        {
            switch (searchField)
            {
                case 0:
                    searchResult = allUsers.FindAll((user) => user.id == Convert.ToInt32(searchValue));
                    break;
                case 1:
                    searchResult = allUsers.FindAll((user) => user.login.Contains(searchValue));
                    break;
                case 2:
                    searchResult = allUsers.FindAll((user) => (int) user.role == Convert.ToInt32(searchValue));
                    break;
                default:
                    searchResult = null;
                    break;
            }
        }

        public void Create()
        {
            Console.Clear();
            Console.WriteLine($"Добро пожаловать, {this.user.login}! Вы - администратор. (F3 - создать, Esc - назад)");
            Console.WriteLine();
            
            int max_id = allUsers.Max((user) => user.id);
            int id = max_id + 1;
            string login = "";
            string password = "";
            Roles role = 0;

            Console.WriteLine($"  id: {id}");
            Console.WriteLine($"  Логин:");
            Console.WriteLine($"  Пароль:");
            Console.WriteLine($"  Роль: {(int)role}");

            Arrow arrow = new Arrow(3, 5);
            ConsoleKeyInfo key = Console.ReadKey();
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
                            login = Input.Read(3, "Логин", false, login);
                        }
                        else if (i == 1)
                        {
                            password = Input.Read(4, "Пароль", false, password);
                        }
                        else
                        {
                            role = (Roles)Convert.ToInt32(Input.Read(5, "Роль", false, ((int)role).ToString()));
                        }
                        break;
                    case (ConsoleKey)Keys.Save:
                        User user = new User(id, login, password, role);
                        allUsers.Add(user);
                        Serializer.Save(allUsers, "users.json");
                        return;
                }

                key = Console.ReadKey();
            }
        }

        public void Delete(int index)
        {
            allUsers.RemoveAt(index);
            Serializer.Save(allUsers, "users.json");
        }

        public void Read(int index)
        {
            User user = allUsers[index];
            ShowUser(user);

            ConsoleKeyInfo key = Console.ReadKey();
            while (key.Key != (ConsoleKey)Keys.Exit)
            {
                switch (key.Key)
                {
                    case (ConsoleKey)Keys.Update:
                        Update(index);
                        ShowUser(user);
                        break;
                    case (ConsoleKey)Keys.Delete:
                        Delete(index);
                        return;
                }

                key = Console.ReadKey(true);
            }
        }

        private void ShowUser(User user)
        {
            Console.Clear();
            Console.WriteLine($"Добро пожаловать, {this.user.login}! Вы - администратор. (F2 - обновить, Del - удалить, Esc - назад)");
            Console.WriteLine();
            Console.WriteLine($"  id: {user.id}");
            Console.WriteLine($"  Логин: {user.login}");
            Console.WriteLine($"  Роль: {RoleName(user.role)}");
        }

        public void Update(int index)
        {
            User user = allUsers[index];
            Console.Clear();
            Console.WriteLine($"Добро пожаловать, {this.user.login}! Вы - администратор. (F3 - Сохранить, Esc - Назад)");
            Console.WriteLine();

            string login = user.login;
            string password = user.password;
            Roles role = user.role;

            Console.WriteLine($"  id: {user.id}");
            Console.WriteLine($"  Логин: {login}");
            Console.WriteLine($"  Пароль: {password}");
            Console.WriteLine($"  Роль: {(int)role}");
            
            Arrow arrow = new Arrow(3, 5);
            ConsoleKeyInfo key = Console.ReadKey();
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
                            login = Input.Read(3, "Логин", false, login);
                        }
                        else if (i == 1)
                        {
                            password = Input.Read(4, "Пароль", false, password);
                        }
                        else
                        {
                            role = (Roles)Convert.ToInt32(Input.Read(5, "Роль", false, ((int)role).ToString()));
                        }
                        break;
                    case (ConsoleKey)Keys.Save:
                        user.login = login;
                        user.password = password;
                        user.role = role;
                        Serializer.Save(allUsers, "users.json");
                        return;
                }

                key = Console.ReadKey(true);
            }
        }
        
        public void Search()
        {
            Console.CursorVisible = true;
            Console.Clear();
            Console.WriteLine($"Добро пожаловать, {this.user.login}! Вы - администратор. (F2 - обновить, Del - удалить, Esc - назад)");
            Console.WriteLine();
            Console.WriteLine("Поиск по:\n0 - id\n1 - логин\n2 - роль");
            Console.Write(": ");
            searchField = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Введите значение поиска");
            Console.Write(": ");
            string? value = Console.ReadLine();
            searchValue = value == null ? "" : value;
            Console.CursorVisible = false;
        }

        private string RoleName(Roles role)
        {
            switch (role)
            {
                case Roles.Admin:
                    return "Администратор";
                case Roles.Hr:
                    return "Менеджер персонала";
                case Roles.Manager:
                    return "Склад-менеджер";
                case Roles.Cashier:
                    return "Кассир";
                case Roles.Accountant:
                    return "Бухгалтер";
                default:
                    return "";
            }
        }
    }
}
