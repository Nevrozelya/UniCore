using UnityEditor;
using UnityEditor.PackageManager;
using UnityEngine;

namespace UniCore.Editor
{
    public static class ProjectSetupEditor
    {
        [MenuItem("UniCore/Setup/Folders And Packages", priority = 0)]
        public static void SetupFoldersAndPackages()
        {
            SetupFolders();
            SetupPackages();
        }

        [MenuItem("UniCore/Setup/Packages Only", priority = 2)]
        public static void SetupPackages()
        {
            string vContainerUrl = "https://github.com/hadashiA/VContainer.git?path=VContainer/Assets/VContainer";
            string uniTaskUrl = "https://github.com/Cysharp/UniTask.git?path=src/UniTask/Assets/Plugins/UniTask";
            string uniRxUrl = "https://github.com/neuecc/UniRx.git?path=Assets/Plugins/UniRx/Scripts";

            string[] urls = new[] { vContainerUrl, uniTaskUrl, uniRxUrl };
            Client.AddAndRemove(packagesToAdd: urls);
        }

        [MenuItem("UniCore/Setup/Folders Only", priority = 1)]
        public static void SetupFolders()
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
