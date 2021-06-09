# 'MyHMM' 类

## 概述

该类与“HMM”类大致相同，只是为了适应离散观测值而做了少许的改动。因为该类还没有测试，所以目前先独立出来。

得益于范型，我们可以传入 int 型或者 double 型数组来执行相应算法。如果将 int 型的数组作为观测值传入方法，将执行离散观测值的 HMM 的算法。这与原 HMM 相同。如果将 double 型数组作为观测值传入方法，将执行连续观测值的 HMM 算法。此时，发射矩阵需存储正态分布的参数。每一行为一个状态，每行的第一列为平均值 mu，第二列为方差 sigma^2。所以，为了获得隐状态到观测状态的概率，该类更新了两个新方法：

| name                     | input                                         | output type | description         |
| ------------------------ | --------------------------------------------- | ----------- | ------------------- |
| `B<T>`                   | `int i,`<br>`T[] Obs`                         | `double`    | [detail](#method-1) |
| `normalDistributionProb` | `double mu,`<br>`double sigma,`<br>`double X` | `double`    | [detail](#method-2) |

该类还对 Baum-Welch 算法的代码结构进行了部分调整，把 “计算最终概率”、“对状态转移矩阵重新估值” 和 “对转移矩阵重新估值” 的部分 分了出来，更新了四个新方法。

| name                                          | input                                                                                     | output type | description         |
| --------------------------------------------- | ----------------------------------------------------------------------------------------- | ----------- | ------------------- |
| getProbObs                                    | `double[,] LogForward`                                                                    | `double`    | [detail](#method-3) |
| ReEstimateTransProbs\<T>                      | `MyHMM hmm,`<br>`double probObs,`<br>`double forward,`<br>`double backward,`<br>`T[] obs` | `double[,]` | [detail](#method-4) |
| ReEstimateEmissionProbs<br>\_ForDiscreteObs   | `MyHMM hmm,`<br>`double probObs,`<br>`double forward,`<br>`double backward,`<br>`T[] obs` | `double[,]` | [detail](#method-5) |
| ReEstimateEmissionProbs<br>\_ForContinuousObs | `MyHMM hmm,`<br>`double probObs,`<br>`double forward,`<br>`double backward,`<br>`T[] obs` | `double[,]` | [detail](#method-6) |

## 详细内容

---

### <code id="method-1">B(int,T)</code>

- #### 完整签名
  ```csharp
  public double B<T>(int i, T k)
  ```
- #### 详细描述
  根据观测值`k`的类型，返回指定的概率。  
  如果`T`是`int`，则视为离散观测值，返回`b_{i}(k)`。  
  如果`T`是`double`，则视为连续观测值，取`b_{i}(0)`为平均值，取`b_{i}(1)`为方差，返回 X=k 时，该正态分布的概率。
- #### 参数
  - `i` : 状态索引
  - `k` : 观测值

---

### <code id="method-2">normalDistributionProb(double,double,double)</code>

- #### 完整签名
  ```csharp
  private double normalDistributionProb(double mu, double sigma, double X)
  ```
- #### 详细描述
  计算正态分布概率。
- #### 参数
  - `mu` : 平均数
  - `sigma` : 标准差（方差开根号）
  - `X` : 随机变量的值

---

### <code id="method-3">getProbObs(double[,])</code>

- #### 完整签名
  ```csharp
  public double getProbObs(double[,] LogForward)
  ```
- #### 详细描述
  根据前向算法的结果，算概率 P(O|\lambda)。把最后的\alpha 全部加起来
- #### 参数
  - `LogForward` : 前向算法的结果

---

### <code id="method-4">ReEstimateTransProbs(MyHMM,double,double[,],double[,],T[])</code>

- #### 完整签名
  ```csharp
  double[,] ReEstimateTransProbs<T>(MyHMM hmm, double probObservations, double[,] forward, double[,] backward, T[] observation)
  ```
- #### 详细描述
  更新状态转移矩阵。
- #### 参数
  - `hmm` : 老一代的 HMM
  - `probObservations` : 最终概率
  - `forward` : 前向算法的结果
  - `backward` : 后向算法的结果
  - `observation` : 观测值序列

---

### <code id="method-5">ReEstimateEmissionProbs_ForDiscreteObs(MyHMM,double,double[,],double[,],int[])</code>

- #### 完整签名
  ```csharp
  double[,] ReEstimateEmissionProbs_ForDiscreteObs(MyHMM hmm, double probObservations, double[,] forward, double[,] backward, int[] observation)
  ```
- #### 详细描述
  更新离散观测值的发射矩阵。
- #### 参数
  - `hmm` : 老一代的 HMM
  - `probObservations` : 最终概率
  - `forward` : 前向算法的结果
  - `backward` : 后向算法的结果
  - `observation` : 离散的观测值序列

---

### <code id="method-6">ReEstimateEmissionProbs_ForContinuousObs(MyHMM,double,double[,],double[,],double[])</code>

- #### 完整签名
  ```csharp
  double[,] ReEstimateEmissionProbs_ForContinuousObs(MyHMM hmm, double probObservations, double[,] forward, double[,] backward, double[] observation)
  ```
- #### 详细描述
  更新连续观测值的发射矩阵。
- #### 参数
  - `hmm` : 老一代的 HMM
  - `probObservations` : 最终概率
  - `forward` : 前向算法的结果
  - `backward` : 后向算法的结果
  - `observation` : 连续的观测值序列

---
