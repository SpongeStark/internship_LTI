# Model 项目

这是根据R语言的HMM包的源代码，写出来的在C#平台计算隐马尔可夫模型的项目。

次项目包含四个文件，也就是四个类
- [`RArray`](RArray.md) : 用于一维或二维的，含有键值对的`double`类型的数组，并可通过键寻值
- [`RDataFrame`](RDataFrame.md) : 类似R语言的`DataFrame`，并加入了指针，用于对行的读取
- [`CsvHelper`](CsvHelper.md) : 用于读写`*.csv`文件，或者CSV文本格式的`*.txt`文件
- [`HMM`](HMM.md) : 隐马尔可夫模型及三个主要算法

另外还有一个[MyHMM](MyHMM.md)类，以便适应连续观测值变量。但是该类还未测试。