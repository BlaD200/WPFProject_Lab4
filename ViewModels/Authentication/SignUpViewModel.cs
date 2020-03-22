using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using WPFProject_Lab4.Models;
using WPFProject_Lab4.Tools;
using WPFProject_Lab4.Tools.Exceptions;
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

        private String _name;
        private String _surname;
        private String _eMail;
        private DateTime? _birthday;

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
                           SignUpImplementation, o => CanExecuteCommand()));
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

        private async void SignUpImplementation(object obj)
        {
            LoaderManager.Instance.ShowLoader();
            var result = await Task.Run(() =>
            {
                try
                {
                    Thread.Sleep(200);
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
                    if (Birthday != null)
                    {
                        try
                        {
                            var user = new User(_login, _password, Name, Surname, Email, Birthday);
                            StationManager.DataStorage.AddUser(user);
                            StationManager.CurrentUser = user;
                        }
                        catch (InvalidNameException e)
                        {
                            MessageBox.Show($"Sign Up failed fo user {_login}. Reason:{Environment.NewLine}{e.Message}", "Error",
                                MessageBoxButton.OK,
                                MessageBoxImage.Error);
                            return false;
                        }
                        catch (FutureBirthdayException)
                        {
                            MessageBox.Show($"Sign Up failed fo user {_login}. Reason:{Environment.NewLine}I don't think, Time machine had been invented...", "Error",
                                MessageBoxButton.OK,
                                MessageBoxImage.Error);
                            return false;
                        }
                        catch (PastBirthdayException e)
                        {
                            MessageBox.Show($"Sign Up failed fo user {_login}. Reason:{Environment.NewLine}{e.Message}", "Error", MessageBoxButton.OK,
                                MessageBoxImage.Error);
                            return false;
                        }
                        catch (InvalidEmailException e)
                        {
                            MessageBox.Show($"Sign Up failed fo user {_login}. Reason:{Environment.NewLine}{e.Message}", "Error", MessageBoxButton.OK,
                                MessageBoxImage.Error);
                            return false;
                        }
                    }
                    else
                    {
                        MessageBox.Show($"Sign Up failed fo user {_login}. Reason:{Environment.NewLine}Birthday cannot be empty.", "Error", MessageBoxButton.OK,
                            MessageBoxImage.Error);
                        return false;
                    }
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
