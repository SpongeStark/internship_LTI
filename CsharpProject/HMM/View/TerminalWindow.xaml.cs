using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace View
{
    /// <summary>
    /// TerminalWindow.xaml 的交互逻辑
    /// </summary>
    public partial class TerminalWindow : Window
    {
        public TerminalWindow()
        {
            InitializeComponent();
            txtResult.Text = "start";
            
        }

        private void btn1_Click(object sender, RoutedEventArgs e)
        {
            Task.Factory.StartNew(() => { updateUIMethod(); });
        }

        private void updateUIMethod()
        {
            //txtResult.Text += ".";
            for (int i = 0; i < 10; i++)
            {
                Dispatcher.BeginInvoke(new Action(() =>
                {
                    txtResult.Text += ".";
                }), System.Windows.Threading.DispatcherPriority.Background);
                
                Thread.Sleep(500);
            }
        }
    }
}
