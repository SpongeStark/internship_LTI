state = c("Hot","Cold")
symbol = c("Small","Medium","Large")

A = read.csv("transProbs.csv", header=TRUE, sep=",");
A = as.matrix(A)
B = read.csv("emission.csv", header=TRUE, sep=",");
B = as.matrix(B)
mean = B[,1]
sd = B[,2]
Pi = read.csv("startProbs.csv", header=TRUE, sep=",");
Pi = unlist(Pi)

X = c()
O = c()

T = 1000

randomNumbers = runif(T,0,1)

if(randomNumbers[1] < Pi[1]){
  X[1] = 1;
  O[1] = rnorm(1,mean[1],sd[1])
}else{
  X[1] = 2;
  O[1] = rnorm(1,mean[2],sd[2])
}

for(i in 2:T){
  probsA = A[X[i-1],];
  if(randomNumbers[i] < probsA[1]){
    X[i] = 1;
    O[i] = rnorm(1,mean[1],sd[1])
  }else{
    X[i] = 2;
    O[i] = rnorm(1,mean[2],sd[2])
  }
}

X = X-1;
# O = O-1;
testData = data.frame(states=X, observations=O)
# testData = data.frame(observations=O)
write.csv(testData,"testDataObsCon1000.csv", row.names=FALSE);
