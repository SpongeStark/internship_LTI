using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public delegate void OneTimeFinish(int time);

namespace Model
{
    public class HMM
    {
        private int numStates;
        private int numSymbols;
        private String[] states;
        private String[] symbols;
        private double[] startProbs;
        private double[,] transProbs;
        private double[,] emissionProbs;

        double Inf = 999999999;

        public int NumStates { get => numStates; }
        public int NumSymbols { get => numSymbols; }
        public string[] States { get => states; }
        public string[] Symbols { get => symbols; }
        public double[] StartProbs { get => startProbs; }
        public double[,] TransProbs { get => transProbs; }
        public double[,] EmissionProbs { get => emissionProbs; }


        #region Constructors

        public HMM(String[] states, String[] symbols, double[] startProbs, double[,] transProbs, double[,] emissionProbs)
        {
            numStates = states.Length;
            numSymbols = symbols.Length;
            this.states = states;
            this.symbols = symbols;
            this.startProbs = startProbs == null ? new double[numStates] : startProbs;
            this.transProbs = transProbs == null ? new double[numStates, numStates] : transProbs;
            this.emissionProbs = emissionProbs == null ? new double[numStates, numSymbols] : emissionProbs;
        }

        public HMM(String[] states, String[] symbols, double[] startProbs) : this(states, symbols, startProbs, null, null)
        {
            for (int i = 0; i < numStates; i++)
            {
                for (int j = 0; j < numStates; j++)
                {
                    this.transProbs[i, j] = 1.0 / numStates;
                }
                for (int j = 0; j < numSymbols; j++)
                {
                    this.emissionProbs[i, j] = 1.0 / numSymbols;
                }
            }
        }

        public HMM(String[] states, String[] symbols) : this(states, symbols, null)
        {
            for (int i = 0; i < numStates; i++)
            {
                this.startProbs[i] = 1.0 / numStates;
            }
        }

        public HMM(int numStates, int numSymbols) : this(new string[numStates], new string[numSymbols])
        {
            for (int i = 0; i < numStates; i++)
            {
                states[i] = "State " + i;
            }
            for (int i = 0; i < numSymbols; i++)
            {
                symbols[i] = "Symbols " + i;
            }
        }

        #endregion

        #region Forward

        /// <summary>
        /// The Forward algorithm
        /// </summary>
        /// <param name="observation"></param>
        /// <returns>A table of alpha pass</returns>
        public RArray getForwardTable(int[] observation)
        {
            double[,] f = forward(observation);
            int numObservations = observation.Length;
            // translate the Log values to normal value by Exp them
            for (int i = 0; i < numStates; i++)
            {
                for (int k = 0; k < numObservations; k++)
                {
                    f[i, k] = Math.Exp(f[i, k]);
                }
            }
            // build the table
            String[] colNames = new String[numObservations];
            for (int i = 0; i < numObservations; i++)
            {
                colNames[i] = i.ToString();
            }
            return new RArray(f, states, colNames);
        }

        // The log value of the result of forward algorithm
        double[,] forward(int[] observation)
        {
            int numObservations = observation.Length;
            double[,] f = new double[numStates, numObservations];
            // Init
            for (int i = 0; i < numStates; i++)
            {
                f[i, 0] = Math.Log(startProbs[i] * emissionProbs[i, observation[0]]);
            }
            // Iteration
            for (int k = 1; k < numObservations; k++)
            {
                for (int i = 0; i < numStates; i++)
                {
                    double LogSum = -Inf;
                    for (int j = 0; j < numStates; j++)
                    {
                        double temp = f[j, k - 1] + Math.Log(transProbs[j, i]);
                        if (temp > -Inf)
                        {
                            LogSum = temp + Math.Log(1 + Math.Exp(LogSum - temp));
                        }
                    }
                    f[i, k] = Math.Log(emissionProbs[i, observation[k]]) + LogSum;
                }
            }
            return f;
        }

        #endregion

        #region Backward
        /// <summary>
        /// The Backward algorithm
        /// </summary>
        /// <param name="observation"></param>
        /// <returns>A table of beta pass</returns>
        public RArray getBackwardtable(int[] observation)
        {
            double[,] b = backward(observation);
            int numObservations = observation.Length;
            // translate the Log values to normal value by Exp them
            for (int i = 0; i < numStates; i++)
            {
                for (int k = 0; k < numObservations; k++)
                {
                    b[i, k] = Math.Exp(b[i, k]);
                }
            }
            // build the table
            String[] colNames = new String[numObservations];
            for (int i = 0; i < numObservations; i++)
            {
                colNames[i] = i.ToString();
            }
            return new RArray(b, states, colNames);
        }

        // The log value of the result of backward algorithm
        public double[,] backward(int[] observation)
        {
            int numObservations = observation.Length;
            double[,] b = new double[numStates, numObservations];
            // Init
            for (int i = 0; i < numStates; i++)
            {
                b[i, numObservations - 1] = 0;//log(1)
            }
            // Iteration
            for (int k = numObservations - 2; k >= 0; k--)
            {
                for (int i = 0; i < numStates; i++)
                {
                    double logSum = -Inf;
                    for (int j = 0; j < numStates; j++)
                    {
                        double temp = b[j, k + 1] + Math.Log(transProbs[i, j] * emissionProbs[j, observation[k + 1]]);
                        if (temp > -Inf)
                        {
                            logSum = temp + Math.Log(1 + Math.Exp(logSum - temp));
                        }
                    }
                    b[i, k] = logSum;
                }
            }
            return b;
        }
        #endregion

        #region Update the Model
        public bool updateModel(double[,] transitionMatrix, double[,] emissionMatrix)
        {
            return updateModel(new HMM(this.states, this.symbols, this.startProbs, transitionMatrix, emissionMatrix));
        }

        public bool updateModel(HMM newModel)
        {
            if (numStates != newModel.NumStates || numSymbols != newModel.NumSymbols)
            {
                return false;
            }
            transProbs = newModel.TransProbs;
            emissionProbs = newModel.EmissionProbs;
            return true;
        }
        #endregion

        #region Baum-Welch

        public HMM baumWelch(int[] observations)
        {
            return baumWelch(observations, 100);
        }

        public HMM baumWelch(int[] observation, int maxIterations)
        {
            //double delta = Math.Pow(10, -9);
            return baumWelch(observation, maxIterations, 1E-9, new OneTimeFinish((i) => { }));
        }

        public HMM baumWelch(int[] observation, OneTimeFinish doSomething)
        {
            return baumWelch(observation, 100, doSomething);
        }

        public HMM baumWelch(int[] observation, int maxIterations, OneTimeFinish doSomething)
        {
            //double delta = Math.Pow(10, -9);
            return baumWelch(observation, maxIterations, 1E-9, doSomething);
        }

        public HMM baumWelch(int[] observation, int maxIterations, double delta, OneTimeFinish doSomething)
        {
            HMM tempHMM = this;
            List<double> diff = new List<double>();
            for (int i = 0; i < maxIterations; i++)
            {
                HMM bw = baumWelchRecursion(tempHMM, observation);
                double[,] T = bw.TransProbs;
                double[,] E = bw.EmissionProbs;
                // 没懂pseudo Count是干嘛的
                T = normalizeMatrx(T);
                E = normalizeMatrx(E);
                double d = diffMatrix(tempHMM.TransProbs, T) + diffMatrix(tempHMM.EmissionProbs, E);
                diff.Add(d);
                if (!tempHMM.updateModel(T, E)) { Console.WriteLine("Error"); }
                doSomething(i + 1);
                if (d < delta)
                {
                    break;
                }
            }
            return tempHMM;
        }

        public HMM baumWelchRecursion(HMM hmm, int[] observation)
        {
            double[,] transitionMatrx = new double[hmm.NumStates, hmm.NumStates];
            double[,] emissionMatrx = new double[hmm.NumStates, hmm.NumSymbols];
            double[,] f = hmm.forward(observation);
            double[,] b = hmm.backward(observation);
            double probObservations = f[0, observation.Length - 1];
            for (int i = 1; i < hmm.NumStates; i++)
            {
                double j = f[i, observation.Length - 1];
                if (j > -Inf)
                {
                    probObservations = j + Math.Log(1 + Math.Exp(probObservations - j));
                }
            }
            for (int x = 0; x < hmm.NumStates; x++)
            {
                for (int y = 0; y < hmm.NumStates; y++)
                {
                    double temp = f[x, 0] + Math.Log(hmm.TransProbs[x, y]) +
                        Math.Log(hmm.EmissionProbs[y, observation[0 + 1]]) + b[y, 0 + 1];
                    for (int i = 1; i < observation.Length - 1; i++)
                    {
                        double j = f[x, i] + Math.Log(hmm.TransProbs[x, y]) +
                            Math.Log(hmm.EmissionProbs[y, observation[i + 1]]) + b[y, i + 1];
                        if (j > -Inf)
                        {
                            temp = j + Math.Log(1 + Math.Exp(temp - j));
                        }
                    }
                    temp = Math.Exp(temp - probObservations);
                    transitionMatrx[x, y] = temp;
                }
            }
            for (int x = 0; x < hmm.NumStates; x++)
            {
                for (int s = 0; s < hmm.NumSymbols; s++)
                {
                    double temp = -Inf;
                    for (int i = 0; i < observation.Length; i++)
                    {
                        if (s == observation[i])
                        {
                            double j = f[x, i] + b[x, i];
                            if (j > -Inf)
                            {
                                temp = j + Math.Log(1 + Math.Exp(temp - j));
                            }
                        }
                    }
                    temp = Math.Exp(temp - probObservations);
                    emissionMatrx[x, s] = temp;
                }
            }
            return new HMM(hmm.States, hmm.Symbols, hmm.StartProbs, transitionMatrx, emissionMatrx);
        }

        double[,] normalizeMatrx(double[,] matrix)
        {
            double[,] M = matrix;
            double nrow = M.GetLength(0);
            double ncol = M.GetLength(1);
            for (int i = 0; i < nrow; i++)
            {
                double sum = 0;
                for (int j = 0; j < ncol; j++)
                {
                    sum += M[i, j];
                }
                for (int j = 0; j < ncol; j++)
                {
                    M[i, j] = M[i, j] / sum;
                }
            }
            return M;
        }

        double diffMatrix(double[,] T1, double[,] T2)
        {
            double result = 0;
            double nrow = T1.GetLength(0);
            double ncol = T1.GetLength(1);
            if (T2.GetLength(0) != nrow || T2.GetLength(1) != ncol)
            {
                return -1;
            }
            for (int i = 0; i < nrow; i++)
            {
                for (int j = 0; j < ncol; j++)
                {
                    result += (T1[i, j] - T2[i, j]) * (T1[i, j] - T2[i, j]);
                }
            }
            result = Math.Sqrt(result);
            return result;
        }

        #endregion

        #region Viterbi
        public RArray getViterbiPath(int[] observation)
        {
            int[] viterbiPath = viterbi(observation);
            string[] strPath = new string[observation.Length];
            double[] pathIndex = new double[observation.Length];
            for (int i = 0; i < observation.Length; i++)
            {
                strPath[i] = states[viterbiPath[i]];
                pathIndex[i] = viterbiPath[i];
            }
            return new RArray(pathIndex, strPath);
        }

        public int[] viterbi(int[] observation)
        {
            int numObservations = observation.Length;

            double[,] v = new double[numStates, numObservations];
            // Init
            for (int i = 0; i < numStates; i++)
            {
                v[i, 0] = Math.Log(startProbs[i] * emissionProbs[i, observation[0]]);
            }
            // Iteration
            for (int k = 1; k < numObservations; k++)
            {
                for (int i = 0; i < numStates; i++)
                {
                    double maxi = -Inf;
                    for (int j = 0; j < numStates; j++)
                    {
                        double temp = v[j, k - 1] + Math.Log(transProbs[j, i]);
                        maxi = Math.Max(maxi, temp);
                    }
                    v[i, k] = Math.Log(emissionProbs[i, observation[k]]) + maxi;
                }
            }
            // Traceback
            int[] viterbiPath = new int[numObservations];
            // get the last state
            double maxTemp = -Inf;
            for (int i = 0; i < numStates; i++)
            {
                if (v[i, numObservations - 1] > maxTemp)
                {
                    maxTemp = v[i, numObservations - 1];
                    viterbiPath[numObservations - 1] = i;
                }
            }
            // roll back
            for (int t = numObservations - 2; t >= 0; t--)
            {
                maxTemp = -Inf;
                for (int i = 0; i < numStates; i++)
                {
                    if (v[i, t] > maxTemp)
                    {
                        maxTemp = v[i, t] + Math.Log(transProbs[i, viterbiPath[t + 1]]);
                        viterbiPath[t] = i;
                    }
                }
            }
            return viterbiPath;
        }


        #endregion

        public override String ToString()
        {
            String builder = "HMM \nStart Probabilities : \n" +
                    new RArray(startProbs, states) +
                    "\nTransition Probabilities : \n" +
                    new RArray(transProbs, states, states) +
                    "\nEmission Probabilities : \n" +
                    new RArray(emissionProbs, states, symbols);
            return builder;
        }
    }
}
