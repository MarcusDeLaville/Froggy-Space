namespace SaveSystem
{
    public class DataSaver
    {
        private BinarySaveSystem _saveSystem;

        public DataSaver(string fileName)
        {
            _saveSystem = new BinarySaveSystem(fileName);
        }

        public void Save<T>(ISaveData<T> data) => _saveSystem.Save(data);
        
        public void Save<T>(ISaveSurrogate<T> data) => _saveSystem.Save(data);
    }
}