source("../HMM.r");

state = c("Hot","Cold")
symbol = c("Small","Medium","Large")

A = read.csv("../matrix_A.csv", header=TRUE, sep=",");
A = as.matrix(A)
B = read.csv("../matrix_B.csv", header=TRUE, sep=",");
B = as.matrix(B)
Pi = read.csv("../pi.csv", header=TRUE, sep=",");
Pi = unlist(Pi)

## initialize the model
hmm <- initHMM(
  States = state, 
  Symbols = symbol, 
  startProbs = Pi, 
  transProbs = A, 
  emissionProbs = B
);

result_simulation = simHMM(hmm,100);

obs = result_simulation$observation;

O = c()
for(i in obs){
  if(i == symbol[1]){O = c(O,0)}
  else if(i == symbol[2]) {O = c(O,1)}
  else {O = c(O,2)}
}

testData = data.frame(observations=O)
write.csv(testData,"testDataObs100.csv", row.names=FALSE);