using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace SaveSystem
{
    public class BinarySaveSystem : ISaveSystem
    {
        private readonly string _filePath;

        public BinarySaveSystem(string fileName)
        {
            _filePath = Application.persistentDataPath + $"/{fileName}.dat";
        }

        public void Save(SaveData data)
        {
            using (FileStream file = File.Create(_filePath))
            {
                new BinaryFormatter().Serialize(file, data);
            }
        }

        public SaveData Load()
        {
            SaveData saveData;
            using (FileStream file = File.Open(_filePath, FileMode.Open))
            {
                object loadedData = new BinaryFormatter().Deserialize(file);
                saveData = (SaveData)loadedData;
            }

            return saveData;
        }
    }
}
