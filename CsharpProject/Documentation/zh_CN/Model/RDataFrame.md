# `RDataFrame` 类

## 简要总结

### <div id="attribute">字段（私有）</div>

| 字段名     | 字段类型       | 描述                         |
| ---------- | -------------- | ---------------------------- |
| `nCol`     | `int`          | 列数（每一列是一个数据序列） |
| `size`     | `int`          | 数据序列的长度               |
| `colNames` | `string[]`     | 每一个数据序列的名称         |
| `data`     | `List<string>` | 数据                         |
| `pointer`  | `int`          | 游标（用于读取行）           |

### <div id="property">属性（公有）</div>

| 属性名           | 属性类型   | 描述           |
| ---------------- | ---------- | -------------- |
| `NumCol { get }` | `int`      | 列数           |
| `Size { get }`   | `int`      | 数据序列的长度 |
| `Title { get }`  | `string[]` | 数据序列的名称 |

### <div id="constructor">构造方法</div>

| 方法名                         | 传入值                        | 描述                                                        |
| ------------------------------ | ----------------------------- | ----------------------------------------------------------- |
| [`RDataFrame`](#constructor-1) | `int numCloumns`              | 初始化一个有`numCloumns`个数据序列的`RDataFrame`实例        |
| [`RDataFrame`](#constructor-2) | `params string[] columnNames` | 初始化一个列名称为`columnNames`个数据序列的`RDataFrame`实例 |

### <div id="public-method">公有方法</div>

| 方法名                          | 传入值                 | 返回值类型  | 描述                                                 |
| ------------------------------- | ---------------------- | ----------- | ---------------------------------------------------- |
| [`moveToFirstLine`](#pub-m-1)   | `void`                 | `void`      | 将游标移动到数据序列的第一行                         |
| [`moveToTitle`](#pub-m-2)       | `void`                 | `void`      | 将游标移动到标题行                                   |
| [`moveTo`](#pub-m-3)            | `int index`            | `string`    | 将游标移动到`index`行                                |
| [`hasNextLine`](#pub-m-4)       | `void`                 | `bool`      | 检查游标是否移动到了最后一行                         |
| [`getColumn`](#pub-m-5)         | `string colName`       | `string[]`  | 获得`colName`所指定的那一列数据序列                  |
| [`getColumn`](#pub-m-6)         | `int colIndex`         | `string[]`  | 获得`colIndex`所指定的那一列数据序列                 |
| [`getLine`](#pub-m-7)           | `void`                 | `string[]`  | 获得当前游标所指定的行，并将游标向后移动一行         |
| [`getLine`](#pub-m-8)           | `int rowIndex`         | `string[]`  | 获得`rowIndex`所指定的行，并将游标移动到此行的后一行 |
| [`toMatrix`](#pub-m-9)          | `void`                 | `string[,]` | 返回该`RDataFrame`实例所对应的矩阵                   |
| [`toDoubleVector`](#pub-m-10)   | `void`                 | `string[]`  | 返回该`RDataFrame`实例所对应的向量                   |
| [`addLine`](#pub-m-11)          | `params string[] args` | `void`      | 添加一行数据                                         |
| [`ToString`](#pub-m-12)         | `void`                 | `string`    | 默认调用`ToStringByColumn()`                         |
| [`ToStringByLine`](#pub-m-13)   | `void`                 | `string`    | 将每个数据序列，以行的形式输出                       |
| [`ToStringByLine`](#pub-m-14)   | `int first`            | `string`    | 将每个数据序列的前`first`个数据，以行的形式输出      |
| [`ToStringByColumn`](#pub-m-15) | `int `                 | `string`    | 将每个数据序列，以列的形式输出                       |
| [`ToStringByColumn`](#pub-m-16) | `int first`            | `string`    | 将每个数据序列的前`first`个数据，以列的形式输出      |

### <div id="private-method">私有方法</div>

| 方法名                   | 传入值           | 返回值类型 | 描述                             |
| ------------------------ | ---------------- | ---------- | -------------------------------- |
| [`checkNames`](#pri-m-1) | `string[] names` | `bool`     | 检查传入的字符串序列是否有重复项 |

---

## 详细描述

---

### <code id="constructor-1">RDataFrame(int)</code>

- #### 完整函数签名
  ```csharp
  public RDataFrame(int numCloumns)
  ```
- #### 详细描述
  实例化一个有`numCloumns`个数据序列的`RDataFrame`实例。
- #### 参数
  - `numCloumns` : 列数（数据序列的数量）
- #### 🌰 使用案例
  ```csharp
  RDataFrame myDataFrame = new RDataFrame(3);
  ```

返回到[**构造方法**](#constructor)

---

### <code id="constructor-2">RDataFrame(params string[])</code>

- #### 完整函数签名
  ```csharp
  public RDataFrame(params string[] columnNames)
  ```
- #### 详细描述
  通过传入的数据序列的名称，实例化一个`RDataFrame`实例。
- #### 参数
  - `columnNames` : 列名称（数据序列的名称）
- #### 🌰 使用案例
  ```csharp
  RDataFrame myDataFrame = new RDataFrame("Cold", "Hot");
  ```

返回到[**构造方法**](#constructor)

---

### <code id="pub-m-1">moveToFirstLine()</code>

- #### 完整函数签名
  ```csharp
  public void moveToForstLine()
  ```
- #### 详细描述
  将游标移动到数据序列的第一行
- #### 🌰 使用案例
  ```csharp
  RDataFrame myDataFrame = new RDataFrame(......);
  myDataFrame.moveToFirstLine();
  ```

返回到[**公有方法**](#public-method)

---

### <code id="pub-m-2">moveToTitle()</code>

- #### 完整函数签名
  ```csharp
  public void moveToTitle()
  ```
- #### 详细描述
  将游标移动到数据序列的标题行（数据序列的上一行）
- #### 🌰 使用案例
  ```csharp
  RDataFrame myDataFrame = new RDataFrame(......);
  myDataFrame.moveToTitle();
  ```

返回到[**公有方法**](#public-method)

---

### <code id="pub-m-3">moveTo(int)</code>

- #### 完整函数签名
  ```csharp
  public void moveTo(int index)
  ```
- #### 详细描述
  将游标移动到指定索引所指的行。  
  如果超出了范围，则会在命令行打印输出错误信息。
- #### 参数
  - `index` : 索引数（第一行数据的索引为0，标题行为-1）
- #### 🌰 使用案例
  ```csharp
  RDataFrame myDataFrame = new RDataFrame(......);
  myDataFrame.moveTo(2);
  ```

返回到[**公有方法**](#public-method)

---

### <code id="pub-m-4">hasNextLine()</code>

- #### 完整函数签名
  ```csharp
  public bool hasNextLine()
  ```
- #### 详细描述
  判断数据序列是否还有下一行（是否是最后一行）。如果有，则返回`true`，否则返回`false`。
- #### 🌰 使用案例
  ```csharp
  RDataFrame myDataFrame = new RDataFrame("data1", "data2");
  myDataFrame.addLine("1","2");//添加一行数据
  myDataFrame.addLine("1","3");//添加一行数据
  string[] output;
  if (myDataFrame.hasNextLine())
  {
    myDataFrame.moveToForstLine();
    output = myDataFrame.getLine();
  }
  //此时 output=["1","2"]
  ```

查看 [`getLine()`](#pub-m-7)

返回到[**公有方法**](#public-method)

---

### <code id="pub-m-5">getColumn(string)</code>

- #### 完整函数签名
  ```csharp
  public string[] getColumn(string colName)
  ```
- #### 详细描述
  返回`colName`所指定的那一列数据序列。如果没有找到，就返回空数组。
- #### 参数
  - `colName` : 列字符串索引
- #### 🌰 使用案例
  ```csharp
  RDataFrame myDataFrame = new RDataFrame("data1", "data2");
  myDataFrame.addLine("1","2");
  myDataFrame.addLine("1","3");
  string[] output = myDataFrame.getColumn("data1");
  //此时 output = ["1","1"]
  ```

返回到 [**公有方法**](#public-method)

---

### <code id="pub-m-6">getColumn(int)</code>

- #### 完整函数签名
  ```csharp
  public string[] getColumn(int colIndex)
  ```
- #### 详细描述
  返回`colIndex`所指定的那一列数据序列。
- #### 参数
  - `colIndex` : 列索引（整型）
- #### 🌰 使用案例
  ```csharp
  RDataFrame myDataFrame = new RDataFrame("data1", "data2");
  myDataFrame.addLine("1","2");
  myDataFrame.addLine("1","3");
  string[] output = myDataFrame.getColumn(1);
  //此时 output = ["2","3"]
  ```
返回到 [**公有方法**](#public-method)

---

### <code id="pub-m-7">getLine()</code>

- #### 完整函数签名
  ```csharp
  public string[] getLine()
  ```
- #### 详细描述
  获得浮标所指的那一行数据，并将浮标向后移动一行。
- #### 🌰 使用案例
  见 [`hasNextLine()`](#pub-m-4)

返回到 [**公有方法**](#public-method)

---

### <code id="pub-m-8">getLine(int)</code>

- #### 完整函数签名
  ```csharp
  public string[] getLine(int rowIndex)
  ```
- #### 详细描述
  返回`rowIndex`所指定的那一行数据。
- #### 参数
  - `rowIndex` : 行索引（整型）
- #### 🌰 使用案例
  ```csharp
  RDataFrame myDataFrame = new RDataFrame("data1", "data2");
  myDataFrame.addLine("1","2");
  myDataFrame.addLine("1","3");
  string[] output = myDataFrame.getLine(1);
  //此时 output = ["1","3"]
  ```
  
返回到 [**公有方法**](#public-method)

---

### <code id="pub-m-9">toMatrix()</code>

- #### 完整函数签名
  ```csharp
  public double[,] toMatrix()
  ```
- #### 详细描述
  将所有数据以矩阵的形式输出，并将字符串数据转化成`double`类型。如果转化失败，则默认为0。
- #### 🌰 使用案例
  ```csharp
  RDataFrame myDataFrame = new RDataFrame("data1", "data2");
  myDataFrame.addLine("1","2");
  myDataFrame.addLine("1","3");
  string[,] output = myDataFrame.toMatrix();
  //此时 output = [ ["1","2"], ["1","3"] ]
  ```

返回到 [**公有方法**](#public-method)

---

### <code id="pub-m-10">toDoubleVector()</code>

- #### 完整函数签名
  ```csharp
  public double[] toDoubleVector()
  ```
- #### 详细描述
  将数据以向量的形式输出，并将字符串数据转化成`double`类型。  
  如果转化失败，则默认为0。如果有多列数据序列，则只取第一列。
- #### 🌰 使用案例
  ```csharp
  RDataFrame myDataFrame = new RDataFrame("data");
  myDataFrame.addLine("1");
  myDataFrame.addLine("2");
  string[,] output = myDataFrame.toDoubleVector();
  //此时 output = ["1","2"]
  ```

返回到 [**公有方法**](#public-method)

---

### <code id="pub-m-11">addLine(params string[])</code>

- #### 完整函数签名
  ```csharp
  public void addLine(params string[] args)
  ```
- #### 详细描述
  添加一行数据。
- #### 🌰 使用案例
  ```csharp
  RDataFrame myDataFrame = new RDataFrame("data1","data2","data3");
  myDataFrame.addLine("1", "1", "2");
  ```

返回到 [**公有方法**](#public-method)

---

### <code id="pub-m-12">ToString()</code>

- #### 完整函数签名
  ```csharp
  public override string ToString()
  ```
- #### 详细描述
  重写ToString方法。

返回到 [**公有方法**](#public-method)

---

### <code id="pub-m-13">ToStringByLine()</code>

- #### 完整函数签名
  ```csharp
  public string ToStringByLine()
  ```
- #### 详细描述
  将每列数据序列，以行的形式输出。
- #### 🌰 使用案例
  ```csharp
  RDataFrame myDataFrame = new RDataFrame("data1","data2","data3");
  myDataFrame.addLine("1", "1", "2");
  myDataFrame.addLine("2", "3", "2");
  Console.WriteLine(myDataFrame.ToStringByLine());
  // 1 2
  // 1 3
  // 2 2
  ```

返回到 [**公有方法**](#public-method)

---

### <code id="pub-m-14">ToStringByLine(int)</code>

- #### 完整函数签名
  ```csharp
  public string ToStringByLine(int first)
  ```
- #### 详细描述
  将每列数据序列前`first`行，以行的形式输出。
- #### 参数
  - `first` : 输出的行数
- #### 🌰 使用案例
  ```csharp
  RDataFrame myDataFrame = new RDataFrame("data1","data2","data3");
  myDataFrame.addLine("1", "1", "2");
  myDataFrame.addLine("2", "3", "2");
  Console.WriteLine(myDataFrame.ToStringByLine(1));
  // 1
  // 1
  // 2
  ```

返回到 [**公有方法**](#public-method)

---

### <code id="pub-m-15">ToStringByColumn()</code>

- #### 完整函数签名
  ```csharp
  public string ToStringByColumn()
  ```
- #### 详细描述
  将每列数据序列，以列的形式输出。
- #### 🌰 使用案例
  ```csharp
  RDataFrame myDataFrame = new RDataFrame("data1","data2","data3");
  myDataFrame.addLine("1", "1", "2");
  myDataFrame.addLine("2", "3", "2");
  Console.WriteLine(myDataFrame.ToStringByLine());
  // 1 1 2
  // 2 3 2
  ```

返回到 [**公有方法**](#public-method)

---

### <code id="pub-m-16">ToStringByColumn(int)</code>

- #### 完整函数签名
  ```csharp
  public string ToStringByLine(int first)
  ```
- #### 详细描述
  将每列数据序列前`first`行，以列的形式输出。
- #### 参数
  - `first` : 输出的行数
- #### 🌰 使用案例
  ```csharp
  RDataFrame myDataFrame = new RDataFrame("data1","data2","data3");
  myDataFrame.addLine("1", "1", "2");
  myDataFrame.addLine("2", "3", "2");
  Console.WriteLine(myDataFrame.ToStringByLine(1));
  // 1 1 2
  ```

返回到 [**公有方法**](#public-method)

---

### <code id="pri-m-1">checkNames(string[])</code>

- #### 完整函数签名
  ```csharp
  bool checkNames(string[] names)
  ```
- #### 详细描述
  判断字符串数组是否存在重复项。如果不存在，则返回`true`，否则返回`false`。
- 服务于: [`构造方法`](#constructor)。
- #### 参数
  - `names` : 字符串数组

返回到 [**私有方法**](#private-method)
