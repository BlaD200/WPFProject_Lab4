using System.Windows.Controls;
using WPFProject_Lab4.Tools.Navigation;
using WPFProject_Lab4.ViewModels.Authentication;

namespace WPFProject_Lab4.Views.Authentication
{
    public partial class SignUpView : UserControl, INavigatable
    {
        internal SignUpView()
        {
            InitializeComponent();
            DataContext = new SignUpViewModel();
        }
    }
}
