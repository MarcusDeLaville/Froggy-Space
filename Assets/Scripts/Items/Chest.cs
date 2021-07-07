using System;
using Inventory;
using UnityEngine;

namespace Items
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class Chest : MonoBehaviour
    {
        public event Action Opened;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out PlayerInventory player))
            {
                Opened?.Invoke();
            }
        }
    }
}