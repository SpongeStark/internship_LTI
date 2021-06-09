#include"HMM.h"
#include <math.h>

HMM initHMM(int numStates, int numnSymbols, 
  double* startProbs, double** transProbs, double** emissionProbs)
{
  HMM hmm;
  hmm.numStates = numStates;
  hmm.numnSymbols = numnSymbols;
  for(int i=0; i<numStates; i++)
  {
    hmm.startProbs[i] = startProbs[i];
    for(int j=0; j<numStates; j++)
    {
      hmm.transProbs[i][j] = transProbs[i][j];
    }
    for(int j=0; j<numnSymbols; j++)
    {
      hmm.emissionProbs[i][j] = emissionProbs[i][j];
    }
  }
  return hmm;
}

double** forward(HMM* hmm, int* observation, int numObservation)
{
  double** result;
  // Init
  for(int i=0; i<hmm->numStates; i++)
  {
    result[i][1] = log(hmm->startProbs[i] * hmm->emissionProbs[i][observation[1]]);
  }
  // Iteration
  for(int k=1; k<numObservation; k++)
  {
    for(int i=0; i<hmm->numStates; i++)
    {
      double logSum = -99999;//-__inf();
      for(int previousState=0; previousState < hmm->numStates; previousState++)
      {
        double temp = result[previousState][k-1] + log(hmm->transProbs[previousState][i]);
        if(temp > -99999 /*-__inf()*/)
        {
          logSum = temp + log(1 + exp(logSum - temp));
        }
      }
      result[i][k] = log(hmm->emissionProbs[i][observation[k]]) + logSum;
    }
  }
  return result;
}

int main(void)
{
  int N = 2;
  int M = 3;
  double *Pi;
  double **A;
  double **B;

  int *O;

  Pi[0] = 0.0;
  Pi[1] = 1.0;

  A[0][0] = 0.7;
  A[0][1] = 0.3;
  A[1][0] = 0.4;
  A[1][1] = 0.6;

  B[0][0] = 0.1;
  B[0][1] = 0.4;
  B[0][2] = 0.5;
  B[1][0] = 0.7;
  B[1][1] = 0.2;
  B[1][2] = 0.1;

  HMM * hmm;
  *hmm = initHMM(N,M,Pi,A,B);

  O[0] = 0;
  O[1] = 1;
  O[2] = 2;

  double** result = forward(hmm, O, 4);

  for(int i=0; i<N; i++)
  {
    for(int j=0; j<M; j++)
    {
      printf(" %.4f ",result[i][j]);
    }
    printf("\n");
  }
  return 0;
}