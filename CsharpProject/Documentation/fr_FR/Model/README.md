# Projet `Model`

C'est un projet pour réaliser le model HMM sous C#, selon le librairie de HMM dans R.

Ce projet contient 4 fichiers, ça vaut dire 4 classes.
- [`RArray`](RArray.md) : Pour le tableau de type `double` à une ou deux dimension, avec les clés. On peut obtiens la valeur selon la clé. 
- [`RDataFrame`](RDataFrame.md) : Ressemble avec `DataFrame` sous R，sauf on ajoute un curseur pour lire chaque ligne.
- [`CsvHelper`](CsvHelper.md) : Pour lire le fichier `*.csv`, ou le fichier `*.txt` dont le format de contenue est csv.
- [`HMM`](HMM.md) : Le model "HMM" et les trois algorithme essentiels.

Par d'ailleurs, il y a encore une classe s'appelle [MyHMM](MyHMM.md). Cette classe est pour s'adapter le HMM avec les observations continues. Cependant, il n'est pas encore testé. 
