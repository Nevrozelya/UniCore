using Cysharp.Threading.Tasks;
using System;
using System.IO;
using System.Threading;

namespace UniCore.Utils
{
    public static class FileUtils
    {
        private const string LOG = "FileUtils";

        public static async UniTask<string> TryReadAsync(string path, CancellationToken token)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                Logg.Warning("Given path is null or empty!", LOG);
                return default;
            }

            if (!File.Exists(path))
            {
                Logg.Warning($"File {path} doesn't exist!", LOG);
                return default;
            }

            try
            {
                return await File.ReadAllTextAsync(path, token);
            }
            catch (Exception e)
            {
                Logg.Error($"Failed to read file at {path}, exception is: {e.Message}", LOG);
                return default;
            }
        }

        public static async UniTask TryWriteAsync(string path, string value, CancellationToken token)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                Logg.Warning("Given path is null or empty!", LOG);
                return;
            }

            value ??= string.Empty;

            string directory = Path.GetDirectoryName(path);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            try
            {
                await File.WriteAllTextAsync(path, value, token);
            }
            catch (Exception e)
            {
                Logg.Error($"Failed to write file at {path}, exception is: {e.Message}", LOG);
            }
        }

        public static async UniTask SerializeAndWriteAsync<T>(string path, T value, CancellationToken token)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                Logg.Warning("Given path is null or empty!", LOG);
                return;
            }

            bool isSerialized = JSON.TrySerialize(value, out string json);

            // We also check for value != null because if null is passed, 
            // we still want to clear the file!
            if (!isSerialized && value != null)
            {
                Logg.Warning($"Failed to serialize file to {path}", LOG);
                return;
            }

            await TryWriteAsync(path, json, token);
        }

        public static async UniTask<T> ReadAndParseAsync<T>(string path, CancellationToken token)
        {
            string json = await TryReadAsync(path, token);

            if (string.IsNullOrWhiteSpace(json))
            {
                Logg.Warning($"Failed to read at path: {path}", LOG);
                return default;
            }

            if (JSON.TryParse(json, out T parsed))
            {
                return parsed;
            }
            else
            {
                Logg.Error($"Failed to parse file at {path}", LOG);
                return default;
            }
        }

        public static bool TryDeleteDirectory(string path)
        {
            if (!Directory.Exists(path))
            {
                Logg.Warning($"Directory {path} doesn't exist!", LOG);
                return false;
            }

            try
            {
                Directory.Delete(path, recursive: true);
                return true;
            }
            catch (Exception e)
            {
                Logg.Error($"Failed to delete directory at {path}, exception is: {e.Message}", LOG);
                return false;
            }
        }

        public static bool TryDeleteFile(string path)
        {
            if (!File.Exists(path))
            {
                Logg.Warning($"File {path} doesn't exist!", LOG);
                return false;
            }

            try
            {
                File.Delete(path);
                return true;
            }
            catch (Exception e)
            {
                Logg.Error($"Failed to delete file at {path}, exception is: {e.Message}", LOG);
                return false;
            }
        }
    }
}
