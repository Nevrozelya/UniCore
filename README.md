# UniCore

## General

Some common usage features that are embeded here to be easily re-used.
It also contains LinqToGameObjects because it is not importable via UPM.

- **[LINQ-to-GameObject-for-Unity](https://github.com/neuecc/LINQ-to-GameObject-for-Unity)**

## Installation

1) Import `UniCore` in Unity Packages Manager via git url : `https://github.com/Nevrozelya/UniCore.git?path=Assets`.
2) Within the Unity Editor toolbar, click on `UniCore > Setup > Packages Only`.
3) That's it!

> *It is also possible to directly use `UniCore > Setup > Folders And Packages` option to use my folders structure.*

## Manual libraries import

- **[VContainer](https://github.com/hadashiA/VContainer)** : `https://github.com/hadashiA/VContainer.git?path=VContainer/Assets/VContainer`
- **[UniTask](https://github.com/Cysharp/UniTask)** : `https://github.com/Cysharp/UniTask.git?path=src/UniTask/Assets/Plugins/UniTask`
- **[UniRx](https://github.com/neuecc/UniRx)** : `https://github.com/neuecc/UniRx.git?path=Assets/Plugins/UniRx/Scripts`

- **[Extenject](https://github.com/Mathijs-Bakker/Extenject)** *(for older projects only)* : `https://github.com/Mathijs-Bakker/Extenject.git?path=UnityProject/Assets/Plugins/Zenject/Source`

## Update Newtonsoft dependency
- Import `com.unity.nuget.newtonsoft-json` via Unity Packages Manager.
- If `Assets/Dependencies/com.unity.nuget.newtonsoft-json@[version]` is older that the version downloaded at `Library/PackageCache/com.unity.nuget.newtonsoft-json@[version]`, replace it!
- If replaced, revert changes on `Packages/manifest.json` and `Packages/packages-lock.json` files.