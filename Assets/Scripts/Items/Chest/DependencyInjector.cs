using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Items.Chest
{
    public class DependencyInjector : MonoBehaviour
    {
        [SerializeField] private Advertisement _advertisement;

        private List<Chest> _chests;

        private void Awake()
        {
            _chests = GetComponentsInChildren<Chest>().ToList();

            foreach (var chest in _chests)
            {
                chest.Inject(_advertisement);
            }
        }
    } 
}