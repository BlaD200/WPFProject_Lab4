using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using WPFProject_Lab4.Models;
using WPFProject_Lab4.Tools;
using WPFProject_Lab4.Tools.Exceptions;
using WPFProject_Lab4.Tools.Managers;
using WPFProject_Lab4.Tools.MVVM;
using WPFProject_Lab4.Tools.Navigation;

namespace WPFProject_Lab4.ViewModels
{
    class EditUserViewModel : BaseViewModel
    {
        private RelayCommand<object> _confirmCommand;
        private RelayCommand<object> _backToUsersCommand;

        private string _login;
        private string _password;

        private String _name;
        private String _surname;
        private String _eMail;
        private DateTime? _birthday;

        #region Properties

        public string Login
        {
            get => _login;
            private set => _login = value;
        }

        public string Password
        {
            get => _password;
            private set => _password = value;
        }

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }

        public string Surname
        {
            get => _surname;
            set
            {
                _surname = value;
                OnPropertyChanged();
            }
        }

        public string Email
        {
            get => _eMail;
            set
            {
                _eMail = value;
                OnPropertyChanged();
            }
        }

        public DateTime? Birthday
        {
            get => _birthday;
            set
            {
                _birthday = value;
                OnPropertyChanged();
            }
        }

        #endregion

        internal EditUserViewModel()
        {
            Login = StationManager.SelectedUser.Login;
            Password = StationManager.SelectedUser.Password;
            Name = StationManager.SelectedUser.Name;
            Surname = StationManager.SelectedUser.Surname;
            Email = StationManager.SelectedUser.Email;
            Birthday = StationManager.SelectedUser.Birthday;
        }

        public RelayCommand<Object> EditUserCommand
        {
            get
            {
                return _confirmCommand ??= new RelayCommand<object>(
                    ConfirmImplementation);
            }
        }

        public RelayCommand<object> BackToUsersCommand
        {
            get
            {
                return _backToUsersCommand ??= (_backToUsersCommand = new RelayCommand<object>(
                    o => NavigationManager.Instance.Navigate(ViewType.Main)));
            }
        }

        private async void ConfirmImplementation(object obj)
        {
            LoaderManager.Instance.ShowLoader();
            var result = await Task.Run(() =>
            {
                try
                {
                    if (Birthday != null)
                    {
                        try
                        {
                            var newUser = new User(_login, _password, Name, Surname, Email, Birthday);
                            StationManager.DataStorage.EditUser(newUser);
                        }
                        catch (FutureBirthdayException)
                        {
                            MessageBox.Show(
                                $"Edit failed fo user {_login}. Reason:{Environment.NewLine}I don't think, Time machine had been invented...",
                                "Error",
                                MessageBoxButton.OK,
                                MessageBoxImage.Error);
                            return false;
                        }
                        catch (PastBirthdayException e)
                        {
                            MessageBox.Show($"Edit failed fo user {_login}. Reason:{Environment.NewLine}{e.Message}",
                                "Error", MessageBoxButton.OK,
                                MessageBoxImage.Error);
                            return false;
                        }
                        catch (InvalidEmailException e)
                        {
                            MessageBox.Show($"Edit failed fo user {_login}. Reason:{Environment.NewLine}{e.Message}",
                                "Error", MessageBoxButton.OK,
                                MessageBoxImage.Error);
                            return false;
                        }
                    }
                    else
                    {
                        MessageBox.Show(
                            $"Edit failed fo user {_login}. Reason:{Environment.NewLine}Birthday cannot be empty.",
                            "Error", MessageBoxButton.OK,
                            MessageBoxImage.Error);
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Edit failed fo user {_login}. Reason:{Environment.NewLine} {ex.Message}");
                    return false;
                }

                MessageBox.Show($"User {_login} was successfully edited.");
                return true;
            });
            LoaderManager.Instance.HideLoader();
            if (result)
            {
                if (Birthday?.Day == DateTime.Today.Day && Birthday?.Month == DateTime.Today.Month)
                    MessageBox.Show("Happy Birthday, my dear friend!🎊🎇🎇🎉🎉🎁🎁");
                Login = "";
                Password = "";
                Name = "";
                Surname = "";
                Email = "";
                Birthday = null;
                NavigationManager.Instance.Navigate(ViewType.Main);
            }
        }
    }
}