namespace SaveSystem
{
    public class DataLoader
    {
        private BinarySaveSystem _saveSystem;

        public DataLoader(string fileName)
        {
            _saveSystem = new BinarySaveSystem(fileName);
        }

        public void Load<T>(ISaveData<T> loader) => _saveSystem.Load(loader);
    }
}