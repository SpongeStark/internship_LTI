# <div id="delegate">`OneTimeFinish` 委托</div>

- ### 完整签名
  ```csharp
  public delegate void OneTimeFinish(int time);
  ```
- ### 说明
  这是一个传入值为`int`，返回值为`void`的函数委托。

# `HMM` 类

## 简要总结

### <div id="attribute">字段（私有）</div>

| 字段名          | 字段类型   | 描述                         |
| --------------- | ---------- | ---------------------------- |
| `numStates`     | `int`      | 状态数（隐藏状态的状态数量） |
| `numSymbols`    | `int`      | 观测状态数                   |
| `states`        | `string[]` | 隐状态名称                   |
| `symbols`       | `string[]` | 观测状态名称                 |
| `startProbs`    | `double[]` | 初始状态概率矩阵             |
| `transProbs`    | `double[]` | 隐状态的状态转移矩阵         |
| `emissionProbs` | `double[]` | 观测状态的发射矩阵           |

### <div id="property">属性（公有）</div>

| 属性名                  | 属性类型   | 描述                         |
| ----------------------- | ---------- | ---------------------------- |
| `NumStates { get }`     | `int`      | 状态数（隐藏状态的状态数量） |
| `NumSymbols { get }`    | `int`      | 观测状态数                   |
| `States { get }`        | `string[]` | 隐状态名称                   |
| `Symbols { get }`       | `string[]` | 观测状态名称                 |
| `StartProbs { get }`    | `double[]` | 初始状态概率矩阵             |
| `TransProbs { get }`    | `double[]` | 隐状态的状态转移矩阵         |
| `EmissionProbs { get }` | `double[]` | 观测状态的发射矩阵           |

### <div id="constructor">构造方法</div>

| 方法名                  | 传入值                                                                                                                      |
| ----------------------- | --------------------------------------------------------------------------------------------------------------------------- |
| [`HMM`](#constructor-1) | `int numStates,`<br>`int numSymbols`                                                                                        |
| [`HMM`](#constructor-2) | `string[] states,`<br>`string[] symbols`                                                                                    |
| [`HMM`](#constructor-3) | `string[] states,`<br>`string[] symbols,`<br>`double[] startProbs`                                                          |
| [`HMM`](#constructor-4) | `string[] states,`<br>`string[] symbols,`<br>`double[] startProbs,`<br>`double[,] transProbs,`<br>`double[,] emissionProbs` |

### <div id="public-method">公有方法</div>

| 方法名                         | 传入值                                                                               | 返回值类型            | 描述                                                                      |
| ------------------------------ | ------------------------------------------------------------------------------------ | --------------------- | ------------------------------------------------------------------------- |
| [`getForwardTable`](#pub-m-1)  | `int[] observations`                                                                 | [`RArray`](RArray.md) | 返回通过前向算法得到的 alpha-pass 的所有结果                              |
| [`getBackwordTable`](#pub-m-2) | `int[] observations`                                                                 | [`RArray`](RArray.md) | 返回通过前向算法得到的 bata-pass 的所有结果                               |
| [`baumWelch`](#pub-m-3)        | `int[] observations`                                                                 | `HMM`                 | 返回通过 Baum-Welch 算法得到的新 HMM                                      |
| [`baumWelch`](#pub-m-4)        | `int[] observations,`<br>`int maxIterations`                                         | `HMM`                 | 返回通过 Baum-Welch 算法得到的新 HMM                                      |
| [`baumWelch`](#pub-m-5)        | `int[] observations,`<br>`delegate doSth`                                            | `HMM`                 | 返回通过 Baum-Welch 算法得到的新 HMM，并在每次更新后，执行`doSth`委托实例 |
| [`baumWelch`](#pub-m-6)        | `int[] observations,`<br>`int maxIterations,`<br>`delegate doSth`                    | `HMM`                 | 返回通过 Baum-Welch 算法得到的新 HMM，并在每次更新后，执行`doSth`委托实例 |
| [`baumWelch`](#pub-m-7)        | `int[] observations,`<br>`int maxIterations,`<br>`double delta,`<br>`delegate doSth` | `HMM`                 | 返回通过 Baum-Welch 算法得到的新 HMM，并在每次更新后，执行`doSth`委托实例 |
| [`getViterbiPath`](#pub-m-8)   | `int[] observations`                                                                 | [`RArray`](RArray.md) | 返回通过 Viterbi 算法得到的因状态序列                                     |
| [`ToString`](#pub-m-9)         | `void`                                                                               | `String`              | 重写父类的`ToString()`方法                                                |

### <div id="private-method">私有方法</div>

| 方法名                           | 传入值                                                       | 返回值类型  | 描述                                                   |
| -------------------------------- | ------------------------------------------------------------ | ----------- | ------------------------------------------------------ |
| [`forward`](#pri-m-1)            | `int[] observations`                                         | `double[,]` | 返回通过前向算法得到的 alpha-pass 的结果的 log 值      |
| [`backward`](#pri-m-2)           | `int[] observations`                                         | `double[,]` | 返回通过后向算法得到的 beta-pass 的结果的 log 值       |
| [`baumWelchRecursion`](#pri-m-3) | `HMM hmm,`<br>`int[] observations`                           | `HMM`       | Baum-Wemch 的一次更新，并返回结果                      |
| [`normalizeMatrx`](#pri-m-4)     | `double[,] matrix`                                           | `double[,]` | 整理传入的矩阵，使得每一行的和为 1，并返回整理后的矩阵 |
| [`updateModel`](#pri-m-5)        | `double[,] transitionMatrix,`<br> `double[,] emissionMatrix` | `bool`      | 更新转移矩阵和发射矩阵                                 |
| [`updateModel`](#pri-m-6)        | `HMM newHMM`                                                 | `bool`      | 更新转移矩阵和发射矩阵                                 |
| [`diffMatrix`](#pri-m-7)         | `double[,] matrix1,`<br> `double[,] matrix2`                 | `double`    | 计算两个矩阵的差                                       |
| [`viterbi`](#pri-m-8)            | `int[] observations`                                         | `int[]`     | 执行 Viterbi 算法，并返回隐状态序列的索引值            |

---

## 详细描述

---

### <code id="constructor-1">HMM(int,int)</code>

- #### 完整函数签名
  ```csharp
  public HMM(int numStates, int numSymbols)
  ```
- #### 详细描述
  实例化一个默认的 HMM，其中初始状态矩阵和转移矩阵的每一个值都等于 1/numStates，发射矩阵的每一个值都等于 1/numSymbols.
- #### 参数
  - `numStates` : 隐状态的数量
  - `numSymbols` : 观测状态的数量
- #### 🌰 使用案例
  ```csharp
  HMM hmm = new HMM(2,3);
  ```

返回到 [**构造方法**](#constructor)

---

### <code id="constructor-2">HMM(string[],string[])</code>

- #### 完整函数签名
  ```csharp
  public HMM(string[] states, string[] symbols)
  ```
- #### 详细描述
  实例化一个默认的 HMM，其中初始状态矩阵和转移矩阵的每一个值都等于 1/numStates，发射矩阵的每一个值都等于 1/numSymbols.
- #### 参数
  - `states` : 隐状态的名称
  - `symbols` : 观测状态的名称
- #### 🌰 使用案例
  ```csharp
  string[] states = new string[] { "q0", "q1" };
  string[] symbols = new string[] { "v0", "v1", "v2" };
  HMM hmm = new HMM(states, symbols);
  ```

返回到 [**构造方法**](#constructor)

---

### <code id="constructor-3">HMM(string[],string[],double[])</code>

- #### 完整函数签名
  ```csharp
  public HMM(string[] states, string[] symbols, double[] startProbs)
  ```
- #### 详细描述
  实例化一个默认的 HMM，给初始状态矩阵赋值，转移矩阵的每一个值都等于 1/numStates，发射矩阵的每一个值都等于 1/numSymbols.
- #### 参数
  - `states` : 隐状态的名称
  - `symbols` : 观测状态的名称
  - `startProbs` : 初始状态矩阵
- #### 🌰 使用案例
  ```csharp
  string[] states = new string[] { "q0", "q1" };
  string[] symbols = new string[] { "v0", "v1", "v2" };
  double[] startProb = new double[] { 0.3, 0.7 };
  HMM hmm = new HMM(states, symbols, startProb);
  ```

返回到 [**构造方法**](#constructor)

---

### <code id="constructor-4">HMM(string[],string[],double[],double[,],double[,])</code>

- #### 完整函数签名
  ```csharp
  public HMM(string[] states, string[] symbols, double[] startProbs, double[,] transProbs, double[,] emissionProbs)
  ```
- #### 详细描述
  实例化一个默认的 HMM，并分别给初始状态矩阵，转移矩阵和发射矩阵赋值。
- #### 参数
  - `states` : 隐状态的名称
  - `symbols` : 观测状态的名称
  - `startProbs` : 初始状态矩阵
  - `transProbs` : 隐状态转移矩阵
  - `emissionProbs` : 观测状态发射矩阵
- #### 🌰 使用案例
  ```csharp
  string[] states = new string[] { "q0", "q1" };
  string[] symbols = new string[] { "v0", "v1", "v2" };
  double[] startProbs = new double[] { 0.3, 0.7 };
  double[,] transProbs = new double[,] {
    new double[] { 0.4, 0.6 },
    new double[] { 0.75, 0.25 }
  };
  double[,] emissionProbs = new double[,] {
    new double[] { 0.3, 0.3, 0.4 },
    new double[] { 0.35, 0.25, 0.4 }
  };
  HMM hmm = new HMM(states, symbols, startProbs, transProbs, emissionProbs);
  ```

返回到 [**构造方法**](#constructor)

---

### <code id="pub-m-1">getForwardTable(int[])</code>

- #### 完整函数签名
  ```csharp
  public RArray getForwardTable(int[] observation)
  ```
- #### 详细描述
  返回一个二维的 alpha-pass 表格（\alpha_t(i)）。
- #### 参数
  - `observation` : 离散的观测状态序列（从 0 开始）
- #### 🌰 使用案例
  ```csharp
  //构造HMM
  HMM hmm = new HMM(......);
  //观测序列
  int[] obs = new int[] { 0,1,0,2 };
  //执行前向算法
  RArray forwardTable = hmm.getForwardTable(obs);
  //将结果输出到命令行
  Console.WriteLine(forwardTable);
  ```

返回到 [**公有方法**](#public-method)

---

### <code id="pub-m-2">getBackwordTable(int[])</code>

- #### 完整函数签名
  ```csharp
  public RArray getBackwordTable(int[] observation)
  ```
- #### 详细描述
  返回一个二维的 beta-pass 表格（\beta(i)）。
- #### 参数
  - `observation` : 离散的观测状态序列（从 0 开始）
- #### 🌰 使用案例
  ```csharp
  //构造HMM
  HMM hmm = new HMM(......);
  //观测序列
  int[] obs = new int[] { 0,1,0,2 };
  //执行前向算法
  RArray backwordTable = hmm.getBackwordTable(obs);
  //将结果输出到命令行
  Console.WriteLine(backwordTable);
  ```

返回到 [**公有方法**](#public-method)

---

### <code id="pub-m-3">baumWelch(int[])</code>

- #### 完整函数签名
  ```csharp
  public HMM baumWelch(int[] observation)
  ```
- #### 详细描述
  返回执行 Baum-Welch 算法之后的 HMM 实例。默认最高执行 100 次，差距小于 1e-9 则停止。
- #### 参数
  - `observation` : 离散的观测状态序列（从 0 开始）
- #### 🌰 使用案例
  ```csharp
  //构造HMM
  HMM hmm = new HMM(......);
  //观测序列（理论上需要非常多的观测值，该算法在比较准确，此处只是举例）
  int[] obs = new int[] { 0,1,0,2 };
  //执行Baum-Welch算法
  HMM newHMM = hmm.baumWelch(obs);
  //将结果输出到命令行
  Console.WriteLine(HMM);
  ```

返回到 [**公有方法**](#public-method)

---

### <code id="pub-m-4">baumWelch(int[],int)</code>

- #### 完整函数签名
  ```csharp
  public HMM baumWelch(int[] observation, int maxIterations)
  ```
- #### 详细描述
  返回执行 Baum-Welch 算法之后的 HMM 实例。最高执行`maxIterations`次，默认差距小于 1e-9 则停止。
- #### 参数
  - `observation` : 离散的观测状态序列（从 0 开始）
  - `maxIterations` : 最高循环次数
- #### 🌰 使用案例
  ```csharp
  //构造HMM
  HMM hmm = new HMM(......);
  //观测序列（理论上需要非常多的观测值，该算法在比较准确，此处只是举例）
  int[] obs = new int[] { 0,1,0,2 };
  //执行Baum-Welch算法（最多更新999次）
  HMM newHMM = hmm.baumWelch(obs, 999);
  //将结果输出到命令行
  Console.WriteLine(HMM);
  ```

返回到 [**公有方法**](#public-method)

---

### <code id="pub-m-5">baumWelch(int[],delegate)</code>

- #### 完整函数签名
  ```csharp
  public HMM baumWelch(int[] observation, OneTimeFinish doSomething)
  ```
- #### 详细描述
  返回执行 Baum-Welch 算法之后的 HMM 实例。最高执行 100 次，默认差距小于 1e-9 则停止。并在每一次更新 HMM 后，执行`doSomething`委托实例。
- #### 参数
  - `observation` : 离散的观测状态序列（从 0 开始）
  - `doSomething` : 每次更新后执行的[`OneTimeFinish`委托](#delegate)实例
- #### 🌰 使用案例
  ```csharp
  //构造HMM
  HMM hmm = new HMM(......);
  //观测序列（理论上需要非常多的观测值，该算法在比较准确，此处只是举例）
  int[] obs = new int[] { 0,1,0,2 };
  OneTimeFinish myDelegate = new OneTimeFinish(
    (i) => { Console.WriteLine(i + "times"); }
  );
  //执行Baum-Welch算法（每次执行完都会打印出当前的循环次数）
  HMM newHMM = hmm.baumWelch(obs, myDelegate);
  //将结果输出到命令行
  Console.WriteLine(HMM);
  ```

返回到 [**公有方法**](#public-method)

---

### <code id="pub-m-6">baumWelch(int[],int,delegate)</code>

- #### 完整函数签名
  ```csharp
  public HMM baumWelch(int[] observation, int maxIterations, OneTimeFinish doSomething)
  ```
- #### 详细描述
  返回执行 Baum-Welch 算法之后的 HMM 实例。最高执行`maxIterations`次，默认差距小于 1e-9 则停止。并在每一次更新 HMM 后，执行`doSomething`委托实例。
- #### 参数
  - `observation` : 离散的观测状态序列（从 0 开始）
  - `maxIterations` : 最高循环次数
  - `doSomething` : 每次更新后执行的[`OneTimeFinish`委托](#delegate)实例
- #### 🌰 使用案例
  ```csharp
  //构造HMM
  HMM hmm = new HMM(......);
  //观测序列（理论上需要非常多的观测值，该算法在比较准确，此处只是举例）
  int[] obs = new int[] { 0,1,0,2 };
  OneTimeFinish myDelegate = new OneTimeFinish(
    (i) => { Console.WriteLine(i + " times"); }
  );
  //执行Baum-Welch算法（最多更新999次，每次执行完都会打印出当前的循环次数）
  HMM newHMM = hmm.baumWelch(obs, 999, myDelegate);
  //将结果输出到命令行
  Console.WriteLine(HMM);
  ```

返回到 [**公有方法**](#public-method)

---

### <code id="pub-m-7">baumWelch(int[],int,double,delegate)</code>

- #### 完整函数签名
  ```csharp
  public HMM baumWelch(int[] observation, int maxIterations, double delta, OneTimeFinish doSomething)
  ```
- #### 详细描述
  返回执行 Baum-Welch 算法之后的 HMM 实例。最高执行`maxIterations`次，默认差距小于 `delta` 则停止。并在每一次更新 HMM 后，执行`doSomething`委托实例。
- #### 参数
  - `observation` : 离散的观测状态序列（从 0 开始）
  - `maxIterations` : 最高循环次数
  - `delta` : 最小收敛差距
  - `doSomething` : 每次更新后执行的[`OneTimeFinish`委托](#delegate)实例
- #### 🌰 使用案例
  ```csharp
  //构造HMM
  HMM hmm = new HMM(......);
  //观测序列（理论上需要非常多的观测值，该算法在比较准确，此处只是举例）
  int[] obs = new int[] { 0,1,0,2 };
  OneTimeFinish myDelegate = new OneTimeFinish(
    (i) => { Console.WriteLine(i + " times"); }
  );
  //执行Baum-Welch算法（最多更新999次，每次执行完都会打印出当前的循环次数。当差距小于1e-10就停止）
  HMM newHMM = hmm.baumWelch(obs, 999, 1e-10, myDelegate);
  //将结果输出到命令行
  Console.WriteLine(HMM);
  ```

返回到 [**公有方法**](#public-method)

---

### <code id="pub-m-8">getViterbiPath(int[])</code>

- #### 完整函数签名
  ```csharp
  public RArray getViterbiPath(int[] observation)
  ```
- #### 详细描述
  根据该`HMM`实例，通过 Viterbi 算法，得到隐状态序列。
- #### 参数
  - `observation` : 离散的观测状态序列（从 0 开始）
- #### 🌰 使用案例
  ```csharp
  //构造HMM
  HMM hmm = new HMM(......);
  //观测序列
  int[] obs = new int[] { 0,1,0,2 };
  //执行Viterbi算法
  RArray hidenStates = hmm.getViterbiPath(obs);
  //将结果输出到命令行
  Console.WriteLine(hidenStates);
  ```

返回到 [**公有方法**](#public-method)

---

### <code id="pub-m-9">ToString()</code>

- #### 完整函数签名
  ```csharp
  public override String ToString()
  ```
- #### 详细描述
  输出该`HMM`实例的全部信息。
- #### 🌰 使用案例
  ```csharp
  //构造HMM
  HMM hmm = new HMM(......);
  //将结果输出到命令行
  Console.WriteLine(hmm);
  ```

返回到 [**公有方法**](#public-method)

---

### <code id="pri-m-1">forward(int[])</code>

- #### 完整函数签名
  ```csharp
  double[,] forward(int[] observation)
  ```
- #### 详细描述
  计算前向算法，返回其 log 值。
- 服务于: [`getForwardTable`](#pub-m-1)。
- #### 参数
  - `observation` : 离散的观测状态序列（从 0 开始）

返回到 [**私有方法**](#private-method)

---

### <code id="pri-m-2">backward(int[])</code>

- #### 完整函数签名
  ```csharp
  double[,] backward(int[] observation)
  ```
- #### 详细描述
  计算后向算法，返回其 log 值。
- 服务于: [`getBackwardTable`](#pub-m-2)。
- #### 参数
  - `observation` : 离散的观测状态序列（从 0 开始）

返回到 [**私有方法**](#private-method)

---

### <code id="pri-m-3">baumWelchRecursion(HMM,int[])</code>

- #### 完整函数签名
  ```csharp
  HMM baumWelchRecursion(HMM hmm, int[] observation)
  ```
- #### 详细描述
  执行 Baum-Welch 算法，进行一次更新。由原来的`HMM`实例，得到更新的`HMM`实例。
- 服务于: [`baumWelch`](#pub-m-7)。
- #### 参数
  - `hmm` : 原 HMM 实例
  - `observation` : 离散的观测状态序列（从 0 开始）

返回到 [**私有方法**](#private-method)

---

### <code id="pri-m-4">normalizeMatrx(double[,])</code>

- #### 完整函数签名
  ```csharp
  double[,] normalizeMatrx(double[,] matrix)
  ```
- #### 详细描述
  将一个矩阵一般化，使得每行的值的和，为 1.
- 服务于: [`baumWelch`](#pub-m-7)。
- #### 参数
  - `matrix` : 需要一般化的矩阵

返回到 [**私有方法**](#private-method)

---

### <code id="pri-m-5">diffMatrix(double[,],double[,])</code>

- #### 完整函数签名
  ```csharp
  bool updateModel(double[,] transitionMatrix, double[,] emissionMatrix)
  ```
- #### 详细描述
  更新本`HMM`实例的转移矩阵和发射矩阵。
- 服务于: [`baumWelch`](#pub-m-7)。
- #### 参数
  - `transitionMatrix` : 新的隐状态转移矩阵
  - `emissionMatrix` : 新的观测值发射矩阵

返回到 [**私有方法**](#private-method)

---

### <code id="pri-m-6">diffMatrix(double[,],double[,])</code>

- #### 完整函数签名
  ```csharp
  bool updateModel(HMM newModel)
  ```
- #### 详细描述
  更新本`HMM`实例的转移矩阵和发射矩阵。
- 服务于: [`baumWelch`](#pub-m-7)。
- #### 参数
  - `newHMM` : 新的`HMM`实例

返回到 [**私有方法**](#private-method)

---

### <code id="pri-m-7">diffMatrix(double[,],double[,])</code>

- #### 完整函数签名
  ```csharp
  double diffMatrix(double[,] T1, double[,] T2)
  ```
- #### 详细描述
  计算两个矩阵之间的差距（每个对应的值之间的差的平方和）。如果两个矩阵的维数不相同，则返回-1.
- 服务于: [`baumWelch`](#pub-m-7)。
- #### 参数
  - `T1` : 矩阵
  - `T2` : 矩阵

返回到 [**私有方法**](#private-method)

---

### <code id="pri-m-8">viterbi(int[])</code>

- #### 完整函数签名
  ```csharp
  int[] viterbi(int[] observation)
  ```
- #### 详细描述
  执行 Viterbi 算法，通过观测值序列，得到隐状态的索引值。
- 服务于: [`getViterbiPath`](#pub-m-8)。
- #### 参数
  - `observation` : 离散的观测状态序列（从 0 开始）

返回到 [**私有方法**](#private-method)

---
