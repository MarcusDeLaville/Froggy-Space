using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using LevelManagment.Data;
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
        
        public void Save(ISaveData data)
        {
            using (FileStream file = File.Create(_filePath))
            {
                new BinaryFormatter().Serialize(file, data);
            }
        }

        public ISaveData Load()
        {
            ISaveData saveData;
            using (FileStream file = File.Open(_filePath, FileMode.Open))
            {
                object loadedData = new BinaryFormatter().Deserialize(file);
                saveData = (ISaveData)loadedData;
            }

            return saveData;
        }
        
        // -------------- [ МОЯ БЛЕВОТА ] -------------- //
        
        public void Save<T>(ISaveData<T> saver)
        {
            SavedData<T> persistData = saver.Save();
            var formatter = new BinaryFormatter();
            
            if (saver is ISaveSurrogate<T> surrogate)
                formatter.SurrogateSelector = surrogate.GetSelector();
            
            using (FileStream file = File.Create(_filePath))
                formatter.Serialize(file, persistData.GetData());
        }
        
        public void Load<T>(ISaveData<T> loader)
        {
            SavedData<T> saveData;
            var formatter = new BinaryFormatter();
            
            if (loader is ISaveSurrogate<T> surrogate)
                formatter.SurrogateSelector = surrogate.GetSelector();
            
            using (FileStream file = File.Open(_filePath, FileMode.Open))
            {
                object loadedData = formatter.Deserialize(file);
                saveData = new SavedData<T>((T)loadedData);
            }
            loader.Load(saveData);
        }
    }
}
