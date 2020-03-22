using System.Windows.Controls;
using WPFProject_Lab4.Tools.Navigation;
using WPFProject_Lab4.ViewModels;
using WPFProject_Lab4.ViewModels.Authentication;

namespace WPFProject_Lab4.Views
{
    public partial class EditUserView : UserControl, INavigatable
    {
        internal EditUserView()
        {
            InitializeComponent();
            DataContext = new EditUserViewModel();
        }
    }
}
