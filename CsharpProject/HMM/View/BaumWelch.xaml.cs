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

using Model;

namespace View
{
    /// <summary>
    /// BaumWelch.xaml 的交互逻辑
    /// </summary>
    public partial class BaumWelch : Window
    {
        OneTimeFinish doSomething;
        HMM hmm;
        int[] observations;
        int maxIters;

        public HMM Hmm { get => hmm;}

        public BaumWelch(HMM hmm, int[] observations, int maxIters)
        {
            InitializeComponent();
            this.hmm = hmm;
            this.observations = observations;
            this.maxIters = maxIters;
            progressBar.Maximum = maxIters;
            doSomething = new OneTimeFinish(updateUI);
            startWork();
        }

        private void updateUI(int time)
        {
            Dispatcher.BeginInvoke(new Action(() =>
            {
                progressBar.Value = time;
                txtResult.Text += ".";
                if (time % 50 == 0)
                {
                    txtResult.Text += (time + " times\n");
                }
            }), System.Windows.Threading.DispatcherPriority.Background);
        }

        private void showResult(HMM resultHMM)
        {
            Dispatcher.BeginInvoke(new Action(() =>
            {
                txtResult.Text += resultHMM;
                DialogResult = true;
            }), System.Windows.Threading.DispatcherPriority.Background);
        }

        public void startWork()
        {
            HMM temp = null;
            Task.Factory.StartNew(() => { 
                temp = hmm.baumWelch(observations, maxIters, doSomething);
                hmm = temp;
                showResult(temp);
            });
        }

        /*
         * Here, I have used method "BeginInvoke" with Dispatcher because "Dispatcher.BeginInvoke" schedules the given action for execution in the UI thread and then returns control to allow the current thread to continue executing.
         * But, Invoke method blocks the caller until the scheduled action is completed.
         * The priority of dispatcher describes the priority at which the operation can be invoked. Here, I used DispatcherPriority.Background as a dispatccher priorty. The Priority "Background" explains that I will start my operation after all other non-idle operations are complete.
         * I defined UI updating operations inside action delegate "new Action(() =>".
        */
    }
}
