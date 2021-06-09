# La classe `RArray`

## Bref

### <div id="attribute">Attribut (privé)</div>

| nom         | type        | description                              |
| ----------- | ----------- | ---------------------------------------- |
| `nRow`      | `int`       | nombre de ligne                          |
| `nCol`      | `int`       | nombre de colonne                        |
| `dimension` | `int`       | dimension                                |
| `rowNames`  | `string`    | nom(index) de ligne                      |
| `colNames`  | `string`    | nom(index) de colonne                    |
| `values1`   | `double[]`  | enregistrer les données pour dimension 1 |
| `values2`   | `double[,]` | enregistrer les données pour dimension 2 |

### <div id="property">Propriété (publique)</div>

| nom                    | type       | description       |
| ---------------------- | ---------- | ----------------- |
| `NumCol { get }`       | `int`      | nombre de colonne |
| `RowNames { get set }` | `string[]` | nom de ligne      |
| `ColNames { get set }` | `string[]` | nom de colonne    |

### <div id="constructor">Constructeur</div>

| nom                        | entrée                                                           | description                   |
| -------------------------- | ---------------------------------------------------------------- | ----------------------------- |
| [`RArray`](#constructor-1) | `double[] values`<br>`string[] colNames`                         | Constructeur pour dimension 1 |
| [`RArray`](#constructor-2) | `double[,] values`<br>`string[] rowNames`<br>`string[] colNames` | Constructeur pour dimension 2 |

### <div id="operator">Surcharge de l'opérateur</div>

| opérateur                         | type de retourner | description                                                                          |
| --------------------------------- | ----------------- | ------------------------------------------------------------------------------------ |
| [`operator[int]{get}`](#op-1)     | `double`          | obtenir la valeur selon l'index entier pour dimension 1                              |
| [`operator[string]{get}`](#op-2)  | `double`          | obtenir la valeur selon l'index de type `string` pour dimension 1 ( `key => value` ) |
| [`operator[int,int]{get}`](#op-3) | `double`          | obtenir la valeur selon l'index entier pour dimension 2                              |

### <div id="public-method">Méthode publique</div>

| nom                            | entrée | type de retourner | description                                                                                      |
| ------------------------------ | ------ | ----------------- | ------------------------------------------------------------------------------------------------ |
| [`ToString`](#pub-m-1)         | `void` | `string`          | récrir la méthode `ToString()`, appeler la méthode correspondant selon [`dimension`](#attribute) |
| [`rowNamesToString`](#pub-m-2) | `void` | `string`          | retourner une chaîne de charactères qui contient tous les noms des lignes                        |
| [`colNamesToString`](#pub-m-3) | `void` | `string`          | retourner une chaîne de charactères qui contient tous les noms des colonne                       |

### <div id="private-method">Méthode privée</div>

| nom                        | entrée | type de retourner | description                      |
| -------------------------- | ------ | ----------------- | -------------------------------- |
| [`dim1ToString`](#pri-m-1) | `void` | `string`          | `ToString()` pour la dimension 1 |
| [`dim2ToString`](#pri-m-2) | `void` | `string`          | `ToString()` pour la dimension 2 |

---

## Détail

---

### <code id="constructor-1">RArray(double[],string[])</code>

- #### Prototype complet
  ```csharp
  public RArray(double[] values, string[] colName)
  ```
- #### Description détaillée
  On entre un tableau de type `double` à une dimension, ainsi l'index de chaque valeur, pour initialiser une instance de `RArray`.
- #### 🌰 Exemple d'usage 
  ```csharp
  double[] values = new double[] {1.0, 2.0, 3.0};
  string[] names = new string[] {"zero", "one", "two"};
  RArray myArray = new RArray(values, names);
  ```

Retourner au [**Constructor**](#constructor)

---

### <code id="constructor-2">RArray(double[,],string[],string[])</code>

- #### Prototype complet
  ```csharp
  public RArray(double[] values, string rowNames, string[] colName)
  ```
- #### Description détaillée
  On entre un tableau de type `double` à deux dimension, ainsi l'index de chaque valeur, pour initialiser une instance de `RArray`.
- #### 🌰 Exemple d'usage 
  ```csharp
  double[,] values = new double[,]{
    new double[] {1.0, 2.0, 3.0},
    new double[] {1.2, 2.3, 3.4}
  };
  string[] rowNames = new string[] {"zero", "one"};
  string[] colNames = new string[] {"zero", "one", "two"};
  RArray myArray = new RArray(values, rowNames, colNames);
  ```

Retourner au [**Constructor**](#constructor)

---

### <code id="op-1">operator:[int]</code>

- #### Prototype complet
  ```csharp
  public double this[int index] { get }
  ```
- #### Description détaillée 
  Obtenez la valeur d'un tableau à une dimensions grâce à l'indexation de `int`.  
  Si l'instance actuelle est bidimensionnel, on retourne -1.  
- #### 🌰 Exemple d'usage
  ```csharp
  double[] values = new double[] {1.0, 2.0, 3.0};
  string[] names = new string[] {"zero", "one", "two"};
  RArray myArray = new RArray(values, names);
  double output = myArray[2];
  Console.WriteLine(output); // ça va afficher 3 dans console
  ```

Retourner au [**Surcharge d'opérateur**](#operator)

---

### <code id="op-2">operator:[string]</code>

- #### Prototype complet
  ```csharp
  public double this[string colName] { get }
  ```
- #### Description détaillée
  Obtenez la valeur d'un tableau à une dimensions grâce à l'indexation de `string`.  
  Si l'instance actuelle est bidimensionnel, on retourne -1.  
  Si on ne trouve pas l'index, on retourne 0.  
- #### 🌰 Exemple d'usage
  ```csharp
  double[] values = new double[] {1.0, 2.0, 3.0};
  string[] names = new string[] {"zero", "one", "two"};
  RArray myArray = new RArray(values, names);
  double output = myArray["one"];
  Console.WriteLine(output); // ça va afficher 2 dans console
  ```

Retourner au [**Surcharge d'opérateur**](#operator)

---

### <code id="op-3">operator:[int,int]</code>

- #### Prototype complet
  ```csharp
  public double this[int indexRow, int indexCol] { get }
  ```
- #### Description détaillée
  Obtenez la valeur d'un tableau à deux dimensions grâce à l'indexation d'entiers.  
  Si l'instance actuelle est unidimensionnel, on retourne -1.
- #### 🌰 Exemple d'usage
  ```csharp
  double[,] values = new double[,]{
    new double[] {1.0, 2.0, 3.0},
    new double[] {1.2, 2.3, 3.4}
  };
  string[] rowNames = new string[] {"zero", "one"};
  string[] colNames = new string[] {"zero", "one", "two"};
  RArray myArray = new RArray(values, rowNames, colNames);
  double output = myArray[1,2];
  Console.WriteLine(output); //ça va afficher 3.4 dans console
  ```

Retourner au [**Surcharge d'opérateur**](#operator)

---

### <code id="pub-m-1">ToString()</code>

- #### Prototype complet
  ```csharp
  public override String ToString()
  ```
- #### Description détaillée
  Selon [`dimension`](#attribute), si l'instance de `RArray` est unidimensionnel, alors on appel la Méthode [`dim1ToString()`](#pub-m-1), et retourne la string.   
  Si c'est bidimensionnel, on appel [`dim2ToString()`](#pub-m-2).
- #### 🌰 Exemple d'usage
  ```csharp
  double[] values = new double[] {1.0, 2.0, 3.0};
  string[] names = new string[] {"zero", "one", "two"};
  RArray myArray = new RArray(values, names);
  Console.WriteLine(myArray.ToString());
  // ou on peut omettre "ToString()"
  Console.WriteLine(myArray);
  ```

Retourner au [**Méthode publique**](#public-method)

---

### <code id="pub-m-2">rowNamesToString()</code>

- #### Prototype complet
  ```csharp
  public String rowNamesToString()
  ```

Retourner au [**Méthode publique**](#public-method)

---

### <code id="pub-m-3">colNamesToString()</code>

- #### Prototype complet

```csharp
public String colNamesToString()
```

Retourner au [**Méthode publique**](#public-method)

---

### <code id="pri-m-1">dim1ToString()</code>

- #### Prototype complet

```csharp
String dim1ToString()
```

- #### Description détaillée
  Renvoie une chaîne contenant toutes les valeurs de tableau à une dimension et les noms de colonne correspondants. La chaîne contient un format et peut être sortie directement sur la ligne de commande ou la zone de texte.  
- Au service de la méthode [`ToString()`](#pub-m-1)

Retourner au [**Méthode privée**](#private-method)

---

### <code id="pri-m-2">dim2ToString()</code>

- #### Prototype complet

```csharp
String dim2ToString()
```

- #### Description détaillée
  Renvoie une chaîne contenant toutes les valeurs de tableau à deux dimension et les noms de colonne correspondants. La chaîne contient un format et peut être sortie directement sur la ligne de commande ou la zone de texte.  
- Au service de la méthode [`ToString()`](#pub-m-1)

Retourner au [**Méthode privée**](#private-method)
