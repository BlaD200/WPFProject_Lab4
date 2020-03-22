using System;

namespace WPFProject_Lab4.Models
{
    [Serializable]
    internal class User
    {
        private Guid _guid;
        private string _login;
        private string _password;

        public Guid Guid
        {
            get
            {
                return _guid;
            }
            private set
            {
                _guid = value;
            }
        }
        public string Login
        {
            get { return _login; }
            set { _login = value; }
        }

        public string Password
        {
            get { return _password; }
        }

        public User(string login, string password)
        {
            _guid = Guid.NewGuid();
            _login = login;
            SetPassword(password);
        }

        private void SetPassword(string password)
        {
            //TODO Add encription
            _password = password;
        }

        internal bool CheckPassword(string password)
        {
            //TODO Compare encrypted passwords
            return _password == password;
        }

        public override string ToString()
        {
            return $"{Login}";
        }
    }
}
