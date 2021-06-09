#ifndef HMM_H
#define HMM_H

#include<stdio.h>
typedef struct{
  int numStates;
  int numnSymbols;
  double* startProbs;
  double** transProbs;
  double** emissionProbs;
} HMM;

#endif // HMM_H