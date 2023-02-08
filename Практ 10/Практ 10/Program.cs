namespace Практ_10
{
    class Program
    {
        public static void Main(string[] args)
        {
            Console.CursorVisible = false;

            while (true)
            {
                User? user = Authorization.AuthUser();
                if (user == null)
                {
                    break;
                }

                switch (user.role)
                {
                    case Roles.Admin:
                        Admin admin = new Admin(user);
                        admin.Start();
                        break;
                    case Roles.Hr:
                        Hr hr = new Hr(user);
                        hr.Start();
                        break;
                    case Roles.Manager:
                        Manager manager = new Manager(user);
                        manager.Start();
                        break;
                    case Roles.Cashier:
                        Cashier cashier = new Cashier(user);
                        cashier.Start();
                        break;
                    case Roles.Accountant:
                        Accountant accountant = new Accountant(user);
                        accountant.Start();
                        break;
                }
            }
        }
    }
}