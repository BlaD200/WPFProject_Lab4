namespace WPFProject_Lab4.Tools.Navigation
{
    internal enum ViewType
    {
        SignIn = 0,
        SignUp = 1,
        Main = 2,
    }

    interface INavigationModel
    {
        void Navigate(ViewType viewType);
    }
}
