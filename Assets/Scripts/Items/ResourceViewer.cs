using System.Collections;
using DG.Tweening;
using UnityEngine;

namespace Items
{
    [RequireComponent(typeof(SpriteRenderer))]
    [RequireComponent(typeof(BoxCollider2D))]
    public class ResourceViewer : MonoBehaviour
    {
        private SpriteRenderer _spriteRenderer;
        private BoxCollider2D _collider;
        private Resource _resource;
        private Tween _tween;

        public Resource Item => _resource;
        
        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _collider = GetComponent<BoxCollider2D>();
            _collider.enabled = false;
        }

        private void OnEnable()
        {
            _tween = transform.DOMoveY(transform.position.y + 0.1f, 0.75f).SetLoops(-1, LoopType.Yoyo)
                .SetEase(Ease.Linear);
            StartCoroutine(TurnOnCollider());
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

        private IEnumerator TurnOnCollider()
        {
            yield return new WaitForSeconds(1f);
            _collider.enabled = true;
        }
    }
}