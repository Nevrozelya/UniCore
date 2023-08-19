using CTA.Core.Utils;
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
                return default;
            }

            if (!File.Exists(path))
            {
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
                return;
            }

            if (value == null) // But we accept string.Empty!
            {
                return;
            }

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
                return;
            }

            if (value == null)
            {
                return;
            }

            if (JSON.TrySerialize(value, out string json))
            {
                await TryWriteAsync(path, json, token);
            }
            else
            {
                Logg.Error($"Failed to serialize file to {path}.", LOG);
            }
        }

        public static async UniTask<T> ReadAndParseAsync<T>(string path, CancellationToken token)
        {
            string json = await TryReadAsync(path, token);

            if (string.IsNullOrWhiteSpace(json))
            {
                return default;
            }

            if (JSON.TryParse(json, out T parsed))
            {
                return parsed;
            }
            else
            {
                Logg.Error($"Failed to parse file at {path}.", LOG);
                return default;
            }
        }

        public static bool TryDeleteDirectory(string path)
        {
            if (Directory.Exists(path))
            {
                try
                {
                    Directory.Delete(path, recursive: true);
                    return true;
                }
                catch (Exception e)
                {
                    Logg.Error($"Failed to delete directory at {path}, exception is: {e.Message}", LOG);
                }
            }
            return false;
        }

        public static bool TryDeleteFile(string path)
        {
            if (File.Exists(path))
            {
                try
                {
                    File.Delete(path);
                    return true;
                }
                catch (Exception e)
                {
                    Logg.Error($"Failed to delete file at {path}, exception is: {e.Message}", LOG);
                }
            }
            return false;
        }
    }
}
