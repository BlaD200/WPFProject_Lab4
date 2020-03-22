using System.Windows.Controls;
using WPFProject_Lab4.Tools.Navigation;
using WPFProject_Lab4.ViewModels.Authentication;

namespace WPFProject_Lab4.Views.Authentication
{
    public partial class SignInView : UserControl,INavigatable
    {
        internal SignInView()
        {
            InitializeComponent();
            DataContext = new SignInViewModel();
        }
    }
}
