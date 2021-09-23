using System.Collections.Generic;
using UnityEngine;

namespace Items
{
    public class ResourceChestSpawner : MonoBehaviour
    {
        [SerializeField] private ResourceViewer _resourceTemplate;
        [SerializeField] private List<Resource> _resources;
        [SerializeField] private int _maxResources = 6;
        [SerializeField] private int _minResources = 3;

        private List<ResourceViewer> _resourceObjects;
        private ResourceChestSpawnerContext _context;
    
        // TODO: сделать пул, чтобы спрятать лист
        public List<ResourceViewer> ResourceObjects => _resourceObjects;

        private void Awake()
        {
            _resourceObjects = new List<ResourceViewer>();
            _context = new ResourceChestSpawnerContext(_minResources, _maxResources, _resources);
        }

        public void SpawnResources()
        {
            foreach (var resource in _resources)
            {
                if (_context.TryGetAmountOfResource(out int value, resource))
                {
                    for (int i = 0; i < value; i++)
                    {
                        var gameResource = Instantiate(_resourceTemplate, transform.position, Quaternion.identity, transform);
                        gameResource.Init(resource);
                        gameResource.gameObject.SetActive(false);
                        _resourceObjects.Add(gameResource);
                    }
                }
            }
        }
    }
}