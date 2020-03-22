using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using WPFProject_Lab4.Models;
using WPFProject_Lab4.Tools.Managers;

namespace WPFProject_Lab4.Tools.DataStorage
{
    internal class SerializedDataStorage : IDataStorage
    {
        private readonly List<User> _users;
        private readonly Random _random = new Random();

        internal SerializedDataStorage()
        {
            try
            {
                _users = SerializationManager.Deserialize<List<User>>(FileFolderHelper.StorageFilePath);
            }
            catch (FileNotFoundException)
            {
                _users = FillDatabase();
            }
        }

        public bool UserExists(string login)
        {
            return _users.Exists(u => u.Login == login);
        }

        public User GetUserByLogin(string login)
        {
            return _users.FirstOrDefault(u => u.Login == login);
        }

        public void AddUser(User user)
        {
            _users.Add(user);
            SaveChanges();
        }

        public void EditUser(User newUser)
        {
            var user = GetUserByLogin(newUser.Login);
            user.Name = newUser.Name;
            user.Surname = newUser.Surname;
            user.Email = newUser.Email;
            user.Birthday = newUser.Birthday;
            SaveChanges();
        }

        public void RemoveUser(User user)
        {
            _users.Remove(user);
            SaveChanges();
        }

        public List<User> UsersList
        {
            get { return _users.ToList(); }
        }

        private void SaveChanges()
        {
            SerializationManager.Serialize(_users, FileFolderHelper.StorageFilePath);
        }

        private List<User> FillDatabase()
        {
            var users = new List<User>();

            var random = new Random();
            var start = new DateTime(1940, 1, 1);
            int range = (DateTime.Today - start).Days;

            for (int i = 0; i < 50; i++)
            {
                string login = RandomAlphaNumericString(random.Next(6, 14));
                string password = RandomAlphaNumericString(random.Next(8, 16));
                string name = RandomName();
                string surname = RandomSurname();
                string email = $"{name}@gmail.com".ToLower();
                DateTime birthday = start.AddDays(random.Next(range)).Date;
                var user = new User(login, password, name, surname, email, birthday);
                users.Add(user);
            }

            return users.ToList();
        }

        private string RandomAlphaNumericString(int length)
        {
            const string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[_random.Next(s.Length)]).ToArray());
        }

        private string RandomName()
        {
            string[] arr =
            {
                "James", "John", "Robert", "Michael", "William", "David", "Richard", "Joseph", "Thomas", "Charles",
                "Christopher", "Daniel", "Matthew", "Anthony", "Donald", "Mark", "Paul", "Steven", "Andrew", "Kenneth",
                "Joshua", "George", "Kevin", "Brian", "Edward", "Ronald", "Timothy", "Jason", "Jeffrey", "Ryan",
                "Jacob", "Gary", "Nicholas", "Eric", "Stephen", "Jonathan", "Larry", "Justin", "Scott", "Brandon",
                "Frank", "Benjamin", "Gregory", "Samuel", "Raymond", "Patrick", "Alexander", "Jack", "Dennis", "Jerry",
                "Tyler", "Aaron", "Jose", "Henry", "Douglas", "Adam", "Peter", "Nathan", "Zachary", "Walter", "Kyle",
                "Harold", "Carl", "Jeremy", "Keith", "Roger", "Gerald", "Ethan", "Arthur", "Terry", "Christian", "Sean",
                "Lawrence", "Austin", "Joe", "Noah", "Jesse", "Albert", "Bryan", "Billy", "Bruce", "Willie", "Jordan",
                "Dylan", "Alan", "Ralph", "Gabriel", "Roy", "Juan", "Wayne", "Eugene", "Logan", "Randy", "Louis",
                "Russell", "Vincent", "Philip", "Bobby", "Johnny", "Bradley"
            };
            return arr[_random.Next(arr.Length)].ToLower();
        }

        private string RandomSurname()
        {
            string[] arr =
            {
                "Smith", "Johnson", "Williams", "Jones", "Brown", "Davis", "Miller", "Wilson", "Moore", "Taylor",
                "Anderson", "Thomas", "Jackson", "White", "Harris", "Martin", "Thompson", "Garcia", "Martinez",
                "Robinson", "Clark", "Rodriguez", "Lewis", "Lee", "Walker", "Hall", "Allen", "Young", "Hernandez",
                "King", "Wright", "Lopez", "Hill", "Scott", "Green", "Adams", "Baker", "Gonzalez", "Nelson", "Carter",
                "Mitchell", "Perez", "Roberts", "Turner", "Phillips", "Campbell", "Parker", "Evans", "Edwards",
                "Collins", "Stewart", "Sanchez", "Morris", "Rogers", "Reed", "Cook", "Morgan", "Bell", "Murphy",
                "Bailey", "Rivera", "Cooper", "Richardson", "Cox", "Howard", "Ward", "Torres", "Peterson", "Gray",
                "Ramirez", "James", "Watson", "Brooks", "Kelly", "Sanders", "Price", "Bennett", "Wood", "Barnes",
                "Ross", "Henderson", "Coleman", "Jenkins", "Perry", "Powell", "Long", "Patterson", "Hughes", "Flores",
                "Washington", "Butler", "Simmons", "Foster", "Gonzales", "Bryant ", "Alexander", "Russell", "Griffin ",
                "Diaz", "Hayes"
            };
            return arr[_random.Next(arr.Length)].ToLower();
        }
    }
}