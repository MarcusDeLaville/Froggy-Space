using System.Collections.Generic;
using UnityEngine;

namespace Items
{
    public readonly struct DropPathCreator
    {
        private const float YPickPosition = 1.5f;
        private const float SideBorderOffset = 1.5f;
        private readonly Dictionary<ResourceViewer, Vector3[]> _patches;
        private readonly float _downXOffset;
        private readonly float _topXOffset;
        private readonly Vector2 _startPosition;

        public DropPathCreator(List<ResourceViewer> resources, Vector2 startPosition, Offset offsets)
        {
            _patches = new Dictionary<ResourceViewer, Vector3[]>();
            _downXOffset = offsets.DownXOffset;
            _topXOffset = offsets.TopXOffset;
            _startPosition = startPosition;
            Init(resources);
        }

        public bool TryGetResourcePath(out Vector3[] path, ResourceViewer resource)
        {
            path = default;
            if (_patches.ContainsKey(resource))
            {
                path = _patches[resource];
                return true;
            }
            return false;
        }

        private void Init(List<ResourceViewer> resources)
        {
            float xRange = -(SideBorderOffset * _downXOffset);
            float xStep = GetStep(resources) * 2;
            foreach (var resource in resources)
            {
                CreatePath(resource, xRange);
                xRange += xStep;
            }
        }

        private float GetStep(List<ResourceViewer> resources)
        {
            return SideBorderOffset / resources.Count * _downXOffset;
        }

        private void CreatePath(ResourceViewer resource, float xStep)
        {
            Vector2 endPosition = GetEndPosition(xStep);
            Vector2 pickPosition = GetPickPosition(endPosition.x);
            Vector3[] path = {pickPosition, endPosition};
            _patches.Add(resource, path);
        }

        private Vector2 GetEndPosition(float step)
        {
            float endXPosition = step + _startPosition.x;
            float endYPosition = _startPosition.y;
            return new Vector2(endXPosition, endYPosition);
        }

        private Vector2 GetPickPosition(float endXPosition)
        {
            float xPick = endXPosition - (endXPosition - _startPosition.x) / 2;
            xPick *= _topXOffset;
            return new Vector2(xPick, _startPosition.y + YPickPosition);
        }
    }
}