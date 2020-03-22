using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using WPFProject_Lab4.Models;
using WPFProject_Lab4.Tools;
using WPFProject_Lab4.Tools.Managers;
using WPFProject_Lab4.Tools.MVVM;
using WPFProject_Lab4.Tools.Navigation;

namespace WPFProject_Lab4.ViewModels.Authentication
{
    internal class SignUpViewModel:BaseViewModel
    {
        #region Fields
        private string _login;
        private string _password;

        #region Commands
        private RelayCommand<object> _signInCommand;
        private RelayCommand<object> _signUpCommand;
        private RelayCommand<object> _closeCommand;
        #endregion
        #endregion

        #region Properties
        public string Login
        {
            get { return _login; }
            set
            {
                _login = value.Replace(" ", "Space");
                OnPropertyChanged();
            }
        }
        public string Password
        {
            get { return _password; }
            set
            {
                _password = "";
                for (int i = 0; i < value.Length; i++)
                {
                    _password += "*";
                }
                OnPropertyChanged();
            }
        }

        #region Commands

        public RelayCommand<object> SignInCommand
        {
            get
            {
                return _signInCommand ?? (_signInCommand = new RelayCommand<object>(
                           o => NavigationManager.Instance.Navigate(ViewType.SignIn)));
            }
        }

        public RelayCommand<Object> SignUpCommand
        {
            get
            {
                return _signUpCommand ?? (_signUpCommand = new RelayCommand<object>(
                           SignUpInplementation, o => CanExecuteCommand()));
            }
        }

        public RelayCommand<Object> CloseCommand
        {
            get
            {
                return _closeCommand ?? (_closeCommand = new RelayCommand<object>(o => Environment.Exit(0)));
            }
        }

        #endregion
        #endregion

        private bool CanExecuteCommand()
        {
            return !string.IsNullOrWhiteSpace(_login) && !string.IsNullOrWhiteSpace(_password);
        }

        private async void SignUpInplementation(object obj)
        {
            LoaderManager.Instance.ShowLoader();
            var result = await Task.Run(() =>
            {
                try
                {
                    Thread.Sleep(1000);
                    if (StationManager.DataStorage.UserExists(_login))
                    {
                        MessageBox.Show($"Sign Up failed fo user {_login}. Reason:{Environment.NewLine} User with such login already exists.");
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Sign Up failed fo user {_login}. Reason:{Environment.NewLine} {ex.Message}");
                    return false;
                }
                try
                {
                    var user = new User(_login, _password);
                    StationManager.DataStorage.AddUser(user);
                    StationManager.CurrentUser = user;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Sign Up failed fo user {_login}. Reason:{Environment.NewLine} {ex.Message}");
                    return false;
                }
                MessageBox.Show($"User {_login} was successfully created.");
                return true;
            });
            LoaderManager.Instance.HideLoader();
            if (result)
                NavigationManager.Instance.Navigate(ViewType.Main);
        }
    }
}
