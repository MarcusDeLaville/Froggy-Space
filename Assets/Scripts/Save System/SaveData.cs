using Inventory;

namespace SaveSystem
{
    [System.Serializable]
    public class SaveData
    {
        public InventoryData InventoryData;

        public SaveData(InventoryData inventoryData)
        {
            InventoryData = inventoryData;
        }
    
        public SaveData()
        {
            InventoryData = default;
        }
    }
}