# `CsvHelper` 类

## 简要总结

### <div id="attribute">字段（私有）</div>

| 字段名 | 字段类型 | 描述     |
| ------ | -------- | -------- |
| `path` | `string` | 文件路径 |

### <div id="constructor">构造方法</div>

| 方法名                        | 传入值        |
| ----------------------------- | ------------- |
| [`CsvHelper`](#constructor-1) | `string path` |

### <div id="public-method">公有方法</div>

| 方法名                                | 传入值                                 | 返回值类型                    | 描述                                                     |
| ------------------------------------- | -------------------------------------- | ----------------------------- | -------------------------------------------------------- |
| [`getDataByStreamReader`](#pub-m-1)   | `char splitChar`                       | [`RDataFrame`](RDataFrame.md) | 读取\*.csv 文件                                          |
| [`writeDataByStreamWriter`](#pub-m-2) | `RDataFrame data,`<br>`char splitChar` | `void`                        | 将一个[`RDataFrame`](RDataFrame.md)对象写入\*.csv 文件里 |

### <div id="static-method">静态方法</div>

| 方法名                     | 传入值                             | 返回值类型                    | 描述            |
| -------------------------- | ---------------------------------- | ----------------------------- | --------------- |
| [`getDataFrame`](#sta-m-1) | `string path`                      | [`RDataFrame`](RDataFrame.md) | 读取\*.csv 文件 |
| [`getDataFrame`](#sta-m-2) | `string path,`<br>`char splitChar` | [`RDataFrame`](RDataFrame.md) | 读取\*.csv 文件 |

### <div id="private-method">私有方法</div>

| 方法名                        | 传入值                                  | 返回值类型 | 描述                                                     |
| ----------------------------- | --------------------------------------- | ---------- | -------------------------------------------------------- |
| [`trimArrayString`](#pri-m-1) | `string[] str`                          | `string[]` | 将一个字符串数组的每一个字符串对象执行`Trim()`并返回结果 |
| [`prepareOneLine`](#pri-m-2)  | `string[] oneLine,`<br>`char splitChar` | `string`   | 将一个字符串数组，通过分隔符，连接成一个字符串           |

---

## 详细描述

---

### <code id="constructor-1">CsvHelper(string)</code>

- #### 完整函数签名
  ```csharp
  public CsvHelper(string path)
  ```
- #### 详细描述
  实例化一个`CsvHelper`对象。
- #### 参数
  - `path` : 文件路径
- #### 🌰 使用案例
  ```csharp
  string path = @"/Users/yangkai/Desktop/data.csv";
  CsvHelper myHelper = new CsvHelper(path);
  ```

返回到 [**构造方法**](#constructor)

---

### <code id="pub-m-1">getDataByStreamReader(char)</code>

- #### 完整函数签名
  ```csharp
  public RDataFrame getDataByStreamReader(char splitChar)
  ```
- #### 详细描述
  读取文件，并以`RDataFrame`对象的形式，返回文件内容。
- #### 参数
  - `splitChar` : 单行数据之间的分隔符
- #### 🌰 使用案例
  ```csharp
  CsvHelper myHelper = new CsvHelper(@"/Users/yangkai/Desktop/data.csv");
  RDataFrame myDataFrame = myHelper.getDataByStreamReader(',');
  Console.WriteLine(myDataFrame);
  ```

返回到 [**公有方法**](#public-method)

---

### <code id="pub-m-2">writeDataByStreamWriter(RDataFrame,char)</code>

- #### 完整函数签名
  ```csharp
  public void writeDataByStreamWriter(RDataFrame data, char splitChar)
  ```
- #### 详细描述
  将`RDataFrame`对象的内容写入文件，单行数据之间以`splitChar`分隔。
- #### 参数
  - `data` : 数据
  - `splitChar` : 单行数据之间的分隔符
- #### 🌰 使用案例
  ```csharp
  CsvHelper myHelper = new CsvHelper(@"/Users/yangkai/Desktop/data.csv");
  RDataFrame myData = ...... ; //创建并构建数据
  myHelper.writeDataByStreamWriter(myData, ',');
  ```

返回到 [**公有方法**](#public-method)

---

### <code id="sta-m-1">getDataFrame(string)</code>

- #### 完整函数签名
  ```csharp
  public static RDataFrame getDataFrame(string path)
  ```
- #### 详细描述
  读取路径为`path`的文件，并以`RDataFrame`对象的形式，返回文件内容。单行数据之间默认以`','`分隔。
- #### 参数
  - `path` : 数据文件的路径
- #### 🌰 使用案例
  ```csharp
  string path = @"/Users/yangkai/Desktop/data.csv";
  RDataFrame myData = CsvHelper.getDataFrame(path);
  Console.WriteLine(myData);
  ```

返回到 [**静态方法**](#static-method)

---

### <code id="sta-m-2">getDataFrame(string,char)</code>

- #### 完整函数签名
  ```csharp
  public static RDataFrame getDataFrame(string path, char splitChar)
  ```
- #### 详细描述
  读取路径为`path`的文件，并以`RDataFrame`对象的形式，返回文件内容。单行数据之间以`splitChar`分隔。
- #### 参数
  - `path` : 数据文件的路径
  - `splitChar` : 单行数据之间的分隔符
- #### 🌰 使用案例
  ```csharp
  string path = @"/Users/yangkai/Desktop/data.csv";
  RDataFrame myData = CsvHelper.getDataFrame(path, ';');//单行数据之间将会以分号分隔
  Console.WriteLine(myData);
  ```

返回到 [**静态方法**](#static-method)

---

### <code id="pri-m-1">trimArrayString(string[])</code>

- #### 完整函数签名
  ```csharp
  private string[] trimArrayString(string[] str)
  ```
- #### 详细描述
  将一个字符串数组的每一个字符串对象，移除头尾的空白空格，并返回结果。
- 服务于: [`getDataByStreamReader`](#pub-m-1)。
- #### 参数
  - `str` : 需要处理的字符串数组对象

返回到[**私有方法**](#private-method)

---

### <code id="pri-m-1">prepareOneLine(string[],char)</code>

- #### 完整函数签名
  ```csharp
  private string prepareOneLine(string[] oneLine, char splitChar)
  ```
- #### 详细描述
  将一个字符串数组，通过分隔符，连接成一个字符串
- 服务于: [`writeDataByStreamWriter`](#pub-m-2)。
- #### 参数
  - `oneLine` : 需要预处理的字符串数组对象
  - `splitChar` : 数据之间的分隔符

返回到[**私有方法**](#private-method)
