using System;
using System.Collections.Generic;
using System.Linq;

namespace Items
{
    public struct ResourceChestSpawnerContext
    {
        private const int StartChanceValue = 10;
        private const int MaxPercentage = 100;

        private Dictionary<Resource, int> _storedResources;

        public ResourceChestSpawnerContext(int minResources, int maxResources, List<Resource> resources)
        {
            _storedResources = new Dictionary<Resource, int>();
            int resourceAmount = GetRandomAmountResources(minResources, maxResources);
            List<Resource> orderResources = OrderResources(resources);
            CreateResources(resourceAmount, orderResources);
            FillToMax(resourceAmount);
        }

        public bool TryGetAmountOfResource(out int value, Resource resource)
        {
            if (_storedResources.ContainsKey(resource))
            {
                value = _storedResources.First(res => res.Key == resource).Value;
                return true;
            }

            value = default;
            return false;
        }

        private void FillToMax(int max)
        {
            int allResources = 0;
            foreach (var res in _storedResources)
            {
                allResources += res.Value;
            }

            if (allResources < max)
            {
                int remainingResources = max - allResources;
                _storedResources[_storedResources.First(res => res.Key.Priority == 1).Key] += remainingResources;
            }
        }

        private int GetRandomAmountResources(int minResources, int maxResources)
        {
            return new Random().Next(minResources, maxResources + 1);
        }

        private List<Resource> OrderResources(List<Resource> resources)
        {
            List<Resource> orderedResources = resources.OrderBy(resource => resource.Priority).ToList();
            return orderedResources;
        }

        private void CreateResources(int resourceAmount, List<Resource> resources)
        {
            for (int i = 0; i < resources.Count; i++)
            {
                float spawnCoefficient = resourceAmount / resources[i].Priority;
                int minChanceValue = Convert.ToInt32(Math.Round(spawnCoefficient * StartChanceValue));
                int maxChanceValue = MaxPercentage - minChanceValue;
                int spawnAmount = Convert.ToInt32(Math.Round(resourceAmount * (maxChanceValue / 100f)));
                _storedResources.Add(resources[i], spawnAmount);
                resourceAmount -= spawnAmount;
            }
        }
    }
}