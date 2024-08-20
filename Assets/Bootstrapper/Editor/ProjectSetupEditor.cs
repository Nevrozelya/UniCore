using UnityEditor;
using UnityEditor.PackageManager;
using UnityEngine;

namespace UniCore.Editor
{
    public static class ProjectSetupEditor
    {
        [MenuItem("UniCore/Setup")]
        public static void Setup()
        {
            SetupFolders();
            SetupPackages();
        }

        [MenuItem("UniCore/Packages Only")]
        private static void SetupPackages()
        {
            string[] packagesToAdd = PackagesToAdd();
            string[] packagesToRemove = PackagesToRemove();
            Client.AddAndRemove(packagesToAdd, packagesToRemove);
        }

        private static string[] PackagesToAdd()
        {
            string newtonsoftName = "com.unity.nuget.newtonsoft-json";
            string vContainerUrl = "https://github.com/hadashiA/VContainer.git?path=VContainer/Assets/VContainer";
            string uniTaskUrl = "https://github.com/Cysharp/UniTask.git?path=src/UniTask/Assets/Plugins/UniTask";
            string uniRxUrl = "https://github.com/neuecc/UniRx.git?path=Assets/Plugins/UniRx/Scripts";
            string unicoreUrl = "https://github.com/Nevrozelya/UniCore.git?path=Assets/Runtime";

            string[] urls = new[] { newtonsoftName, vContainerUrl, uniTaskUrl, uniRxUrl, unicoreUrl };
            return urls;
        }

        private static string[] PackagesToRemove()
        {
            string unicoreBootstrapper = "com.nevrozelya.unicore-bootstrapper";
            string visualScripting = "com.unity.visualscripting";
            string collab = "com.unity.collab-proxy";
            string timeline = "com.unity.timeline";
            string test = "com.unity.test-framework";
            string rider = "com.unity.ide.rider";
            string aiNav = "com.unity.ai.navigation";

            string[] urls = new[] { unicoreBootstrapper, visualScripting, collab, timeline, test, rider, aiNav };
            return urls;
        }

        [MenuItem("UniCore/Folders Only")]
        private static void SetupFolders()
        {
            // Delete useless assets
            DeleteFolder("TutorialInfo");
            AssetDatabase.DeleteAsset("Assets/Readme.asset");

            // Create Needed folders
            CreateFolder("Core");
            CreateFolder("Data");
            CreateFolder("Editor");

            // Move Settings & Scenes folders
            MoveFolder("Settings", "Data");
            MoveFolder("Scenes", "Data");
        }

        private static void CreateFolder(string folderName, string rootName = "Assets")
        {
            string endPath = $"{rootName}/{folderName}";

            if (AssetDatabase.IsValidFolder(endPath))
            {
                Debug.LogError($"{endPath} folder already exists!");
                return;
            }

            string error = AssetDatabase.CreateFolder(rootName, folderName);

            if (!string.IsNullOrWhiteSpace(error))
            {
                Debug.LogError($"Failed to create {endPath} : {error}");
            }
        }

        private static void MoveFolder(string folderName, string targetName, string rootName = "Assets")
        {
            string sourcePath = $"{rootName}/{folderName}";

            if (AssetDatabase.IsValidFolder(sourcePath))
            {
                string destinationPath = $"{rootName}/{targetName}/{folderName}";
                string error = AssetDatabase.MoveAsset(sourcePath, destinationPath);

                if (!string.IsNullOrWhiteSpace(error))
                {
                    Debug.LogError($"Failed to move {sourcePath} : {error}");
                }
            }
        }

        private static void DeleteFolder(string folderName, string rootName = "Assets")
        {
            string path = $"{rootName}/{folderName}";

            if (AssetDatabase.IsValidFolder(path))
            {
                bool isDeleted = AssetDatabase.DeleteAsset(path);

                if (!isDeleted)
                {
                    Debug.LogError($"Failed to delete {path}");
                }
            }
        }
    }
}
