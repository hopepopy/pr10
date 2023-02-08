namespace Практ_10
{
    public class User
    {
        public int id;
        public string login;
        public string password;
        public Roles role;

        public User(int id, string login, string password, Roles role)
        {
            this.id = id;
            this.login = login;
            this.password = password;
            this.role = role;
        }
    }
}
