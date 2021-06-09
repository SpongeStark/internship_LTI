# La classe `CsvHelper`

## Bref

### <div id="attribute">Attribut (privé)</div>

| nom    | type     | description       |
| ------ | -------- | ----------------- |
| `path` | `string` | chemin du fichier |

### <div id="constructor">Constructeur</div>

| nom                           | entrée        |
| ----------------------------- | ------------- |
| [`CsvHelper`](#constructor-1) | `string path` |

### <div id="public-method">Méthode publique</div>

| nom                                   | entrée                                 | type de retourner             | description                                                          |
| ------------------------------------- | -------------------------------------- | ----------------------------- | -------------------------------------------------------------------- |
| [`getDataByStreamReader`](#pub-m-1)   | `char splitChar`                       | [`RDataFrame`](RDataFrame.md) | lire le fichier \*.csv                                               |
| [`writeDataByStreamWriter`](#pub-m-2) | `RDataFrame data,`<br>`char splitChar` | `void`                        | écrire un objet [`RDataFrame`](RDataFrame.md) dans un fichier \*.csv |

### <div id="static-method">Méthode statique</div>

| nom                        | entrée                             | type de retourner             | description            |
| -------------------------- | ---------------------------------- | ----------------------------- | ---------------------- |
| [`getDataFrame`](#sta-m-1) | `string path`                      | [`RDataFrame`](RDataFrame.md) | lire le fichier \*.csv |
| [`getDataFrame`](#sta-m-2) | `string path,`<br>`char splitChar` | [`RDataFrame`](RDataFrame.md) | lire le fichier \*.csv |

### <div id="private-method">Méthode privée</div>

| nom                           | entrée                                  | type de retourner | description                                                                                              |
| ----------------------------- | --------------------------------------- | ----------------- | -------------------------------------------------------------------------------------------------------- |
| [`trimArrayString`](#pri-m-1) | `string[] str`                          | `string[]`        | exécuter `Trim()` pour chaque chaine de chractères dans un tableau de `string`, et retourner le résultat |
| [`prepareOneLine`](#pri-m-2)  | `string[] oneLine,`<br>`char splitChar` | `string`          | concaténérer un tableau de chaînes de caractères en une seule chaîne, en utilisant le séparateur.        |

---

## Détail

---

### <code id="constructor-1">CsvHelper(string)</code>

- #### Prototype complet
  ```csharp
  public CsvHelper(string path)
  ```
- #### Description détaillée
  instancier un objet de `CsvHelper`.
- #### Paramètre d'entrée
  - `path` : le chemin du fichier
- #### 🌰 Exemple d'usage
  ```csharp
  string path = @"/Users/yangkai/Desktop/data.csv";
  CsvHelper myHelper = new CsvHelper(path);
  ```

Retourner au [**Constructor**](#constructor)

---

### <code id="pub-m-1">getDataByStreamReader(char)</code>

- #### Prototype complet
  ```csharp
  public RDataFrame getDataByStreamReader(char splitChar)
  ```
- #### Description détaillée
  Lire le fichier, et retourner le contenue en format d'un objet `RDataFrame`.
- #### Paramètre d'entrée
  - `splitChar` : le séparateur entre les valeurs dans une seule ligne
- #### 🌰 Exemple d'usage
  ```csharp
  CsvHelper myHelper = new CsvHelper(@"/Users/yangkai/Desktop/data.csv");
  RDataFrame myDataFrame = myHelper.getDataByStreamReader(',');
  Console.WriteLine(myDataFrame);
  ```

Retourner au [**Méthode publique**](#public-method)

---

### <code id="pub-m-2">writeDataByStreamWriter(RDataFrame,char)</code>

- #### Prototype complet
  ```csharp
  public void writeDataByStreamWriter(RDataFrame data, char splitChar)
  ```
- #### Description détaillée
  Écrire le conteue d'un objet `RDataFrame` dans le fichier, et séparer les donners dans une ligne en utilisant `splitChar`.
- #### Paramètre d'entrée
  - `data` : les données
  - `splitChar` : le séparateur entre les valeurs dans une seule ligne
- #### 🌰 Exemple d'usage
  ```csharp
  CsvHelper myHelper = new CsvHelper(@"/Users/yangkai/Desktop/data.csv");
  RDataFrame myData = ...... ; //créer et construire les données
  myHelper.writeDataByStreamWriter(myData, ',');
  ```

Retourner au [**Méthode publique**](#public-method)

---

### <code id="sta-m-1">getDataFrame(string)</code>

- #### Prototype complet
  ```csharp
  public static RDataFrame getDataFrame(string path)
  ```
- #### Description détaillée
  Lire le fichier dont le chemin est `path`, et retourner le contenue sous la forme d'un objet de `RDataFrame`. Les valeurs dans une ligne sont séparées par défault par `','`.
- #### Paramètre d'entrée
  - `path` : le chemin du fichier
- #### 🌰 Exemple d'usage
  ```csharp
  string path = @"/Users/yangkai/Desktop/data.csv";
  RDataFrame myData = CsvHelper.getDataFrame(path);
  Console.WriteLine(myData);
  ```

Retourner au [**Méthode statique**](#static-method)

---

### <code id="sta-m-2">getDataFrame(string,char)</code>

- #### Prototype complet
  ```csharp
  public static RDataFrame getDataFrame(string path, char splitChar)
  ```
- #### Description détaillée
  Lire le fichier dont le chemin est `path`, et retourner le contenue sous la forme d'un objet de `RDataFrame`. Les valeurs dans une ligne sont séparées par `splitChar`.
- #### Paramètre d'entrée
  - `path` : le chemin du fichier
  - `splitChar` : le séparateur entre les valeurs dans une seule ligne
- #### 🌰 Exemple d'usage
  ```csharp
  string path = @"/Users/yangkai/Desktop/data.csv";
  RDataFrame myData = CsvHelper.getDataFrame(path, ';');//Les valeurs dans une ligne sont séparées par point-vergule
  Console.WriteLine(myData);
  ```

Retourner au [**Méthode statique**](#static-method)

---

### <code id="pri-m-1">trimArrayString(string[])</code>

- #### Prototype complet
  ```csharp
  private string[] trimArrayString(string[] str)
  ```
- #### Description détaillée
  Prendre chaque chaîne de charactère d'un tableau de `string`, supprimer les espaces en tête et en queue, et renvoie le résultat.
- Au service de la méthode [`getDataByStreamReader`](#pub-m-1)。
- #### Paramètre d'entrée
  - `str` : le tableau de chaîne de charactère qui doit être traité

Retourner au[**Méthode privée**](#private-method)

---

### <code id="pri-m-1">prepareOneLine(string[],char)</code>

- #### Prototype complet
  ```csharp
  private string prepareOneLine(string[] oneLine, char splitChar)
  ```
- #### Description détaillée
  Concaténérer un tableau de chaînes de caractères en une seule chaîne, en utilisant le séparateur.
- Au service de la méthode [`writeDataByStreamWriter`](#pub-m-2)。
- #### Paramètre d'entrée
  - `oneLine` : l'objet du tableau de chaîne de charactère qui doit être prétraité
  - `splitChar` : le séparateur entre les valeurs dans une seule ligne

Retourner au[**Méthode privée**](#private-method)
