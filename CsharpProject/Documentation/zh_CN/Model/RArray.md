# `RArray` 类

## 简要总结

### <div id="attribute">字段（私有）</div>

| 字段名      | 字段类型    | 描述                     |
| ----------- | ----------- | ------------------------ |
| `nRow`      | `int`       | 行数                     |
| `nCol`      | `int`       | 列数                     |
| `dimension` | `int`       | 维数                     |
| `rowNames`  | `string`    | 行名称（行索引）         |
| `colNames`  | `string`    | 列名称（列索引）         |
| `values1`   | `double[]`  | 当数组为一维时，用来存值 |
| `values2`   | `double[,]` | 当数组为二维时，用来存值 |

### <div id="property">属性（公有）</div>

| 属性名                 | 属性类型   | 描述   |
| ---------------------- | ---------- | ------ |
| `NumCol { get }`       | `int`      | 列数   |
| `RowNames { get set }` | `string[]` | 行名称 |
| `ColNames { get set }` | `string[]` | 列名称 |

### <div id="constructor">构造方法</div>

| 方法名                     | 传入值                                                           | 描述                 |
| -------------------------- | ---------------------------------------------------------------- | -------------------- |
| [`RArray`](#constructor-1) | `double[] values`<br>`string[] colNames`                         | 给一维数组的构造方法 |
| [`RArray`](#constructor-2) | `double[,] values`<br>`string[] rowNames`<br>`string[] colNames` | 给二维数组的构造方法 |

### <div id="operator">运算符重载</div>

| 运算符                            | 返回值类型 | 描述                                                  |
| --------------------------------- | ---------- | ----------------------------------------------------- |
| [`operator[int] {get}`](#op-1)     | `double`   | 通过整型索引，获得一维数组的值                        |
| [`operator[string] {get}`](#op-2)  | `double`   | 通过字符串型索引，获得一维数组的值 ( `key => value` ) |
| [`operator[int,int]  {get}`](#op-3) | `double`   | 通过整型索引，获得二维数组的值                        |

### <div id="public-method">公有方法</div>

| 方法名                         | 传入值 | 返回值类型 | 描述                                                            |
| ------------------------------ | ------ | ---------- | --------------------------------------------------------------- |
| [`ToString`](#pub-m-1)         | `void` | `string`   | 重写`ToString()`方法，根据[`dimension`](#attribute)调用相应方法 |
| [`rowNamesToString`](#pub-m-2) | `void` | `string`   | 返回一个包含所有行名称的字符串                                  |
| [`colNamesToString`](#pub-m-3) | `void` | `string`   | 返回一个包含所有列名称的字符串                                  |

### <div id="private-method">私有方法</div>

| 方法名                     | 传入值 | 返回值类型 | 描述                               |
| -------------------------- | ------ | ---------- | ---------------------------------- |
| [`dim1ToString`](#pri-m-1) | `void` | `string`   | 返回一个包含所有一维数组值的字符串 |
| [`dim2ToString`](#pri-m-2) | `void` | `string`   | 返回一个包含所有二维数组值的字符串 |

---

## 详细描述

---

### <code id="constructor-1">RArray(double[],string[])</code>

- #### 完整函数签名
  ```csharp
  public RArray(double[] values, string[] colName)
  ```
- #### 详细描述
  通过传入一个一维的 double 数组，以及每个值的字符串索引，来构造一个`RArray`实例。
- #### 🌰 使用案例
  ```csharp
  double[] values = new double[] {1.0, 2.0, 3.0};
  string[] names = new string[] {"zero", "one", "two"};
  RArray myArray = new RArray(values, names);
  ```

返回到[**构造方法**](#constructor)

---

### <code id="constructor-2">RArray(double[,],string[],string[])</code>

- #### 完整函数签名
  ```csharp
  public RArray(double[] values, string rowNames, string[] colName)
  ```
- #### 详细描述
  通过传入一个二维的 double 数组，以及每行和每列的字符串索引，来构造一个`RArray`实例。
- #### 🌰 使用案例
  ```csharp
  double[,] values = new double[,]{
    new double[] {1.0, 2.0, 3.0},
    new double[] {1.2, 2.3, 3.4}
  };
  string[] rowNames = new string[] {"zero", "one"};
  string[] colNames = new string[] {"zero", "one", "two"};
  RArray myArray = new RArray(values, rowNames, colNames);
  ```

返回到[**构造方法**](#constructor)

---

### <code id="op-1">operator:[int]</code>

- #### 完整签名
  ```csharp
  public double this[int index] { get }
  ```
- #### 详细描述
  通过整型索引，获得一维数组的值.  
  如果当前实例是二维数组，或者则返回-1.  
- #### 🌰 例子：
  ```csharp
  double[] values = new double[] {1.0, 2.0, 3.0};
  string[] names = new string[] {"zero", "one", "two"};
  RArray myArray = new RArray(values, names);
  double output = myArray[2];
  Console.WriteLine(output); // 命令行将会显示3
  ```

返回到[**运算符重载**](#operator)

---

### <code id="op-2">operator:[string]</code>

- #### 完整签名
  ```csharp
  public double this[string colName] { get }
  ``` 
- #### 详细描述
  通过字符串型索引，获得一维数组的值.  
  如果当前实例是二维数组，则返回 -1.  
  如果并没有匹配到字符串索引，则返回 0.  
- #### 🌰 例子：
  ```csharp
  double[] values = new double[] {1.0, 2.0, 3.0};
  string[] names = new string[] {"zero", "one", "two"};
  RArray myArray = new RArray(values, names);
  double output = myArray["one"];
  Console.WriteLine(output); // 命令行将会显示2
  ```

返回到[**运算符重载**](#operator)

---

### <code id="op-3">operator:[int,int]</code>

- #### 完整签名
  ```csharp
  public double this[int indexRow, int indexCol] { get }
  ```
- #### 详细描述
  通过整型索引，获得二维数组的值.  
  如果当前实例是一维数组，则返回 -1.  
- #### 🌰 例子：
  ```csharp
  double[,] values = new double[,]{
    new double[] {1.0, 2.0, 3.0},
    new double[] {1.2, 2.3, 3.4}
  };
  string[] rowNames = new string[] {"zero", "one"};
  string[] colNames = new string[] {"zero", "one", "two"};
  RArray myArray = new RArray(values, rowNames, colNames);
  double output = myArray[1,2];
  Console.WriteLine(output); //命令行将会显示3.4
  ```

返回到[**运算符重载**](#operator)

---

### <code id="pub-m-1">ToString()</code>

- #### 完整签名
  ```csharp
  public override String ToString()
  ```
- #### 详细描述
  根据[`dimension`](#attribute)，如果`RArray`的实例是一维的，则调用[`dim1ToString()`](#pub-m-1)方法，并返回该字符串。如果实例是二维的，则调用[`dim2ToString()`](#pub-m-2)方法并返回字符串。
- #### 🌰 例子：
  ```csharp
  double[] values = new double[] {1.0, 2.0, 3.0};
  string[] names = new string[] {"zero", "one", "two"};
  RArray myArray = new RArray(values, names);
  Console.WriteLine(myArray.ToString());
  // 或者直接省略掉ToString()
  Console.WriteLine(myArray);
  ```

返回到[**公有方法**](#public-method)

---

### <code id="pub-m-2">rowNamesToString()</code>

- #### 完整签名
  ```csharp
  public String rowNamesToString()
  ```

返回到[**公有方法**](#public-method)

---

### <code id="pub-m-3">colNamesToString()</code>

- #### 完整签名
```csharp
public String colNamesToString()
```

返回到[**公有方法**](#public-method)

---

### <code id="pri-m-1">dim1ToString()</code>

- #### 完整签名
```csharp
String dim1ToString()
```
- #### 详细描述
  返回一个包含所有一维数组值和相应列名称的字符串。该字符串含有格式，可直接在命令行或文本框输出。  
  服务于[`ToString()`](#pub-m-1)方法

返回到[**私有方法**](#private-method)

---

### <code id="pri-m-2">dim2ToString()</code>

- #### 完整签名
```csharp
String dim2ToString()
```
- #### 详细描述
  返回一个包含所有二维数组值和相应行列名称的字符串。该字符串含有格式，可直接在命令行或文本框输出。  
  服务于[`ToString()`](#pub-m-1)方法

返回到[**私有方法**](#private-method)
