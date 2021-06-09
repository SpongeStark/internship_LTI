# <div id="delegate">La délégation `OneTimeFinish`</div>

- Prototype complet
  ```csharp
  public delegate void OneTimeFinish(int time);
  ```
- description détaillée  
  C'est une délégation, dont l'entrée est un entirt, et ne retourne rien.

# La classe `HMM`

## Bref

### <div id="attribute">Attribut (privé)</div>

| nom             | type       | description                                               |
| --------------- | ---------- | --------------------------------------------------------- |
| `numStates`     | `int`      | nombre des états cachés                                   |
| `numSymbols`    | `int`      | nombre des états d'observation                            |
| `states`        | `string[]` | nom des états cachés                                      |
| `symbols`       | `string[]` | nom des états d'observation                               |
| `startProbs`    | `double[]` | la matrice de probabilités initiales                      |
| `transProbs`    | `double[]` | la matrice de probabilités de transition des états cachés |
| `emissionProbs` | `double[]` | la matrice de probabilités d'émission                     |

### <div id="property">Propriété (publique)</div>

| nom                     | type       | description                                               |
| ----------------------- | ---------- | --------------------------------------------------------- |
| `NumStates { get }`     | `int`      | nombre des états cachés                                   |
| `NumSymbols { get }`    | `int`      | nombre des états d'observation                            |
| `States { get }`        | `string[]` | nom des états cachés                                      |
| `Symbols { get }`       | `string[]` | nom des états d'observation                               |
| `StartProbs { get }`    | `double[]` | la matrice de probabilités initiales                      |
| `TransProbs { get }`    | `double[]` | la matrice de probabilités de transition des états cachés |
| `EmissionProbs { get }` | `double[]` | la matrice de probabilités d'émission                     |

### <div id="constructor">Constructeur</div>

| nom                     | entrée                                                                                                                      |
| ----------------------- | --------------------------------------------------------------------------------------------------------------------------- |
| [`HMM`](#constructor-1) | `int numStates,`<br>`int numSymbols`                                                                                        |
| [`HMM`](#constructor-2) | `string[] states,`<br>`string[] symbols`                                                                                    |
| [`HMM`](#constructor-3) | `string[] states,`<br>`string[] symbols,`<br>`double[] startProbs`                                                          |
| [`HMM`](#constructor-4) | `string[] states,`<br>`string[] symbols,`<br>`double[] startProbs,`<br>`double[,] transProbs,`<br>`double[,] emissionProbs` |

### <div id="public-method">Méthode publique</div>

| nom                            | entrée                                                                               | type de retourne      | description                                                                       |
| ------------------------------ | ------------------------------------------------------------------------------------ | --------------------- | --------------------------------------------------------------------------------- |
| [`getForwardTable`](#pub-m-1)  | `int[] observations`                                                                 | [`RArray`](RArray.md) | le résultat du tableau de alpha-pass obtenue par l'algorithme forward             |
| [`getBackwordTable`](#pub-m-2) | `int[] observations`                                                                 | [`RArray`](RArray.md) | le résultat du tableau de beta-pass obtenue par l'algorithme backward             |
| [`baumWelch`](#pub-m-3)        | `int[] observations`                                                                 | `HMM`                 | retourner le nouveau HMM obtenue par l'algorithme Baum-Welch                      |
| [`baumWelch`](#pub-m-4)        | `int[] observations,`<br>`int maxIterations`                                         | `HMM`                 | retourner le nouveau HMM obtenue par l'algorithme Baum-Welch                      |
| [`baumWelch`](#pub-m-5)        | `int[] observations,`<br>`delegate doSth`                                            | `HMM`                 | retourner le nouveau HMM obtenue par l'algorithme Baum-Welch, et exécuter `doSth` |
| [`baumWelch`](#pub-m-6)        | `int[] observations,`<br>`int maxIterations,`<br>`delegate doSth`                    | `HMM`                 | retourner le nouveau HMM obtenue par l'algorithme Baum-Welch, et exécuter `doSth` |
| [`baumWelch`](#pub-m-7)        | `int[] observations,`<br>`int maxIterations,`<br>`double delta,`<br>`delegate doSth` | `HMM`                 | retourner le nouveau HMM obtenue par l'algorithme Baum-Welch, et exécuter `doSth` |
| [`getViterbiPath`](#pub-m-8)   | `int[] observations`                                                                 | [`RArray`](RArray.md) | retourner le chemin des états cachés obtenue l'algorithme Viterbi                 |
| [`ToString`](#pub-m-9)         | `void`                                                                               | `String`              | récrire la méthode `ToString()` de la classe père                                 |

### <div id="private-method">Méthode privée</div>

| nom                              | entrée                                                       | type de retourne | description                                                                         |
| -------------------------------- | ------------------------------------------------------------ | ---------------- | ----------------------------------------------------------------------------------- |
| [`forward`](#pri-m-1)            | `int[] observations`                                         | `double[,]`      | le résultat logarithmique du tableau de alpha-pass obtenue par l'algorithme forward |
| [`backward`](#pri-m-2)           | `int[] observations`                                         | `double[,]`      | le résultat logarithmique du tableau de beta-pass obtenue par l'algorithme backward |
| [`baumWelchRecursion`](#pri-m-3) | `HMM hmm,`<br>`int[] observations`                           | `HMM`            | une fois de mettre à jour pour l'algorithme Baum-Wemch                              |
| [`normalizeMatrx`](#pri-m-4)     | `double[,] matrix`                                           | `double[,]`      | normaliser la matrice d'entrée et la retourner                                      |
| [`updateModel`](#pri-m-5)        | `double[,] transitionMatrix,`<br> `double[,] emissionMatrix` | `bool`           | mettre à jour la matrice de transition et la matrice d'émission                     |
| [`updateModel`](#pri-m-6)        | `HMM newHMM`                                                 | `bool`           | mettre à jour la matrice de transition et la matrice d'émission                     |
| [`diffMatrix`](#pri-m-7)         | `double[,] matrix1,`<br> `double[,] matrix2`                 | `double`         | calculer la différence entre deux matrice                                           |
| [`viterbi`](#pri-m-8)            | `int[] observations`                                         | `int[]`          | retourner l'index du chemin des états cachés obtenue l'algorithme Viterbi           |

---

## Détail

---

### <code id="constructor-1">HMM(int,int)</code>

- #### Prototype complet
  ```csharp
  public HMM(int numStates, int numSymbols)
  ```
- #### Description détaillée
  Inctancier une `HMM`, dont toutes les valeurs de la matrice initial et de la matrice de transition sont "1/numStates", et toutes les valeurs de la matrice d'émission sont "1/numSymbols". 
- #### Paramètre d'entrée
  - `numStates` : nombre d'états cachés
  - `numSymbols` : nombre d'états d'observation
- #### 🌰 Exemple d'usage
  ```csharp
  HMM hmm = new HMM(2,3);
  ```

Retourner au [**Constructor**](#constructor)

---

### <code id="constructor-2">HMM(string[],string[])</code>

- #### Prototype complet
  ```csharp
  public HMM(string[] states, string[] symbols)
  ```
- #### Description détaillée
  Inctancier une `HMM`, dont toutes les valeurs de la matrice initial et la matrice de transition sont "1/numStates", et toutes les valeurs de la matrice d'émission sont "1/numSymbols". 
- #### Paramètre d'entrée
  - `states` : nom d'états cachés
  - `symbols` : nom d'états d'observation
- #### 🌰 Exemple d'usage
  ```csharp
  string[] states = new string[] { "q0", "q1" };
  string[] symbols = new string[] { "v0", "v1", "v2" };
  HMM hmm = new HMM(states, symbols);
  ```

Retourner au [**Constructor**](#constructor)

---

### <code id="constructor-3">HMM(string[],string[],double[])</code>

- #### Prototype complet
  ```csharp
  public HMM(string[] states, string[] symbols, double[] startProbs)
  ```
- #### Description détaillée
  Inctancier une `HMM`, dont toutes les valeurs de la matrice transition sont "1/numStates", et toutes les valeurs de la matrice d'émission sont "1/numSymbols". On assigne les valeurs de la matrice initial par la paramètre d'entrée.
- #### Paramètre d'entrée
  - `states` : nom d'états cachés
  - `symbols` : nom d'états d'observation
  - `startProbs` : matrice de probabilités des états initials
- #### 🌰 Exemple d'usage
  ```csharp
  string[] states = new string[] { "q0", "q1" };
  string[] symbols = new string[] { "v0", "v1", "v2" };
  double[] startProb = new double[] { 0.3, 0.7 };
  HMM hmm = new HMM(states, symbols, startProb);
  ```

Retourner au [**Constructor**](#constructor)

---

### <code id="constructor-4">HMM(string[],string[],double[],double[,],double[,])</code>

- #### Prototype complet
  ```csharp
  public HMM(string[] states, string[] symbols, double[] startProbs, double[,] transProbs, double[,] emissionProbs)
  ```
- #### Description détaillée
  Inctancier une `HMM`, et assigner la matrice initial, la matrice de transition et la matrice d'émission, par les paramètres d'entrée.
- #### Paramètre d'entrée
  - `states` : nom d'états cachés
  - `symbols` : nom d'états d'observation
  - `startProbs` : matrice de probabilités des états initials
  - `transProbs` : matrice de probabilités des états cachés
  - `emissionProbs` : matrice de probabilités des états d'observation
- #### 🌰 Exemple d'usage
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

Retourner au [**Constructor**](#constructor)

---

### <code id="pub-m-1">getForwardTable(int[])</code>

- #### Prototype complet
  ```csharp
  public RArray getForwardTable(int[] observation)
  ```
- #### Description détaillée
  Retourner un tableau à deux dimensions de alpha-pass (\alpha_t(i)).
- #### Paramètre d'entrée
  - `observation` : la série d'observation discrète (à partir de 0)
- #### 🌰 Exemple d'usage
  ```csharp
  //instancier HMM
  HMM hmm = new HMM(......);
  //la série d'observation
  int[] obs = new int[] { 0,1,0,2 };
  //exécuter l'algorithme forward
  RArray forwardTable = hmm.getForwardTable(obs);
  //afficher le résultat dans console
  Console.WriteLine(forwardTable);
  ```

Retourner au [**Méthode publique**](#public-method)

---

### <code id="pub-m-2">getBackwordTable(int[])</code>

- #### Prototype complet
  ```csharp
  public RArray getBackwordTable(int[] observation)
  ```
- #### Description détaillée
  Retourner un tableau à deux dimensions de beta-pass (\beta_t(i)).
- #### Paramètre d'entrée
  - `observation` : la série d'observation discrète (à partir de 0)    
- #### 🌰 Exemple d'usage
  ```csharp
  //instancier HMM
  HMM hmm = new HMM(......);
  //la série d'observation
  int[] obs = new int[] { 0,1,0,2 };
  //exécuter l'algorithme backward
  RArray backwordTable = hmm.getBackwordTable(obs);
  //afficher le résultat dans console
  Console.WriteLine(backwordTable);
  ```

Retourner au [**Méthode publique**](#public-method)

---

### <code id="pub-m-3">baumWelch(int[])</code>

- #### Prototype complet
  ```csharp
  public HMM baumWelch(int[] observation)
  ```
- #### Description détaillée 
  Retourner l'instance de `HMM` après exécuter l'algorithme Baum-Welch. L'itération maximale est par défault 100 fois, et si la différence d'un mèttre à jour est inférieur à 1e-9, il s'arrête.
- #### Paramètre d'entrée
  - `observation` : la série d'observation discrète (à partir de 0)    
- #### 🌰 Exemple d'usage
  ```csharp
  //instancier HMM
  HMM hmm = new HMM(......);
  //la série d'observation
  //Il faut théoriquement beaucoup plus d'observation pour obtenir un bon résultat. Ici est seulement un exemple
  int[] obs = new int[] { 0,1,0,2 };
  //exécuter l'algorithme Baum-Welch
  HMM newHMM = hmm.baumWelch(obs);
  //afficher le résultat dans console
  Console.WriteLine(HMM);
  ```

Retourner au [**Méthode publique**](#public-method)

---

### <code id="pub-m-4">baumWelch(int[],int)</code>

- #### Prototype complet
  ```csharp
  public HMM baumWelch(int[] observation, int maxIterations)
  ```
- #### Description détaillée 
  Retourner l'instance de `HMM` après exécuter l'algorithme Baum-Welch. L'itération maximale est `maxIterations` fois, et si la différence d'un mèttre à jour est inférieur à 1e-9, il s'arrête.
- #### Paramètre d'entrée
  - `observation` : la série d'observation discrète (à partir de 0)    
  - `maxIterations` : numbre maximale d'itération de mettre à jour
- #### 🌰 Exemple d'usage
  ```csharp
  //instancier HMM
  HMM hmm = new HMM(......);
  //la série d'observation
  //Il faut théoriquement beaucoup plus d'observation pour obtenir un bon résultat. Ici est seulement un exemple
  int[] obs = new int[] { 0,1,0,2 };
  //exécuter l'algorithme Baum-Welch (999 fois MAX de mettre à jour)
  HMM newHMM = hmm.baumWelch(obs, 999);
  //afficher le résultat dans console
  Console.WriteLine(HMM);
  ```

Retourner au [**Méthode publique**](#public-method)

---

### <code id="pub-m-5">baumWelch(int[],delegate)</code>

- #### Prototype complet
  ```csharp
  public HMM baumWelch(int[] observation, OneTimeFinish doSomething)
  ```
- #### Description détaillée
  Retourner l'instance de `HMM` après exécuter l'algorithme Baum-Welch. L'itération maximale est par défault 100 fois, et si la différence d'un mèttre à jour est inférieur à 1e-9, il s'arrête. Après chaque mis à jour, on exécute l'instance de la délégation `doSomething`.
- #### Paramètre d'entrée
  - `observation` : la série d'observation discrète (à partir de 0)    
  - `doSomething` : l'instance de [la délégation `OneTimeFinish`](#delegate) que l'on exécute apràs chaque mis à jour.
- #### 🌰 Exemple d'usage
  ```csharp
  //instancier HMM
  HMM hmm = new HMM(......);
  //la série d'observation
  //Il faut théoriquement beaucoup plus d'observation pour obtenir un bon résultat. Ici est seulement un exemple
  int[] obs = new int[] { 0,1,0,2 };
  OneTimeFinish myDelegate = new OneTimeFinish(
    (i) => { Console.WriteLine(i + "times"); }
  );
  //exécuter l'algorithme Baum-Welch et afficher le nombre de fois courant après chaque mis à jour
  HMM newHMM = hmm.baumWelch(obs, myDelegate);
  //afficher le résultat dans console
  Console.WriteLine(HMM);
  ```

Retourner au [**Méthode publique**](#public-method)

---

### <code id="pub-m-6">baumWelch(int[],int,delegate)</code>

- #### Prototype complet
  ```csharp
  public HMM baumWelch(int[] observation, int maxIterations, OneTimeFinish doSomething)
  ```
- #### Description détaillée
  Retourner l'instance de `HMM` après exécuter l'algorithme Baum-Welch. L'itération maximale est `maxIterations` fois, et si la différence d'un mèttre à jour est inférieur à 1e-9, il s'arrête. Après chaque mis à jour, on exécute l'instance de la délégation `doSomething`.
- #### Paramètre d'entrée
  - `observation` : la série d'observation discrète (à partir de 0)    
  - `maxIterations` : numbre maximale d'itération de mettre à jour
  - `doSomething` : l'instance de [la délégation `OneTimeFinish`](#delegate) que l'on exécute apràs chaque mis à jour.
- #### 🌰 Exemple d'usage
  ```csharp
  //instancier HMM
  HMM hmm = new HMM(......);
  //la série d'observation
  //Il faut théoriquement beaucoup plus d'observation pour obtenir un bon résultat. Ici est seulement un exemple
  int[] obs = new int[] { 0,1,0,2 };
  OneTimeFinish myDelegate = new OneTimeFinish(
    (i) => { Console.WriteLine(i + " times"); }
  );
  //exécuter l'algorithme Baum-Welch 
  //999 fois MAX de mettre à jour, et afficher le nombre de fois courant après chaque mis à jour
  HMM newHMM = hmm.baumWelch(obs, 999, myDelegate);
  //afficher le résultat dans console
  Console.WriteLine(HMM);
  ```

Retourner au [**Méthode publique**](#public-method)

---

### <code id="pub-m-7">baumWelch(int[],int,double,delegate)</code>

- #### Prototype complet
  ```csharp
  public HMM baumWelch(int[] observation, int maxIterations, double delta, OneTimeFinish doSomething)
  ```
- #### Description détaillée
  Retourner l'instance de `HMM` après exécuter l'algorithme Baum-Welch. L'itération maximale est `maxIterations` fois, et si la différence d'un mèttre à jour est inférieur à `delta`, il s'arrête. Après chaque mis à jour, on exécute l'instance de la délégation `doSomething`.
- #### Paramètre d'entrée
  - `observation` : la série d'observation discrète (à partir de 0)    
  - `maxIterations` : numbre maximale d'itération de mettre à jour
  - `delta` : la différence minimale de convergence
  - `doSomething` : l'instance de [la délégation `OneTimeFinish`](#delegate) que l'on exécute apràs chaque mis à jour.
- #### 🌰 Exemple d'usage
  ```csharp
  //instancier HMM
  HMM hmm = new HMM(......);
  //la série d'observation
  //Il faut théoriquement beaucoup plus d'observation pour obtenir un bon résultat. Ici est seulement un exemple
  int[] obs = new int[] { 0,1,0,2 };
  OneTimeFinish myDelegate = new OneTimeFinish(
    (i) => { Console.WriteLine(i + " times"); }
  );
  //exécuter l'algorithme Baum-Welch 
  //999 fois MAX de mettre à jour, et afficher le nombre de fois courant après chaque mis à jour
  //si la différence d'un mèttre à jour est inférieur à 1e-10, il s'arrête
  HMM newHMM = hmm.baumWelch(obs, 999, 1e-10, myDelegate);
  //afficher le résultat dans console
  Console.WriteLine(HMM);
  ```

Retourner au [**Méthode publique**](#public-method)

---

### <code id="pub-m-8">getViterbiPath(int[])</code>

- #### Prototype complet
  ```csharp
  public RArray getViterbiPath(int[] observation)
  ```
- #### Description détaillée 
  D'après cette instance `HMM`, on exécute l'algorithme Vitervi, et obtient le chemin des états cachés.
- #### Paramètre d'entrée
  - `observation` : la série d'observation discrète (à partir de 0)    
- #### 🌰 Exemple d'usage
  ```csharp
  //instancier HMM
  HMM hmm = new HMM(......);
  //la série d'observation
  int[] obs = new int[] { 0,1,0,2 };
  //exécuter l'algorithme Vitervi
  RArray hidenStates = hmm.getViterbiPath(obs);
  //afficher le résultat dans console
  Console.WriteLine(hidenStates);
  ```

Retourner au [**Méthode publique**](#public-method)

---

### <code id="pub-m-9">ToString()</code>

- #### Prototype complet
  ```csharp
  public override String ToString()
  ```
- #### Description détaillée 
  Sortir toute l'information de cette instance.
- #### 🌰 Exemple d'usage
  ```csharp
  //instancier HMM
  HMM hmm = new HMM(......);
  //afficher le résultat dans console
  Console.WriteLine(hmm);
  ```

Retourner au [**Méthode publique**](#public-method)

---

### <code id="pri-m-1">forward(int[])</code>

- #### Prototype complet
  ```csharp
  double[,] forward(int[] observation)
  ```
- #### Description détaillée 
  Exécuter l'algorithme forward, et retourner les valeurs logarithmiques.
- Au service de la méthode [`getForwardTable`](#pub-m-1)。
- #### Paramètre d'entrée
  - `observation` : la série d'observation discrète (à partir de 0)    

Retourner au [**Méthode privée**](#private-method)

---

### <code id="pri-m-2">backward(int[])</code>

- #### Prototype complet
  ```csharp
  double[,] backward(int[] observation)
  ```
- #### Description détaillée
  Exécuter l'algorithme backward, et retourner les valeurs logarithmiques.
- Au service de la méthode [`getBackwardTable`](#pub-m-2)。
- #### Paramètre d'entrée
  - `observation` : la série d'observation discrète (à partir de 0)    

Retourner au [**Méthode privée**](#private-method)

---

### <code id="pri-m-3">baumWelchRecursion(HMM,int[])</code>

- #### Prototype complet
  ```csharp
  HMM baumWelchRecursion(HMM hmm, int[] observation)
  ```
- #### Description détaillée 
  Exécuter une fois l'algorithme Baum-Welch et mettre à jour l'ancienne instance `HMM`, retrouner la nouvelle instance.
- Au service de la méthode [`baumWelch`](#pub-m-7)。
- #### Paramètre d'entrée
  - `hmm` : l'ancienne instance de `HMM`
  - `observation` : la série d'observation discrète (à partir de 0)    

Retourner au [**Méthode privée**](#private-method)

---

### <code id="pri-m-4">normalizeMatrx(double[,])</code>

- #### Prototype complet
  ```csharp
  double[,] normalizeMatrx(double[,] matrix)
  ```
- #### Description détaillée 
  Normaliser une matrice pour que la somme de chaque ligne est 1.
- Au service de la méthode [`baumWelch`](#pub-m-7)。
- #### Paramètre d'entrée
  - `matrix` : la matrice qui doit être normalisé

Retourner au [**Méthode privée**](#private-method)

---

### <code id="pri-m-5">diffMatrix(double[,],double[,])</code>

- #### Prototype complet
  ```csharp
  bool updateModel(double[,] transitionMatrix, double[,] emissionMatrix)
  ```
- #### Description détaillée 
  Mettre à jour son matrice de transition et son matrice d'émission.
- Au service de la méthode [`baumWelch`](#pub-m-7)。
- #### Paramètre d'entrée
  - `transitionMatrix` : la nouvelle matrice de transition
  - `emissionMatrix` : la nouvelle matrice d'émission

Retourner au [**Méthode privée**](#private-method)

---

### <code id="pri-m-6">diffMatrix(double[,],double[,])</code>

- #### Prototype complet
  ```csharp
  bool updateModel(HMM newModel)
  ```
- #### Description détaillée
  Mettre à jour son matrice de transition et son matrice d'émission.
- Au service de la méthode [`baumWelch`](#pub-m-7)。
- #### Paramètre d'entrée
  - `newHMM` : la nouvelle instance de `HMM`

Retourner au [**Méthode privée**](#private-method)

---

### <code id="pri-m-7">diffMatrix(double[,],double[,])</code>

- #### Prototype complet
  ```csharp
  double diffMatrix(double[,] T1, double[,] T2)
  ```
- #### Description détaillée 
  Calculer la différence de deux matrice (la somme des carrés de la différence de chaque deux valeurs homologues)  
  Si deux matrices n'ont pas la même dimension ou taille, on retourne -1.
- Au service de la méthode [`baumWelch`](#pub-m-7)。
- #### Paramètre d'entrée
  - `T1` : une matrice
  - `T2` : une autre matrice

Retourner au [**Méthode privée**](#private-method)

---

### <code id="pri-m-8">viterbi(int[])</code>

- #### Prototype complet
  ```csharp
  int[] viterbi(int[] observation)
  ```
- #### Description détaillée 
  Exécuter l'algorithme Viterbi selon la série d'obsertation.  
  Retourner les index de chemin d'état caché
- Au service de la méthode [`getViterbiPath`](#pub-m-8)。
- #### Paramètre d'entrée
  - `observation` : la série d'observation discrète (à partir de 0)    

Retourner au [**Méthode privée**](#private-method)

---
