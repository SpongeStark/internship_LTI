# La classe `RDataFrame`

## Bref

### <div id="attribute">Attribut (privé)</div>

| nom        | type           | description                                                |
| ---------- | -------------- | ---------------------------------------------------------- |
| `nCol`     | `int`          | nombre de colonne (chaque colonne est une série de donnée) |
| `size`     | `int`          | longeur de la série de donnée                              |
| `colNames` | `string[]`     | nom de chaque série de donnée                              |
| `data`     | `List<string>` | les données                                                |
| `pointer`  | `int`          | le curseur (au service de lire chaque ligne)               |

### <div id="property">Propriété (publique)</div>

| nom              | type       | description                   |
| ---------------- | ---------- | ----------------------------- |
| `NumCol { get }` | `int`      | nombre de colonne             |
| `Size { get }`   | `int`      | longeur de la série de donnée |
| `Title { get }`  | `string[]` | nom de chaque série de donnée |

### <div id="constructor">Constructeur</div>

| nom                            | entrée                        |
| ------------------------------ | ----------------------------- |
| [`RDataFrame`](#constructor-1) | `int numCloumns`              |
| [`RDataFrame`](#constructor-2) | `params string[] columnNames` |

### <div id="public-method">Méthode publique</div>

| nom                             | entrée                 | type de retourner | description                                                                            |
| ------------------------------- | ---------------------- | ----------------- | -------------------------------------------------------------------------------------- |
| [`moveToFirstLine`](#pub-m-1)   | `void`                 | `void`            | déplacer le curseur à la première ligne de donnée                                      |
| [`moveToTitle`](#pub-m-2)       | `void`                 | `void`            | déplacer le curseur à la ligne de titre                                                |
| [`moveTo`](#pub-m-3)            | `int index`            | `string`          | déplacer le curseur à la `index`-ème ligne                                             |
| [`hasNextLine`](#pub-m-4)       | `void`                 | `bool`            | vérifier si le surseur est à la dernière ligne                                         |
| [`getColumn`](#pub-m-5)         | `string colName`       | `string[]`        | obtenir une série de donnée (une colonne) dont le nom est `colName`                    |
| [`getColumn`](#pub-m-6)         | `int colIndex`         | `string[]`        | obtenir une série de donnée (une colonne) dont l'index est `colIndex`                  |
| [`getLine`](#pub-m-7)           | `void`                 | `string[]`        | ontenir la ligne de donnée où le curseur est, et le curseur se déplace une ligne       |
| [`getLine`](#pub-m-8)           | `int rowIndex`         | `string[]`        | obtenir la `rowIndex`-ème ligne, et le curseur se déplace une ligne                    |
| [`toMatrix`](#pub-m-9)          | `void`                 | `string[,]`       | retourner la matrice dont cette instance de `RDataFrame` est correspondante            |
| [`toDoubleVector`](#pub-m-10)   | `void`                 | `string[]`        | retourner le vecteur dont cette instance de `RDataFrame` est correspondante            |
| [`addLine`](#pub-m-11)          | `params string[] args` | `void`            | ajouter une ligne de donnée                                                            |
| [`ToString`](#pub-m-12)         | `void`                 | `string`          | appeler `ToStringByColumn()` par défault                                               |
| [`ToStringByLine`](#pub-m-13)   | `void`                 | `string`          | affichier chaque série de donnée sous forme de ligne                                   |
| [`ToStringByLine`](#pub-m-14)   | `int first`            | `string`          | affichier les premières `first` valeur de chaque série de donnée sous forme de ligne   |
| [`ToStringByColumn`](#pub-m-15) | `int `                 | `string`          | affichier chaque série de donnée sous forme de colonne                                 |
| [`ToStringByColumn`](#pub-m-16) | `int first`            | `string`          | affichier les premières `first` valeur de chaque série de donnée sous forme de colonne |

### <div id="private-method">Méthode privée</div>

| nom                      | entrée           | type de retourner | description                                                                              |
| ------------------------ | ---------------- | ----------------- | ---------------------------------------------------------------------------------------- |
| [`checkNames`](#pri-m-1) | `string[] names` | `bool`            | vérifier s'il existe les termes pareils dans le tableau de chaîne de charactère d'entrée |

---

## Détail

---

### <code id="constructor-1">RDataFrame(int)</code>

- #### Prototype complet
  ```csharp
  public RDataFrame(int numCloumns)
  ```
- #### Description détaillée
  Instancier un objet `RDataFrame` qui a `numCloumns` séries de donnée.
- #### Paramètre d'entrée
  - `numCloumns` : nombre de colonne (nombre de série de donnée)
- #### 🌰 Exemple d'usage
  ```csharp
  RDataFrame myDataFrame = new RDataFrame(3);
  ```

Retourner au [**Constructeur**](#constructor)

---

### <code id="constructor-2">RDataFrame(params string[])</code>

- #### Prototype complet
  ```csharp
  public RDataFrame(params string[] columnNames)
  ```
- #### Description détaillée
  Instancier un objet `RDataFrame` en utilisant les noms de colonne d'entrée.
- #### Paramètre d'entrée
  - `columnNames` : noms du colonne (noms des séries de donnée)
- #### 🌰 Exemple d'usage
  ```csharp
  RDataFrame myDataFrame = new RDataFrame("Cold", "Hot");
  ```

Retourner au [**Constructeur**](#constructor)

---

### <code id="pub-m-1">moveToFirstLine()</code>

- #### Prototype complet
  ```csharp
  public void moveToForstLine()
  ```
- #### Description détaillée
  Déplacer le curseur à la première ligne de donnée 
- #### 🌰 Exemple d'usage
  ```csharp
  RDataFrame myDataFrame = new RDataFrame(......);
  myDataFrame.moveToFirstLine();
  ```

Retourner au [**Méthode publique**](#public-method)

---

### <code id="pub-m-2">moveToTitle()</code>

- #### Prototype complet
  ```csharp
  public void moveToTitle()
  ```
- #### Description détaillée
  Déplacer le curseur à la ligne de titre  (la ligne précédent de la prelière ligne de donnée)
- #### 🌰 Exemple d'usage
  ```csharp
  RDataFrame myDataFrame = new RDataFrame(......);
  myDataFrame.moveToTitle();
  ```

Retourner au [**Méthode publique**](#public-method)

---

### <code id="pub-m-3">moveTo(int)</code>

- #### Prototype complet
  ```csharp
  public void moveTo(int index)
  ```
- #### Description détaillée
  Déplacer le curseur à la `index`-ème ligne.  
  Si `index` est hors le rayon, on affiche le message d'erreur dans console.
- #### Paramètre d'entrée
  - `index` : numéro d'index (la première ligne de donnée est 0, la ligne de titre est -1)
- #### 🌰 Exemple d'usage
  ```csharp
  RDataFrame myDataFrame = new RDataFrame(......);
  myDataFrame.moveTo(2);
  ```

Retourner au [**Méthode publique**](#public-method)

---

### <code id="pub-m-4">hasNextLine()</code>

- #### Prototype complet
  ```csharp
  public bool hasNextLine()
  ```
- #### Description détaillée
  Vérifier s'il y a encore une ligne de donnée. Si c'est la dernière ligne, on retourne `false`, sinon on retourne `true`.
- #### 🌰 Exemple d'usage
  ```csharp
  RDataFrame myDataFrame = new RDataFrame("data1", "data2");
  myDataFrame.addLine("1","2");//ajouter une ligne de donnée
  myDataFrame.addLine("1","3");//ajouter une ligne de donnée
  string[] output;
  if (myDataFrame.hasNextLine())
  {
    myDataFrame.moveToForstLine();
    output = myDataFrame.getLine();
  }
  //Actuellement, output=["1","2"]
  ```

Voir aussi [`getLine()`](#pub-m-7)

Retourner au [**Méthode publique**](#public-method)

---

### <code id="pub-m-5">getColumn(string)</code>

- #### Prototype complet
  ```csharp
  public string[] getColumn(string colName)
  ```
- #### Description détaillée
  Retourner la série de donnée dont le nom de colonne est `colName`. Si on ne trouve pas, on renvoyer une chaîne vide.
- #### Paramètre d'entrée
  - `colName` : nom de colonne (index de type `string`)
- #### 🌰 Exemple d'usage
  ```csharp
  RDataFrame myDataFrame = new RDataFrame("data1", "data2");
  myDataFrame.addLine("1","2");
  myDataFrame.addLine("1","3");
  string[] output = myDataFrame.getColumn("data1");
  //Actuellement, output = ["1","1"]
  ```

Retourner au [**Méthode publique**](#public-method)

---

### <code id="pub-m-6">getColumn(int)</code>

- #### Prototype complet
  ```csharp
  public string[] getColumn(int colIndex)
  ```
- #### Description détaillée
  Retourner la série de donnée dont le numéro de colonne est `colIndex`.
- #### Paramètre d'entrée
  - `colIndex` : l'index de la série de donnée (numéro de colonne)
- #### 🌰 Exemple d'usage
  ```csharp
  RDataFrame myDataFrame = new RDataFrame("data1", "data2");
  myDataFrame.addLine("1","2");
  myDataFrame.addLine("1","3");
  string[] output = myDataFrame.getColumn(1);
  //Actuellement, output = ["2","3"]
  ```
  Retourner au [**Méthode publique**](#public-method)

---

### <code id="pub-m-7">getLine()</code>

- #### Prototype complet
  ```csharp
  public string[] getLine()
  ```
- #### Description détaillée
  Obtenir une ligne de donnée où le curseur indique, et déplacer le curseur à la ligne suivant.
- #### 🌰 Exemple d'usage
  Voir [`hasNextLine()`](#pub-m-4)

Retourner au [**Méthode publique**](#public-method)

---

### <code id="pub-m-8">getLine(int)</code>

- #### Prototype complet
  ```csharp
  public string[] getLine(int rowIndex)
  ```
- #### Description détaillée
  Retourner une ligne de donn"e où `rowIndex` indique.
- #### Paramètre d'entrée
  - `rowIndex` : l'index de la ligne
- #### 🌰 Exemple d'usage
  ```csharp
  RDataFrame myDataFrame = new RDataFrame("data1", "data2");
  myDataFrame.addLine("1","2");
  myDataFrame.addLine("1","3");
  string[] output = myDataFrame.getLine(1);
  //Actuellement, output = ["1","3"]
  ```

Retourner au [**Méthode publique**](#public-method)

---

### <code id="pub-m-9">toMatrix()</code>

- #### Prototype complet
  ```csharp
  public double[,] toMatrix()
  ```
- #### Description détaillée
  Sortir toutes les données sous la forme de Matrice, et convertir les `string`s à `double`s.   
  Si la conversion est échoué, la valeur est par défaut 0.  
- #### 🌰 Exemple d'usage
  ```csharp
  RDataFrame myDataFrame = new RDataFrame("data1", "data2");
  myDataFrame.addLine("1","2");
  myDataFrame.addLine("1","3");
  string[,] output = myDataFrame.toMatrix();
  //Actuellement, output = [ ["1","2"], ["1","3"] ]
  ```

Retourner au [**Méthode publique**](#public-method)

---

### <code id="pub-m-10">toDoubleVector()</code>

- #### Prototype complet
  ```csharp
  public double[] toDoubleVector()
  ```
- #### Description détaillée
  Sortir les données sous la forme de vecteur, et convertir les `string`s à `double`s.  
  Si la conversion est échoué, la valeur est par défaut 0.   
  S'il y a plusieurs séries de donnée, on ne prend que la première série.
- #### 🌰 Exemple d'usage
  ```csharp
  RDataFrame myDataFrame = new RDataFrame("data");
  myDataFrame.addLine("1");
  myDataFrame.addLine("2");
  string[,] output = myDataFrame.toDoubleVector();
  //Actuellement, output = ["1","2"]
  ```

Retourner au [**Méthode publique**](#public-method)

---

### <code id="pub-m-11">addLine(params string[])</code>

- #### Prototype complet
  ```csharp
  public void addLine(params string[] args)
  ```
- #### Description détaillée
  Ajouter une ligne de donnée.
- #### 🌰 Exemple d'usage
  ```csharp
  RDataFrame myDataFrame = new RDataFrame("data1","data2","data3");
  myDataFrame.addLine("1", "1", "2");
  ```

Retourner au [**Méthode publique**](#public-method)

---

### <code id="pub-m-12">ToString()</code>

- #### Prototype complet
  ```csharp
  public override string ToString()
  ```
- #### Description détaillée
  Récrire la méthode `ToString`.

Retourner au [**Méthode publique**](#public-method)

---

### <code id="pub-m-13">ToStringByLine()</code>

- #### Prototype complet
  ```csharp
  public string ToStringByLine()
  ```
- #### Description détaillée
  Prendre chaque série de donnée, et sortir sous la forme de ligne.
- #### 🌰 Exemple d'usage
  ```csharp
  RDataFrame myDataFrame = new RDataFrame("data1","data2","data3");
  myDataFrame.addLine("1", "1", "2");
  myDataFrame.addLine("2", "3", "2");
  Console.WriteLine(myDataFrame.ToStringByLine());
  // 1 2
  // 1 3
  // 2 2
  ```

Retourner au [**Méthode publique**](#public-method)

---

### <code id="pub-m-14">ToStringByLine(int)</code>

- #### Prototype complet
  ```csharp
  public string ToStringByLine(int first)
  ```
- #### Description détaillée
  Prendre les premières `first` ligne de chaque série de donnée, et sortir sous la forme de ligne.
- #### Paramètre d'entrée
  - `first` : nombre de ligne que l'on sort
- #### 🌰 Exemple d'usage
  ```csharp
  RDataFrame myDataFrame = new RDataFrame("data1","data2","data3");
  myDataFrame.addLine("1", "1", "2");
  myDataFrame.addLine("2", "3", "2");
  Console.WriteLine(myDataFrame.ToStringByLine(1));
  // 1
  // 1
  // 2
  ```

Retourner au [**Méthode publique**](#public-method)

---

### <code id="pub-m-15">ToStringByColumn()</code>

- #### Prototype complet
  ```csharp
  public string ToStringByColumn()
  ```
- #### Description détaillée
  Prendre chaque série de donnée, et sortir sous la forme de colonne.
- #### 🌰 Exemple d'usage
  ```csharp
  RDataFrame myDataFrame = new RDataFrame("data1","data2","data3");
  myDataFrame.addLine("1", "1", "2");
  myDataFrame.addLine("2", "3", "2");
  Console.WriteLine(myDataFrame.ToStringByLine());
  // 1 1 2
  // 2 3 2
  ```

Retourner au [**Méthode publique**](#public-method)

---

### <code id="pub-m-16">ToStringByColumn(int)</code>

- #### Prototype complet
  ```csharp
  public string ToStringByLine(int first)
  ```
- #### Description détaillée
  Prendre les premières `first` ligne de chaque série de donnée, et sortir sous la forme de colonne.
- #### Paramètre d'entrée
  - `first` : nombre de ligne que l'on sort
- #### 🌰 Exemple d'usage
  ```csharp
  RDataFrame myDataFrame = new RDataFrame("data1","data2","data3");
  myDataFrame.addLine("1", "1", "2");
  myDataFrame.addLine("2", "3", "2");
  Console.WriteLine(myDataFrame.ToStringByLine(1));
  // 1 1 2
  ```

Retourner au [**Méthode publique**](#public-method)

---

### <code id="pri-m-1">checkNames(string[])</code>

- #### Prototype complet
  ```csharp
  bool checkNames(string[] names)
  ```
- #### Description détaillée
  Verifier si un tableau de chaîne de charactère contient deux objets pareils. S'il existe, on retourne `true`, sinon on retourne `false`.
- Au service du [`Constructeur`](#constructor-2)。
- #### Paramètre d'entrée
  - `names` : le tableau de chaîne de charactère

Retourner au [**Méthode privée**](#private-method)
