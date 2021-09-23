using SaveSystem;

namespace Items.Chest
{
    public class ChestSaver
    {
        private const string FileName = "ChestState";
        private readonly ISaveSystem _saver;

        public ChestSaver(string id)
        {
            _saver = new BinarySaveSystem(FileName + id);
        }

        public void TrySave(ChestData data)
        {
            _saver.Save(data);
        }

        public ChestData TryLoad()
        {
            try
            {
                ISaveData data = _saver.Load();
                if (data is ChestData chestData)
                    return chestData;
                return new ChestData(false);
            }
            catch
            {
                return new ChestData(false);
            }
        }
    }
}