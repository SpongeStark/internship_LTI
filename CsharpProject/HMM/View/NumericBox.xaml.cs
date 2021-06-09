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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace View
{
    /// <summary>
    /// NumericBox.xaml 的交互逻辑
    /// </summary>
    public partial class NumericBox : UserControl
    {
        public int numStates;
        public NumericBox()
        {
            InitializeComponent();
            numStates = 0;
        }

        private void Button_Click_up(object sender, RoutedEventArgs e)
        {
            // int number = int.Parse(lbl_number.Content.ToString());
            // number++;
            numStates++;
            lbl_number.Content = numStates;
        }

        private void Button_Click_down(object sender, RoutedEventArgs e)
        {
            //int number = int.Parse(lbl_number.Content.ToString());
            //number = number <= 0 ? 0 : number - 1;
            numStates = numStates <= 0 ? 0 : numStates - 1;
            lbl_number.Content = numStates;
        }
    }
}
