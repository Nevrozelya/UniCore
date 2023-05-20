
# UniCore

## General

A project used to regroup common dependencies of Nevrozelya's projects, based on :

- **[Extenject](https://github.com/Mathijs-Bakker/Extenject)**
- **[UniTask](https://github.com/Cysharp/UniTask)**
- **[UniRx](https://github.com/neuecc/UniRx)**
- **[LINQ-to-GameObject-for-Unity](https://github.com/neuecc/LINQ-to-GameObject-for-Unity)**

## Installation

1) Import `UniCore` in Unity Packages Manager via git url : `https://github.com/Nevrozelya/UniCore.git?path=Assets/Core`.

2) Add `"com.unity.nuget.newtonsoft-json": "3.2.1"` to your project's `manifest.json`.

3) Add
```
"com.unity.nuget.newtonsoft-json": {
      "version": "3.2.1",
      "depth": 0,
      "source": "registry",
      "dependencies": {},
      "url": "https://packages.unity.com"
    }
```
to your project's `packages-lock.json`.