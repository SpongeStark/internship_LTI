data = read.csv("testDataObsCon1000.csv", header=TRUE, sep=",")

obs1 = c()
obs2 = c()

for(i in 1:1000){
  if(data$states[i]==0){
    obs1 = c(obs1, data$observations[i])
  }else{
    obs2 = c(obs2, data$observations[i])
  }
}

print(mean(obs1))
print(sd(obs1))
print(mean(obs2))
print(sd(obs2))