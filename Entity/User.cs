namespace MDW.Entity
{
    public class User
    {
        public bool Enabled { get; set; }

        public string Name { get { return string.Format("{0} {1}", FirstName, LastName); } }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string Role { get; set; }

        public string Theme { get; set; }

        public string Gravatar { get; set; }
    }
}