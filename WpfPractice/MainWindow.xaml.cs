using System.Windows;
using WpfPractice.ViewModels;

namespace WpfPractice
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel();
        }

        private void Sum(object sender, RoutedEventArgs e)
        {
            //var vm = DataContext as MainWindowViewModel;
            //vm.Result = vm.NumOne + vm.NumTwo;
        }
    }
}
