using System.IO;
using UnityEngine;

namespace SaveSystem
{
    public class JsonSaveSystem : ISaveSystem
    {
        private readonly string _filePath;

        public JsonSaveSystem(string fileName)
        {
            _filePath = Application.persistentDataPath + $"/{fileName}.json";
        }
    
        public void Save(ISaveData data)
        {
            var json = JsonUtility.ToJson(data);
            using (var writer = new StreamWriter(_filePath))
            {
                writer.WriteLine(json);
            }
        }

        public ISaveData Load()
        {
            string json = "";
            using (var reader = new StreamReader(_filePath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    json += line;
                }
            }

            // не юзать json вообще, если все таки понадобится, то надо решить эту хуету
            // if (string.IsNullOrEmpty(json))
            // {
            //     return new SaveData();
            // }

            return JsonUtility.FromJson<ISaveData>(json);
        }
    }
}