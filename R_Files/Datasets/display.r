fileName = "2015/salon-2015-oct-dec.csv";

data = read.csv(fileName, header=TRUE, sep=";");

# noise = data$Noise;
noise = data$Noise[1:599];

# plot(noise, type="l");
barplot(noise);
