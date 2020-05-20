using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace populationDatabase1
{
    /// <summary>
    /// Interaction logic for NewCityWindow.xaml
    /// </summary>
    public partial class NewCityWindow : Window
    {
        VM vm;
        public NewCityWindow(VM vm)
        {
            InitializeComponent();
            this.vm = vm;
            DataContext = vm;
        }

        public void Save_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                vm.AddRow();
                Close();
            }
            catch
            {
                MessageBox.Show("Check fields");
            }
        }

        private void NewCity_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = (NewCity.Text.Length == 0 && e.Key == Key.Space);
        }
    }
}
