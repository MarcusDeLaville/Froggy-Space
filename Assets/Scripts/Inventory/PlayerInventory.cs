using System;
using System.Collections.Generic;
using UnityEngine;
using Items;
using SaveSystem;

namespace Inventory
{
    [RequireComponent(typeof(PlayerCollisionHandler))]
    public class PlayerInventory : MonoBehaviour
    {
        [SerializeField] private SaveSystemType _saveSystemType;
    
        private InventoryData _items;
        private InventorySaver _saver;
        private PlayerCollisionHandler _collisionHandler;
    
        public event Action<ResourceData, int> ItemAdded;
        public event Action<Dictionary<ResourceData, int>> InventoryLoaded;
    
        private void Awake()
        {
            _collisionHandler = GetComponent<PlayerCollisionHandler>();
        }
    
        private void OnEnable()
        {
            _collisionHandler.Picked += OnResourcePicked;
        }
    
        private void Start()
        {
            _saver = new InventorySaver(_saveSystemType);
            LoadInventory();
        }
    
        private void OnDisable()
        {
            _collisionHandler.Picked -= OnResourcePicked;
        }
    
        public void SaveInventory()
        {
            var data = new SaveData(_items);
            _saver.TrySaveInventory(data);
        }
        
        private void OnResourcePicked(Resource resource)
        {
            ResourceData data = new ResourceData
            {
                ResourcePriority = resource.Priority
            };
            AddItemToInventory(data);
        }
    
        private void AddItemToInventory(ResourceData data)
        {
            int value;
            if (_items.ContainsKey(data))
            {
                _items.SetResourceDataValue(data, 1);
                value = _items.GetResourceDataValue(data);
            }
            else
            {
                _items.Add(data, 1);
                value = 1;
            }
    
            ItemAdded?.Invoke(data, value);
        }
    
        // public на время тестов
        public void LoadInventory()
        {
            var data = _saver.TryLoadInventory();
            InventoryLoaded?.Invoke(data.InventoryData.Inventory);
            _items = data.InventoryData;
        }

        // Метод на время тестов
        public void ClearInventory()
        {
            _items = default;
            SaveInventory();
            LoadInventory();
        }
    }
}