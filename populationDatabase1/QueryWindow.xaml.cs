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
    /// Interaction logic for QueryWindow.xaml
    /// </summary>
    public partial class QueryWindow : Window
    {
        VM vm;
        public QueryWindow(VM vm)
        {
            InitializeComponent();
            this.vm = vm;
            DataContext = vm;
        }

        private void Total_Click(object sender, RoutedEventArgs e)
        {
            vm.CalcTotal();
        }

        private void Average_Click(object sender, RoutedEventArgs e)
        {
            vm.CalcAverage();
        }

        private void Lowest_Click(object sender, RoutedEventArgs e)
        {
            vm.CalcLowest();
        }

        private void Highest_Click(object sender, RoutedEventArgs e)
        {
            vm.CalcHighest();
        }
    }
}
