using Inventory;

namespace SaveSystem
{
    public class InventorySaver
    {
        private const string FileName = "InventorySave";

        private readonly ISaveSystem _saveSystem;

        public InventorySaver(SaveSystemType saveSystemType)
        {
            switch (saveSystemType)
            {
                case SaveSystemType.Binary:
                    _saveSystem = new BinarySaveSystem(FileName);
                    break;
                case SaveSystemType.Json:
                    _saveSystem = new JsonSaveSystem(FileName);
                    break;
            }
        }

        public void TrySaveInventory(InventorySaveData data)
        {
            try
            {
                _saveSystem.Save(data);
            }
            catch
            {
                _saveSystem.Save(new InventorySaveData());
            }
        }

        public InventorySaveData TryLoadInventory()
        {
            try
            {
                var data = _saveSystem.Load() as InventorySaveData;
                return data;
            }
            catch
            {
                return new InventorySaveData();
            }
        }
    }
}