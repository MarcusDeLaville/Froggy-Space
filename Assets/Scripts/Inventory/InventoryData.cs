using System.Collections.Generic;

namespace Inventory
{
    [System.Serializable]
    public struct InventoryData
    {
        public Dictionary<ResourceData, int> Inventory;

        public InventoryData(Dictionary<ResourceData, int> inventory)
        {
            Inventory = inventory;
        }

        public bool ContainsKey(ResourceData data)
        {
            if (Inventory != null)
                return Inventory.ContainsKey(data);

            Inventory = new Dictionary<ResourceData, int>();
            return false;
        }

        public void SetResourceDataValue(ResourceData data, int addedValue)
        {
            Inventory[data] += addedValue;
        }

        public void Add(ResourceData data, int value)
        {
            Inventory.Add(data, value);
        }

        public int GetResourceDataValue(ResourceData data)
        {
            return Inventory[data];
        }
    }
}