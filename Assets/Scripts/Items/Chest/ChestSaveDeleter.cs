using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Items.Chest
{
    public class ChestSaveDeleter : MonoBehaviour
    {
        private List<Chest> _chests;

        private void Awake()
        {
            _chests = GetComponentsInChildren<Chest>().ToList();
        }

        public void DeleteSaves()
        {
            foreach (var chest in _chests)
            {
                chest.ClearSaves();
            }
        }
    }
}