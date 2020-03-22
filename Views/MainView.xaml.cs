using System.Windows.Controls;
using WPFProject_Lab4.Tools.Navigation;
using WPFProject_Lab4.ViewModels;

namespace WPFProject_Lab4.Views
{
    public partial class MainView : UserControl, INavigatable
    {
        public MainView()
        {
            InitializeComponent();
            DataContext = new MainViewModel(); 
        }
    }
}
