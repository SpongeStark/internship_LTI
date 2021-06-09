using Microsoft.Win32;
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

using Model;

namespace View
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        string pathOfInitStatesCsv;
        string pathOfTransProbsCsv;
        string pathOfEmissionProbsCsv;
        string pathOfObservationCsv;
        // int NumSates { get => numBox_State.numStates; }
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btn_OpenFile_InitStates_Click(object sender, RoutedEventArgs e)
        {
            setEvent_OpenFile(btn_OpenFile_InitStates, lbl_OpenFile_InitStates, ref pathOfInitStatesCsv);
        }

        private void btn_OpenFile_TransProbs_Click(object sender, RoutedEventArgs e)
        {
            setEvent_OpenFile(btn_OpenFile_TransProbs, lbl_OpenFile_TransProbs, ref pathOfTransProbsCsv);
        }

        private void btn_OpenFile_EmissionProbs_Click(object sender, RoutedEventArgs e)
        {
            setEvent_OpenFile(btn_OpenFile_EmissionProbs, lbl_OpenFile_EmissionProbs, ref pathOfEmissionProbsCsv);
        }

        private void btn_OpenFile_Click(object sender, RoutedEventArgs e)
        {
            setEvent_OpenFile(btn_OpenFile_observation, lbl_OpenFile_observation, ref pathOfObservationCsv);
        }

        private void setEvent_OpenFile(Button button, Label label, ref string path)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "CSV Files (*.csv)|*csv|Text Files (*.txt)|*.txt";
            if (openFileDialog.ShowDialog() == true)
            {
                path = openFileDialog.FileName;
                string[] temp = path.Split('\\');
                // show the file name
                label.Content = temp[temp.Length - 1];
                // change the content of button
                button.Content = "Change File";
            }
        }

        private HMM getHMM()
        {
            if(pathOfInitStatesCsv == null || pathOfTransProbsCsv == null || pathOfEmissionProbsCsv == null)
            {
                return null;
            }
            RDataFrame initData = CsvHelper.getDataFrame(pathOfInitStatesCsv);
            RDataFrame transData = CsvHelper.getDataFrame(pathOfTransProbsCsv);
            RDataFrame emissionData = CsvHelper.getDataFrame(pathOfEmissionProbsCsv);

            string[] states = initData.Title;
            string[] symbols = emissionData.Title;
            double[] pi = initData.toDoubleVector();
            double[,] A = transData.toMatrix();
            double[,] B = emissionData.toMatrix();

            return new HMM(states, symbols, pi, A, B);
        }

        private int[] getIntObservations(int startIndex)
        {
            if (pathOfObservationCsv == null) return null;
            RDataFrame observation = CsvHelper.getDataFrame(pathOfObservationCsv);
            double[] temp_O = observation.toDoubleVector();
            int[] O = new int[temp_O.Length];
            for (int i = 0; i < temp_O.Length; i++)
            {
                O[i] = (int)temp_O[i] - startIndex;
            }
            return O;
        }

        // Forward/Backward
        private void btn_FB_Click(object sender, RoutedEventArgs e)
        {
            HMM hmm = getHMM();
            int[] O = getIntObservations(0);
            if (hmm == null || O == null)
            {
                MessageBox.Show("Please input the data files!", "Files Error");
            }
            else
            {
                string output = hmm.ToString();
                output += "\nForward Table:\n" + hmm.getForwardTable(O);
                output += "\nBackward Table:\n" + hmm.getBackwardtable(O);
                //MessageBox.Show(output);
                ViterbiWindow resultWindow = new ViterbiWindow();
                resultWindow.Title = "Forward and Backward result";
                resultWindow.txtResult.Text = output;
                resultWindow.ShowDialog();
            }
        }

        // Baum-Welch
        private void btn_bw_Click(object sender, RoutedEventArgs e)
        {
            HMM hmm = getHMM();
            int[] O = getIntObservations(0);
            if (hmm == null || O == null)
            {
                MessageBox.Show("Please input the data files!","Files Error");
            }
            else
            {
                int maxIters;
                if(int.TryParse(txt_MaxIters.Text, out maxIters) && maxIters > 0)
                {
                    BaumWelch bwWindow = new BaumWelch(hmm, O, maxIters);
                    if (bwWindow.ShowDialog() == true)
                    {
                        //MessageBox.Show(bwWindow.Hmm.ToString(), "Baum-Welch result");
                        HmmDisplayWindow hmmDisplay = new HmmDisplayWindow(bwWindow.Hmm);
                        hmmDisplay.ShowDialog();
                    }
                }
                else
                {
                    MessageBox.Show("Please input a positive integer for the max iterations","MaxIters error");
                }
                
            }
        }

        private void btn_Viterbi_Click(object sender, RoutedEventArgs e)
        {
            HMM hmm = getHMM();
            int[] O = getIntObservations(0);
            if (hmm == null || O == null)
            {
                MessageBox.Show("Please input the data files!", "Files Error");
            }
            else
            {
                string output = "\"obs\"\t\"index\"\t\"Viterbi Path\"\n";
                RArray result = hmm.getViterbiPath(O);
                for (int t = 0; t < result.NumCol; t++)
                {
                    output += O[t] + "\t"+result[t] + "\t" + result.ColNames[t] + "\n";
                }
                ViterbiWindow viterbiWindow = new ViterbiWindow();
                viterbiWindow.txtResult.Text = output;
                viterbiWindow.Show();
            }
        }


        private void lbl_OpenFile_InitStates_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (pathOfInitStatesCsv != null)
            {
                RDataFrame transData = CsvHelper.getDataFrame(pathOfInitStatesCsv);
                MatrixWindow matrixWindow = new MatrixWindow(transData);
                matrixWindow.ShowDialog();
            }
        }

        private void lbl_OpenFile_TransProbs_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (pathOfTransProbsCsv != null)
            {
                RDataFrame transData = CsvHelper.getDataFrame(pathOfTransProbsCsv);
                MatrixWindow matrixWindow = new MatrixWindow(transData);
                matrixWindow.ShowDialog();
            }
        }

        private void lbl_OpenFile_EmissionProbs_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (pathOfEmissionProbsCsv != null)
            {
                RDataFrame transData = CsvHelper.getDataFrame(pathOfEmissionProbsCsv);
                MatrixWindow matrixWindow = new MatrixWindow(transData);
                matrixWindow.ShowDialog();
            }
        }

        private void lbl_OpenFile_observation_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (pathOfObservationCsv != null)
            {
                int[] observs = getIntObservations(0);
                PreviewGraphWindow previewGraphWindow = new PreviewGraphWindow(observs);
                previewGraphWindow.Show();
            }
        }
    }
}
