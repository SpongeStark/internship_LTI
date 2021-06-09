using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class MyHMM
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

        public double B<T>(int i, T k)
        {
            if (k is int k_discrete)
            {
                return emissionProbs[i, k_discrete];
            }
            if (k is double k_continuous)
            {
                return normalDistributionProb(emissionProbs[i, 0], Math.Sqrt(emissionProbs[i, 1]), k_continuous);
            }
            return 0;
        }

        private double normalDistributionProb(double mu, double sigma, double X)
        {
            double denom = sigma * Math.Sqrt(2 * Math.PI);
            double exponent = -(X - mu) * (X - mu) / (2 * sigma * sigma);
            return Math.Exp(exponent) / denom;
        }

        #region Constructors

        public MyHMM(String[] states, String[] symbols, double[] startProbs, double[,] transProbs, double[,] emissionProbs)
        {
            numStates = states.Length;
            numSymbols = symbols.Length;
            this.states = states;
            this.symbols = symbols;
            this.startProbs = startProbs ?? (new double[numStates]);
            this.transProbs = transProbs ?? (new double[numStates, numStates]);
            this.emissionProbs = emissionProbs ?? (new double[numStates, numSymbols]);
        }

        public MyHMM(String[] states, String[] symbols, double[] startProbs) : this(states, symbols, startProbs, null, null)
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

        public MyHMM(String[] states, String[] symbols) : this(states, symbols, null)
        {
            for (int i = 0; i < numStates; i++)
            {
                this.startProbs[i] = 1.0 / numStates;
            }
        }

        public MyHMM(int numStates, int numSymbols) : this(new string[numStates], new string[numSymbols])
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
        public RArray getForwardTable<T>(T[] observation)
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
        public double[,] forward<T>(T[] observation)
        {
            int numObservations = observation.Length;
            double[,] f = new double[numStates, numObservations];
            // Init
            for (int i = 0; i < numStates; i++)
            {
                f[i, 0] = Math.Log(startProbs[i] * B(i, observation[0]));
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
                    f[i, k] = Math.Log(B(i, observation[k])) + LogSum;
                }
            }
            return f;
        }

        #endregion

        public double getProbObs(double[,] LogForward)
        {
            int lengthObs = LogForward.Length / numStates;
            //算概率P(O|\lambda)，把最后的\alpha全部加起来
            double probObservations = LogForward[0, lengthObs - 1];
            for (int i = 1; i < numStates; i++)
            {
                double j = LogForward[i, lengthObs - 1];
                if (j > -Inf)
                {
                    //ln(sum+b)=ln(b)+ln(1+exp(ln(sum)-ln(b)))
                    probObservations = j + Math.Log(1 + Math.Exp(probObservations - j));
                }
            }
            return probObservations;
        }

        #region Backward
        /// <summary>
        /// The Backward algorithm
        /// </summary>
        /// <param name="observation"></param>
        /// <returns>A table of beta pass</returns>
        public RArray getBackwardtable<T>(T[] observation)
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
        public double[,] backward<T>(T[] observation)
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
                        double temp = b[j, k + 1] + Math.Log(transProbs[i, j] * B(j, observation[k + 1]));
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
            return updateModel(new MyHMM(this.states, this.symbols, this.startProbs, transitionMatrix, emissionMatrix));
        }

        public bool updateModel(MyHMM newModel)
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

        public MyHMM baumWelch<T>(T[] observations)
        {
            return baumWelch(observations, 100);
        }

        public MyHMM baumWelch<T>(T[] observation, int maxIterations)
        {
            //double delta = Math.Pow(10, -9);
            return baumWelch(observation, maxIterations, 1E-9, new OneTimeFinish((i) => { }));
        }

        public MyHMM baumWelch<T>(T[] observation, OneTimeFinish doSomething)
        {
            return baumWelch(observation, 100, doSomething);
        }

        public MyHMM baumWelch<T>(T[] observation, int maxIterations, OneTimeFinish doSomething)
        {
            //double delta = Math.Pow(10, -9);
            return baumWelch(observation, maxIterations, 1E-9, doSomething);
        }

        public MyHMM baumWelch<T>(T[] observation, int maxIterations, double delta, OneTimeFinish doSomething)
        {
            MyHMM tempHMM = this;
            List<double> diff = new List<double>();
            for (int i = 0; i < maxIterations; i++)
            {
                MyHMM bw = baumWelchRecursion(tempHMM, observation);
                double[,] newTransMatrix = bw.TransProbs;
                double[,] newEmissionMatrix = bw.EmissionProbs;
                // 没懂pseudo Count是干嘛的
                newTransMatrix = normalizeMatrx(newTransMatrix);
                if (observation is int[])
                {//如果是连续的观测值，就不需要normalize
                    newEmissionMatrix = normalizeMatrx(newEmissionMatrix);
                }
                double d = diffMatrix(tempHMM.TransProbs, newTransMatrix) + diffMatrix(tempHMM.EmissionProbs, newEmissionMatrix);
                diff.Add(d);
                if (!tempHMM.updateModel(newTransMatrix, newEmissionMatrix)) { Console.WriteLine("Error"); }
                doSomething(i + 1);
                if (d < delta)
                {
                    break;
                }
            }
            return tempHMM;
        }

        public MyHMM baumWelchRecursion<T>(MyHMM hmm, T[] observation)
        {
            //计算前向概率和后向概率
            double[,] forward = hmm.forward(observation);
            double[,] backward = hmm.backward(observation);

            //算概率P(O|\lambda)
            double probObservations = hmm.getProbObs(forward);
        
            //更新“状态转移矩阵”
            double[,] transitionMatrx = ReEstimateTransProbs(hmm, probObservations, forward, backward, observation);

            //更新“发射矩阵”
            double[,] emissionMatrx = new double[hmm.NumStates, hmm.NumSymbols];
            if (observation is int[] O)
            {//离散观测值的更新 Emission Matrix
                emissionMatrx = ReEstimateEmissionProbs_ForDiscreteObs(hmm, probObservations, forward, backward, O);
            }
            else if (observation is double[] Obs)
            {//连续观测值 更新正态分布的参数
                emissionMatrx = ReEstimateEmissionProbs_ForContinuousObs(hmm, probObservations, forward, backward, Obs);
            }

            return new MyHMM(hmm.States, hmm.Symbols, hmm.StartProbs, transitionMatrx, emissionMatrx);
        }

        double[,] ReEstimateTransProbs<T>(MyHMM hmm, double probObservations, double[,] forward, double[,] backward, T[] observation)
        {
            double[,] newTransMatrix = new double[hmm.NumStates, hmm.NumStates];
            for (int x = 0; x < hmm.NumStates; x++)
            {
                for (int y = 0; y < hmm.NumStates; y++)
                {
                    //只计算\xi_t(i,j)，然后再normalise
                    double temp = forward[x, 0] + Math.Log(hmm.TransProbs[x, y]) +
                        Math.Log(hmm.B(y, observation[0 + 1])) + backward[y, 0 + 1];
                    for (int i = 1; i < observation.Length - 1; i++)
                    {
                        double j = forward[x, i] + Math.Log(hmm.TransProbs[x, y]) +
                            Math.Log(hmm.B(y, observation[i + 1])) + backward[y, i + 1];
                        if (j > -Inf)
                        {
                            temp = j + Math.Log(1 + Math.Exp(temp - j));
                        }
                    }
                    temp = Math.Exp(temp - probObservations);
                    newTransMatrix[x, y] = temp;
                }
            }
            return newTransMatrix;
        }

        double[,] ReEstimateEmissionProbs_ForDiscreteObs(MyHMM hmm, double probObservations, double[,] forward, double[,] backward, int[] observation)
        {
            double[,] newEmissionMatrix = new double[hmm.NumStates, hmm.NumSymbols];
            for (int x = 0; x < hmm.NumStates; x++)
            {
                for (int k = 0; k < hmm.NumSymbols; k++)
                {
                    //同理，只计算\gamma_t{i}，然后再normalise
                    double temp = -Inf;
                    for (int t = 0; t < observation.Length; t++)
                    {
                        if (k == observation[t])
                        {
                            double j = forward[x, t] + backward[x, t];
                            if (j > -Inf)
                            {
                                temp = j + Math.Log(1 + Math.Exp(temp - j));
                            }
                        }
                    }
                    temp = Math.Exp(temp - probObservations);
                    newEmissionMatrix[x, k] = temp;
                }
            }
            return newEmissionMatrix;
        }

        double[,] ReEstimateEmissionProbs_ForContinuousObs(MyHMM hmm, double probObservations, double[,] forward, double[,] backward, double[] observation)
        {
            double[,] newEmissionMatrix = new double[hmm.NumStates, hmm.NumSymbols];
            for (int x = 0; x < hmm.NumStates; x++)
            {
                double logMean = -Inf;
                double logVar = -Inf;
                double denom = -Inf;

                //calculate gamma
                for (int t = 0; t < observation.Length; t++)
                {
                    double temp = forward[x, t] + backward[x, t];
                    denom = temp + Math.Log(1 + Math.Exp(denom - temp));
                    double tempLogObs = Math.Log(observation[t]);
                    temp += tempLogObs;
                    logMean = temp + Math.Log(1 + Math.Exp(logMean - temp));
                    temp += tempLogObs;
                    logVar = temp + Math.Log(1 + Math.Exp(logVar - temp));
                }
                logMean = logMean - denom;
                logVar = logVar - denom;
                //更新数据
                newEmissionMatrix[x, 0] = Math.Exp(logMean); //更新期望
                newEmissionMatrix[x, 1] = Math.Exp(logVar) - Math.Exp(2 * logMean); //更新标准差
            }
            return newEmissionMatrix;
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
        public RArray getViterbiPath<T>(T[] observation)
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

        public int[] viterbi<T>(T[] observation)
        {
            int numObservations = observation.Length;

            double[,] v = new double[numStates, numObservations];
            // Init
            for (int i = 0; i < numStates; i++)
            {
                v[i, 0] = Math.Log(startProbs[i] * B(i, observation[0]));
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
                    v[i, k] = Math.Log(B(i, observation[k])) + maxi;
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
