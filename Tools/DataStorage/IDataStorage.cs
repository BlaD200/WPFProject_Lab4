using System.Collections.Generic;
using WPFProject_Lab4.Models;

namespace WPFProject_Lab4.Tools.DataStorage
{
    internal interface IDataStorage
    {
        bool UserExists(string login);

        User GetUserByLogin(string login);

        void AddUser(User user);

        void EditUser(User newUser);

        void RemoveUser(User user);

        List<User> UsersList { get; }
    }
}
