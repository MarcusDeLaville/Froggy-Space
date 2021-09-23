using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using Inventory;

namespace Items.UI
{
    public class ResourcePresenter : MonoBehaviour
    {
        [SerializeField] private PlayerInventory _playerInventory;
        [SerializeField] private TextMeshProUGUI _amountOfCoin;
        [SerializeField] private TextMeshProUGUI _amountOfDiamond;
    
        private Coroutine _saver;
        private WaitForSeconds _waitingTime = new WaitForSeconds(3f);
    
        private void OnEnable()
        {
            _playerInventory.ItemAdded += OnItemPicked;
            _playerInventory.InventoryLoaded += OnInventoryLoad;
        }
    
        private void OnDisable()
        {
            _playerInventory.ItemAdded -= OnItemPicked;
            _playerInventory.InventoryLoaded -= OnInventoryLoad;
        }
    
        private void OnItemPicked(ResourceData resource, int value)
        {
            switch (resource.ResourcePriority)
            {
                case 1:
                    _amountOfCoin.text = value.ToString();
                    break;
                case 2:
                    _amountOfDiamond.text = value.ToString();
                    break;
            }
    
            if (_saver != null)
            {
                StopCoroutine(_saver);
            }
    
            _saver = StartCoroutine(WaitToSave());
        }
    
        private void OnInventoryLoad(Dictionary<ResourceData, int> items)
        {
            if (items != null)
            {
                var keys = items.Keys.ToList();
                _amountOfCoin.text = items[keys[0]].ToString();
                _amountOfDiamond.text = items[keys[1]].ToString();
            }
            else
            {
                _amountOfCoin.text = "0";
                _amountOfDiamond.text = "0";
            }
        }
    
        private IEnumerator WaitToSave()
        {
            yield return _waitingTime;
            _playerInventory.SaveInventory();
        }
    }
}