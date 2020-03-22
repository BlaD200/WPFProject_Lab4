using System.Collections.ObjectModel;
using WPFProject_Lab4.Models;
using WPFProject_Lab4.Tools;
using WPFProject_Lab4.Tools.Managers;

namespace WPFProject_Lab4.ViewModels
{
    internal class MainViewModel : BaseViewModel
    {
        private ObservableCollection<User> _users;
        public ObservableCollection<User> Users
        {
            get => _users;
            private set
            {
                _users = value;
                OnPropertyChanged();
            }
        }

        public string CurrentUser
        {
            get
            {
                return $"Current User {StationManager.CurrentUser}";
            }
        }

        public MainViewModel()
        {
            _users = new ObservableCollection<User>(StationManager.DataStorage.UsersList);
        }
        
    }
}
