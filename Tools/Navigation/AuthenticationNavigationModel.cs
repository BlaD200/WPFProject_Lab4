using System;
using WPFProject_Lab4.Views;
using WPFProject_Lab4.Views.Authentication;
using MainView = WPFProject_Lab4.Views.MainView;
using SignUpView = WPFProject_Lab4.Views.Authentication.SignUpView;

namespace WPFProject_Lab4.Tools.Navigation
{
    internal class AuthenticationNavigationModel : BaseNavigationModel
    {
        public AuthenticationNavigationModel(IContentOwner contentOwner) : base(contentOwner)
        {
            
        }

        protected override void InitializeView(ViewType viewType)
        {
            switch (viewType)
            {
                case ViewType.SignIn:
                    AddView(ViewType.SignIn, new SignInView());
                    break;
                case ViewType.SignUp:
                    AddView(ViewType.SignUp, new SignUpView());
                    break;
                case ViewType.Main:
                    AddView(ViewType.Main, new MainView());
                    break;
                case ViewType.EditUser:
                    AddView(ViewType.EditUser, new EditUserView());
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(viewType), viewType, null);
            }
        }
    }
}
