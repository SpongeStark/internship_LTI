# library(HMM)
source("HMM.r");

state = c("Hot","Cold")
symbol = c("Small","Medium","Large")

A = read.csv("matrix_A.csv", header=TRUE, sep=",");
A = as.matrix(A)
B = read.csv("matrix_B.csv", header=TRUE, sep=",");
B = as.matrix(B)
Pi = read.csv("pi.csv", header=TRUE, sep=",");
Pi = unlist(Pi)

## initialize the model
hmm <- initHMM(
  States = state, 
  Symbols = symbol, 
  startProbs = Pi, 
  transProbs = A, 
  emissionProbs = B);
# print(hmm)

## Set the observations for F/B algo
O = read.csv("data/testDataO.csv", header=TRUE, sep=",");
observations = c()
for(index in O){
  observations = c(observations, symbol[index])
}

## Calculate backward probablities
logBackwardProbabilities = backward(hmm,observations) 
print(exp(logBackwardProbabilities))

## Calculate forward probablities
logForwardProbabilities = forward(hmm,observations)
print(exp(logForwardProbabilities))

## Baum-Welch Algotithm
bw = baumWelch(hmm,observations)
print(bw$hmm)

## Viterbi Algorithm
viterbi = viterbi(hmm,observations)
print(viterbi)