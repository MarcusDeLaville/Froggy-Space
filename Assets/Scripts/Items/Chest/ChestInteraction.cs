using System;
using UnityEngine;

namespace Items.Chest
{
    public abstract class ChestInteraction : MonoBehaviour
    {
        public abstract event Action Opening;
    
        public abstract void Interact();
    }
}