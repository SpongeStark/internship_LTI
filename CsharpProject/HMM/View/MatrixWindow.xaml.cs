using Model;
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

namespace View
{
    /// <summary>
    /// MatrixWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MatrixWindow : Window
    {
        public MatrixWindow()
        {
            InitializeComponent();
        }

        public MatrixWindow(double[,] matrix):this()
        {
            myFormula.Formula = getLatex(matrix);
        }
        public MatrixWindow(RDataFrame probs):this(probs.toMatrix())
        { }

        public static string getLatex(double[,] matrix)
        {
            string temp = @"\pmatrix{";
            for(int i=0; i<matrix.GetLength(0); i++)
            {
                for(int j=0; j<matrix.GetLength(1); j++)
                {
                    string number = string.Format("{0:0.00####}", matrix[i, j]);
                    temp += number + (j==matrix.GetLength(1)-1?" ": "& ");
                }
                temp += (i == matrix.GetLength(0) - 1 ? " " : @"\\");
            }
            temp += @"}";
            return temp;
        }
        public static string getLatex(double[] vector)
        {
            double[,] matrix = new double[1, vector.Length];
            for(int i=0; i< vector.Length; i++)
            {
                matrix[0, i] = vector[i];
            }
            return getLatex(matrix);
        }
    }
}
