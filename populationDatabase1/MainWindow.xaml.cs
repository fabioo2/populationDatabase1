using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace populationDatabase1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        VM vm = new VM();
        public MainWindow()
        {
            InitializeComponent();
            DataContext = vm;
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            EditCityWindow ecw = new EditCityWindow(vm)
            {
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
                Owner = this
            };

            if (vm.SelectedCity != null)
                ecw.ShowDialog();

            vm.Clear();
        }

        private void New_Click(object sender, RoutedEventArgs e)
        {
            NewCityWindow ncw = new NewCityWindow(vm)
            {
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
                Owner = this
            };
            ncw.ShowDialog();

            vm.Clear();
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (vm.SelectedCity != null)
            {
                vm.DeleteRow();
            }
        }

        private void Query_Click(object sender, RoutedEventArgs e)
        {
            QueryWindow qw = new QueryWindow(vm)
            {
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
                Owner = this
            };
            qw.ShowDialog();

            vm.Clear();
        }
    }
}
