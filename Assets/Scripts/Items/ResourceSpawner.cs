using System;
using System.Collections.Generic;
using Items.Chest;
using UnityEngine;

namespace Items
{
    [RequireComponent(typeof(ChestInteraction))]
    [RequireComponent(typeof(ResourceChestSpawner))]
    public class ResourceSpawner : MonoBehaviour
    {
        private ChestInteraction _chestInteraction;
        private ResourceChestSpawner _resourceChestSpawner;

        public event Action<List<ResourceViewer>> Spawned;

        private void Awake()
        {
            _chestInteraction = GetComponent<ChestInteraction>();
            _resourceChestSpawner = GetComponent<ResourceChestSpawner>();
        }

        private void OnEnable()
        {
            _chestInteraction.Opening += OnChestOpening;
        }
        
        private void OnDisable()
        {
            _chestInteraction.Opening -= OnChestOpening;
        }

        private void OnChestOpening()
        {
            foreach (var resource in _resourceChestSpawner.ResourceObjects)
            {
                resource.gameObject.SetActive(true);
            }

            Spawned?.Invoke(_resourceChestSpawner.ResourceObjects);
            enabled = false;
        }
    }
}