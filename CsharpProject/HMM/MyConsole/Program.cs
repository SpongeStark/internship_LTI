using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Model;

namespace MyConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            //HMM hmm = initHmm();
            //testForward(hmm);
            //testBackward(hmm);
            //testBaumWelch(hmm);

            //testReadCSV();
            //testWriteCSV();

            //testLearningByBaumWelch();
            //testAllInputFiles();
            //testViterbi();

            //testSomething();

            testMyHMM();
            Console.ReadKey();
        }

        static HMM initHmm()
        {
            String[] states = {
                "H","C"
            };
            String[] symbols = {
                "S", "M", "L"
            };
            // state matrix
            double[,] A = {
                {0.7, 0.3},
                {0.4, 0.6}
            };
            // process matrix
            double[,] B = {
                {0.1, 0.4, 0.5},
                {0.7, 0.2, 0.1}
            };
            // init state, 0 ~ N-1
            double[] pi = { 0, 1 };


            return new HMM(states, symbols, pi, A, B);
        }

        static void testForward(HMM hmm)
        {
            // observations, 0 ~ T-1
            int[] O = { 0, 1, 0, 2 };
            RArray forwardResult = hmm.getForwardTable(O);
            Console.WriteLine("Forward-Algorithm:");
            Console.WriteLine(forwardResult);
        }

        static void testBackward(HMM hmm)
        {
            // observations, 0 ~ T-1
            int[] O = { 0, 1, 0, 2 };
            RArray backwardResult = hmm.getBackwardtable(O);
            Console.WriteLine("Backward-Algorithm:");
            Console.WriteLine(backwardResult);
        }

        static void testBaumWelch(HMM hmm)
        {
            int[] O = { 0, 1, 0, 2 };
            Console.WriteLine(hmm.baumWelch(O, doSomethingAfterOneTimeFinish));
            //HMM hmm2 = new HMM(2, 3);
            //Console.WriteLine(hmm2);
        }

        static void doSomethingAfterOneTimeFinish(int time)
        {
            Console.Write(".");
            if(time % 50 == 0)
            {
                Console.WriteLine(time + " times");
            }
        }
        static void testSomething()
        {
        }

        static void testReadCSV()
        {
            CsvHelper myHelper = new CsvHelper(@"C:\Users\56531\Desktop\test_data\testData.csv");
            RDataFrame data = myHelper.getDataByStreamReader(',');
            //List<List<string>> data = myHelper.getDataByTextfieldParser(',');
            //foreach (List<string> column in data)
            //{
            //    Console.WriteLine("Colume :");
            //    for(int i=0; i<5; i++)
            //    {
            //        Console.Write(column[i]);
            //    }
            //    Console.WriteLine();
            //}
            Console.WriteLine(data.ToStringByColumn(20));
        }

        static void testWriteCSV()
        {
            CsvHelper myHelper = new CsvHelper(@"C:\Users\56531\Desktop\test_data\testWriteData.csv");
            RDataFrame data = new RDataFrame("A", "B");
            data.addLine(1.ToString(), 2.ToString());
            data.addLine(2.ToString(), 1.ToString());
            data.addLine(1.ToString(), 3.ToString());
            data.addLine(2.ToString(), 2.ToString());
            data.addLine(1.ToString(), 3.ToString());
            data.addLine(1.ToString(), 3.ToString());
            data.addLine(2.ToString(), 4.ToString());
            myHelper.writeDataByStreamWriter(data, ',');
            //myHelper.writeDataByWriteAllText(data, ',');
            Console.WriteLine("Success");
        }

        static void testLearningByBaumWelch()
        {
            string[] states = { "Hot", "Cold" };
            string[] symbols = { "S", "M", "L" };
            // state matrix
            double[,] A = {
                {0.6, 0.4},
                {0.3, 0.7}
            };
            // process matrix
            double[,] B = {
                {0.3, 0.3, 0.4},
                {0.2, 0.5, 0.3}
            };
            // init state, 0 ~ N-1
            double[] pi = { 0.4, 0.6 };
            HMM initHMM = new HMM(states, symbols, pi, A, B);
            //CsvHelper myCsvHelper = new CsvHelper(@"C:\Users\56531\Desktop\test_data\testData.csv");
            CsvHelper myCsvHelper = new CsvHelper(@"C:\Users\56531\Desktop\testDataObs10000.csv");
            RDataFrame data = myCsvHelper.getDataByStreamReader(',');
            string[] strO = data.getColumn("observations");
            int[] O = new int[data.Size];
            
            for (int i = 0; i < data.Size; i++)
            {
                O[i] = int.Parse(strO[i]) - 0;
            }
            HMM newHMM = initHMM.baumWelch(O,5999,doSomethingAfterOneTimeFinish);
            Console.WriteLine(newHMM);

        }

        static void testAllInputFiles()
        {
            //CsvHelper initReader = new CsvHelper(@"/Users/yangkai/Documents/Codes/VisualStudio/internship/HMM_from_R/data/startProbs.csv");
            RDataFrame initData = CsvHelper.getDataFrame(@"/Users/yangkai/Documents/Codes/VisualStudio/internship/HMM_from_R/data/startProbs.csv");
            RDataFrame transData = CsvHelper.getDataFrame(@"/Users/yangkai/Documents/Codes/VisualStudio/internship/HMM_from_R/data/transProbs.csv");
            RDataFrame emissionData = CsvHelper.getDataFrame(@"/Users/yangkai/Documents/Codes/VisualStudio/internship/HMM_from_R/data/emissionProbs.csv");

            string[] states = initData.Title;
            string[] symbols = emissionData.Title;
            double[] pi = initData.toDoubleVector();
            double[,] A = transData.toMatrix();
            double[,] B = emissionData.toMatrix();


            HMM hmm = new HMM(states, symbols, pi, A, B);
            Console.WriteLine(hmm);
        }

        static void testViterbi()
        {
            RDataFrame initData = CsvHelper.getDataFrame(@"C:\Users\56531\Desktop\test_data\test_FB/startProbs.csv");
            RDataFrame transData = CsvHelper.getDataFrame(@"C:\Users\56531\Desktop\test_data\test_FB/transProbs.csv");
            RDataFrame emissionData = CsvHelper.getDataFrame(@"C:\Users\56531\Desktop\test_data\test_FB/emissionProbs.csv");

            string[] states = initData.Title;
            string[] symbols = emissionData.Title;
            double[] pi = initData.toDoubleVector();
            double[,] A = transData.toMatrix();
            double[,] B = emissionData.toMatrix();

            HMM hmm = new HMM(states, symbols, pi, A, B);

            int[] O = new int[] { 0, 1, 0, 2 };
            Console.WriteLine(hmm.getViterbiPath(O).colNamesToString());

        }

        static void testMyHMM()
        {
            string[] states = { "Hot", "Cold" };
            string[] symbols = { "S", "M", "L" };
            // state matrix
            double[,] A = {
                {0.6, 0.4},
                {0.3, 0.7}
            };
            // process matrix
            double[,] B = {
                {0.3, 0.3, 0.4},
                {0.2, 0.5, 0.3}
            };
            // init state, 0 ~ N-1
            double[] pi = { 0.4, 0.6 };
            MyHMM initHMM = new MyHMM(states, symbols, pi, A, B);
            //CsvHelper myCsvHelper = new CsvHelper(@"C:\Users\56531\Desktop\test_data\testData.csv");
            CsvHelper myCsvHelper = new CsvHelper(@"C:\Users\56531\Desktop\test_data\test_BaumWelch\testDataObs10000.csv");
            RDataFrame data = myCsvHelper.getDataByStreamReader(',');
            string[] strO = data.getColumn("observations");
            int[] O = new int[data.Size];

            for (int i = 0; i < data.Size; i++)
            {
                O[i] = int.Parse(strO[i]) - 0;
            }
            MyHMM newHMM = initHMM.baumWelch(O, 999, doSomethingAfterOneTimeFinish);
            Console.WriteLine(newHMM);
        }
    }
}
