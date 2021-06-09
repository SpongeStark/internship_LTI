# library(HMM)
source("HMM/R/HMM.r");

# Initialise HMM
hmm = initHMM(c("A","B"), c("L","R"), transProbs=matrix(c(.8,.2,.2,.8),2), emissionProbs=matrix(c(.6,.4,.4,.6),2))
print(hmm)
# Sequence of observations
observations = c("L","L","R","R")
# Calculate forward probablities
logForwardProbabilities = forward(hmm,observations) 
print(exp(logForwardProbabilities))















#  ____                     _
# |  __|                   | |
# | |__   _    _    _____  | | __
# |  __| | |  | |  /  ___| | |/ /
# | |    | |__| | |  (___  |   <
# |_|     \___,_|  \_____| |_|\_\
