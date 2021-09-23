using Inventory;

namespace SaveSystem
{
    [System.Serializable]
    public class InventorySaveData : ISaveData
    {
        public InventoryData InventoryData;

        public InventorySaveData(InventoryData inventoryData)
        {
            InventoryData = inventoryData;
        }
    
        public InventorySaveData()
        {
            InventoryData = default;
        }
    }
}