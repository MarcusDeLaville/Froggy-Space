using System;
using DG.Tweening;
using UnityEngine;

namespace Items
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class ResourceViewer : MonoBehaviour
    {
        private Resource _resource;
        private SpriteRenderer _spriteRenderer;
        private Tween _tween;

        public Resource Item => _resource;
        
        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void OnEnable()
        {
            _tween = transform.DOMoveY(transform.position.y + 0.1f, 0.75f).SetLoops(-1, LoopType.Yoyo)
                .SetEase(Ease.Linear);
        }

        private void OnDisable()
        {
            _tween.Kill();
        }

        public void Init(Resource resource)
        {
            _resource = resource;
            _spriteRenderer.sprite = _resource.Icon;
        }
    }
}