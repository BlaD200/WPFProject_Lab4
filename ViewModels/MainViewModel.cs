using System.Collections.ObjectModel;
using WPFProject_Lab4.Models;
using WPFProject_Lab4.Tools;
using WPFProject_Lab4.Tools.Managers;
using WPFProject_Lab4.Tools.MVVM;
using WPFProject_Lab4.Tools.Navigation;

namespace WPFProject_Lab4.ViewModels
{
    internal class MainViewModel : BaseViewModel
    {
        private ObservableCollection<User> _users;
        private User _selectedUser;
        private RelayCommand<object> _editUserCommand;
        private RelayCommand<object> _signInCommand;
        private RelayCommand<object> _removeUserCommand;

        public ObservableCollection<User> Users
        {
            get => _users;
            set
            {
                _users = value;
                OnPropertyChanged();
            }
        }

        public User SelectedUser
        {
            get => _selectedUser;
            set
            {
                if (value == null)
                    return;
                _selectedUser = value;
                StationManager.SelectedUser = _selectedUser;
                OnPropertyChanged();
            }
        }

        public RelayCommand<object> EditUserCommand
        {
            get
            {
                return _editUserCommand ?? (_signInCommand = new RelayCommand<object>(
                           o => NavigationManager.Instance.Navigate(ViewType.EditUser)));
            }
        }

        public RelayCommand<object> SignInCommand
        {
            get
            {
                return _signInCommand ??= new RelayCommand<object>(
                    o => NavigationManager.Instance.Navigate(ViewType.SignIn));
            }
        }

        public string CurrentUser
        {
            get { return $"Current User {StationManager.CurrentUser}"; }
        }

        public RelayCommand<object> RemoveUserCommand
        {
            get
            {
                return _removeUserCommand ??= new RelayCommand<object>(o => RemoveUserImplementation());
            }
        }

        private void RemoveUserImplementation()
        {
            Users.Remove(SelectedUser);
            StationManager.DataStorage.RemoveUser(SelectedUser);
        }

        public void UpdateUsers()
        {
            _users = new ObservableCollection<User>(StationManager.DataStorage.UsersList);
        }

        public MainViewModel()
        {
            _users = new ObservableCollection<User>(StationManager.DataStorage.UsersList);
        }
    }
}