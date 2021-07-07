using System;
using System.Collections.Generic;
using UnityEngine;

namespace Items
{
    [RequireComponent(typeof(Chest))]
    [RequireComponent(typeof(ResourceChestSpawner))]
    public class ResourceSpawner : MonoBehaviour
    {
        private Chest _chest;
        private ResourceChestSpawner _resourceChestSpawner;

        public event Action<List<ResourceViewer>> Spawned;

        private void Awake()
        {
            _chest = GetComponent<Chest>();
            _resourceChestSpawner = GetComponent<ResourceChestSpawner>();
        }

        private void OnEnable()
        {
            _chest.Opened += OnChestOpened;
        }

        private void OnDisable()
        {
            _chest.Opened -= OnChestOpened;
        }

        private void OnChestOpened()
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