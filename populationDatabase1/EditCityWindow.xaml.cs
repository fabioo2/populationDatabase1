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
    /// Interaction logic for EditCityWindow.xaml
    /// </summary>
    public partial class EditCityWindow : Window
    {
        VM vm;
        public EditCityWindow(VM vm)
        {
            InitializeComponent();
            this.vm = vm;
            DataContext = vm;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                vm.EditRow();
                Close();
            }
            catch
            {
                MessageBox.Show("Check fields");
            }
        }
    }
}
