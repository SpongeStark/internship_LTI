# library(HMM)
source("HMM.r");

state = c("Hot","Cold")
symbol = c("Small","Medium","Large")

Pi = c(0.4,0.6)
A = matrix(c(0.3,0.7, 0.4, 0.6),nrow=2, byrow=TRUE)
B = matrix(c(0.3,0.3,0.4, 0.2,0.5,0.3),nrow=2, byrow=TRUE)

hmm <- initHMM(
  States = state, 
  Symbols = symbol, 
  startProbs = Pi, 
  transProbs = A, 
  emissionProbs = B);

## Set the observations for Baum-Welch algo
data = read.csv("data/testData.csv", header=TRUE, sep=",");
O = data$observations
observations = c()
for(index in O){
  observations = c(observations, symbol[index])
}

## Baum-Welch
bw = baumWelch(hmm,observations,999)
print(bw$hmm)
# the differences
diff = bw$difference
end = length(diff)
start = end-10
print(diff[start:end])