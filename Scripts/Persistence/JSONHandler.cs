using System.IO;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace Apocryphon.Persistence
{
    public static class JSONHandler
    {
        public static void Save(
            JToken json,
            string filename,
            string savename
        )
        {
            string path = GetSavePath(savename);

            Directory.CreateDirectory(path);

            path = Path.Join(path, filename);

            SaveFromPath(json, path);
        }

        public static void Save(
            JToken json,
            string filename
        )
        {
            string path = Path.Join(Application.persistentDataPath, filename);

            SaveFromPath(json, path);
        }

        public static void SaveFromPath(JToken json, string path)
        {
            if (File.Exists(path)) File.Delete(path);

            using FileStream stream = File.Create(path);
            stream.Close();

            File.WriteAllText(path, json.ToString());
        }
        
        public static JToken Load(
            string filename,
            string savename
        )
        {
            string path = Path.Join(GetSavePath(savename), filename);
            
            return LoadFromPath(path);
        }

        public static JToken Load(
            string filename
        )
        {
            string path = Path.Join(Application.persistentDataPath, filename);
            
            return LoadFromPath(path);
        }

        public static bool CheckFileExists(string filename)
        {
            return File.Exists(
                Path.Join(Application.persistentDataPath, filename)
            );
        }

        public static JToken LoadFromPath(string path)
        {
            string stringData = File.ReadAllText(path);

            return JToken.Parse(stringData);
        }

        public static string GetSavePath(string savename)
        {
            return Path.Join(Application.persistentDataPath, "save", savename);
        }

        public static string GetSavesPath()
        {
            return Path.Join(Application.persistentDataPath, "save");
        }
    }
}