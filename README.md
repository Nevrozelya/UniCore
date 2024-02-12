
# UniCore

## General

A project used to regroup common dependencies of Nevrozelya's projects, based on :

- **[Extenject](https://github.com/Mathijs-Bakker/Extenject)**
- **[UniTask](https://github.com/Cysharp/UniTask)**
- **[UniRx](https://github.com/neuecc/UniRx)**
- **[LINQ-to-GameObject-for-Unity](https://github.com/neuecc/LINQ-to-GameObject-for-Unity)**

## Installation

1) Import `UniCore` in Unity Packages Manager via git url : `https://github.com/Nevrozelya/UniCore.git?path=Assets`.
2) That's it!

## Update Newtonsoft dependency
- Import `com.unity.nuget.newtonsoft-json` via Unity Packages Manager.
- If `Assets/Dependencies/com.unity.nuget.newtonsoft-json@[version]` is older that the version downloaded at `Library/PackageCache/com.unity.nuget.newtonsoft-json@[version]`, replace it!
- If replaced, revert changes on `Packages/manifest.json` and `Packages/packages-lock.json` files.

---

## Manual libraries import

- VContainer : `https://github.com/hadashiA/VContainer.git?path=VContainer/Assets/VContainer#1.14.0`
- UniTask : `https://github.com/Cysharp/UniTask.git?path=src/UniTask/Assets/Plugins/UniTask`
- UniRx : `https://github.com/neuecc/UniRx.git?path=Assets/Plugins/UniRx/Scripts`
