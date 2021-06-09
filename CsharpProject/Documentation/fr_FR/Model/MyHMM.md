# La classe 'MyHMM'

## Bref

Cette classe est à peu près la même que la classe "HMM", avec quelques modifications pour tenir compte des observations discrètes. Cette classe n'ayant pas été testée, elle est donc actuellement séparée.

Grâce à Gérérique ("Generic" en l'anglais), nous pouvons passer dans des tableaux de type 'int' ou 'double' pour exécuter les algorithmes correspondants. Si un tableau de type 'int' est passé dans la méthode comme observation, l'algorithme du HMM avec des observations discrètes est exécuté. C'est la même chose que pour la classe "HMM". Si un tableau de type 'double' est passé dans la méthode comme observation, l'algorithme du HMM avec observations continues est exécuté. Dans ce cas, la matrice d'émission doit stocker les paramètres de la distribution normale. Chaque ligne est un état, la première colonne de chaque ligne est la moyenne 'mu' et la deuxième colonne est la variance 'sigma^2'. Ainsi, pour obtenir la probabilité de l'état caché à l'état observé, la classe est mise à jour avec deux nouvelles méthodes.

| name                     | input                                         | output type | description         |
| ------------------------ | --------------------------------------------- | ----------- | ------------------- |
| `B<T>`                   | `int i,`<br>`T[] Obs`                         | `double`    | [Détail](#method-1) |
| `normalDistributionProb` | `double mu,`<br>`double sigma,`<br>`double X` | `double`    | [Détail](#method-2) |

On restructure également partiellement le code de l'algorithme de Baum-Welch en séparant les sections "Calcul des probabilités finales", "Re-éstimer la matrice de transition d'état" et "Re-éstimer la matrice d'émission". Quatre nouvelles méthodes ont été ajoutées.

| name                                          | input                                                                                     | output type | description         |
| --------------------------------------------- | ----------------------------------------------------------------------------------------- | ----------- | ------------------- |
| getProbObs                                    | `double[,] LogForward`                                                                    | `double`    | [Détail](#method-3) |
| ReEstimateTransProbs\<T>                      | `MyHMM hmm,`<br>`double probObs,`<br>`double forward,`<br>`double backward,`<br>`T[] obs` | `double[,]` | [Détail](#method-4) |
| ReEstimateEmissionProbs<br>\_ForDiscreteObs   | `MyHMM hmm,`<br>`double probObs,`<br>`double forward,`<br>`double backward,`<br>`T[] obs` | `double[,]` | [Détail](#method-5) |
| ReEstimateEmissionProbs<br>\_ForContinuousObs | `MyHMM hmm,`<br>`double probObs,`<br>`double forward,`<br>`double backward,`<br>`T[] obs` | `double[,]` | [Détail](#method-6) |

## Détail

---

### <code id="method-1">B(int,T)</code>

- #### Prototype complet
  ```csharp
  public double B<T>(int i, T k)
  ```
- #### Description détaillée
  Selon le type de l'observation `k`, on retourne la probabilité correspondante.   
  Si `T` est de type `int`, ça vaut dire que les observation sont discrètes, on retourne `b_{i}(k)`.   
  Si `T` est de type `double`, ça vaut dire que les observation sont continues. On prend `b_{i}(0)` comme la moyenne, prend `b_{i}(1)` comme la variance. On retourne la probabilité de la distribution normale quand X=k.
- #### Paramètre d'entrée
  - `i` : l'index d'état
  - `k` : la valeur d'observation

---

### <code id="method-2">normalDistributionProb(double,double,double)</code>

- #### Prototype complet
  ```csharp
  private double normalDistributionProb(double mu, double sigma, double X)
  ```
- #### Description détaillée
  Calculer la probabilité de la distribution normale.
- #### Paramètre d'entrée
  - `mu` : la moyenne
  - `sigma` : l'écart-type (la racine carré de la variance)
  - `X` : la valeur de la variable aléatoire

---

### <code id="method-3">getProbObs(double[,])</code>

- #### Prototype complet
  ```csharp
  public double getProbObs(double[,] LogForward)
  ```
- #### Description détaillée
  En utilisant le résultat de l'algo forward, on calcule la probabilité P(O|\lambda). (faire la somme des derniers \alpha )
- #### Paramètre d'entrée
  - `LogForward` : le résultat de l'algo forward

---

### <code id="method-4">ReEstimateTransProbs(MyHMM,double,double[,],double[,],T[])</code>

- #### Prototype complet
  ```csharp
  double[,] ReEstimateTransProbs<T>(MyHMM hmm, double probObservations, double[,] forward, double[,] backward, T[] observation)
  ```
- #### Description détaillée
  Re-éstimer la matrice de transition d'état.
- #### Paramètre d'entrée
  - `hmm` : l'ancien HMM
  - `probObservations` : la probabilité finiale
  - `forward` : le résultat de l'algo forward
  - `backward` : le résultat de l'algo backward
  - `observation` : la série des observations

---

### <code id="method-5">ReEstimateEmissionProbs_ForDiscreteObs(MyHMM,double,double[,],double[,],int[])</code>

- #### Prototype complet
  ```csharp
  double[,] ReEstimateEmissionProbs_ForDiscreteObs(MyHMM hmm, double probObservations, double[,] forward, double[,] backward, int[] observation)
  ```
- #### Description détaillée
  Re-éstimer la matrice d'émission avec les observations discrètes.
- #### Paramètre d'entrée
  - `hmm` : l'ancien HMM
  - `probObservations` : la probabilité finiale
  - `forward` : le résultat de l'algo forward
  - `backward` : le résultat de l'algo backward
  - `observation` : la série des observations discrètes

---

### <code id="method-6">ReEstimateEmissionProbs_ForContinuousObs(MyHMM,double,double[,],double[,],double[])</code>

- #### Prototype complet
  ```csharp
  double[,] ReEstimateEmissionProbs_ForContinuousObs(MyHMM hmm, double probObservations, double[,] forward, double[,] backward, double[] observation)
  ```
- #### Description détaillée
  Re-éstimer la matrice d'émission avec les observations continues.
- #### Paramètre d'entrée
  - `hmm` : l'ancien HMM
  - `probObservations` : la probabilité finiale
  - `forward` : le résultat de l'algo forward
  - `backward` : le résultat de l'algo backward
  - `observation` : la série des observations continues

---
