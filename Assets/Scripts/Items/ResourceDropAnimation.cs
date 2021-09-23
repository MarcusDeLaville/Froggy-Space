using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace Items
{
    [RequireComponent(typeof(ResourceSpawner))]
    public class ResourceDropAnimation : MonoBehaviour
    {
        [SerializeField] private float _dropDuration;
        [SerializeField] private Offset _offset;

        private ResourceSpawner _resourceSpawner;

        private void Awake()
        {
            _resourceSpawner = GetComponent<ResourceSpawner>();
        }

        private void OnEnable()
        {
            _resourceSpawner.Spawned += OnResourcesSpawned;
        }

        private void OnDisable()
        {
            _resourceSpawner.Spawned -= OnResourcesSpawned;
        }

        private void OnResourcesSpawned(List<ResourceViewer> resources)
        {
            DropPathCreator patches = new DropPathCreator(resources, transform.position, _offset);
            AnimateDropping(resources, patches);
        }

        private void AnimateDropping(List<ResourceViewer> resources, DropPathCreator patches)
        {
            foreach (var resource in resources)
            {
                if (patches.TryGetResourcePath(out Vector3[] path, resource))
                {
                    resource.transform.DOPath(path, _dropDuration, PathType.CatmullRom)
                        .SetEase(Ease.Linear);
                }
            }
        }
    }
}