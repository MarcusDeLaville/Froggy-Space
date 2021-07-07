using UnityEngine;

namespace Items
{
    [System.Serializable]
    public struct Offset
    {
        [SerializeField] private float _downXOffset;
        [SerializeField] private float _topXOffset;

        public float DownXOffset => _downXOffset;
        public float TopXOffset => _topXOffset;
    }
}