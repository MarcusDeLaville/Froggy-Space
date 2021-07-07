using UnityEngine;

namespace Items
{
    [RequireComponent(typeof(Chest))]
    [RequireComponent(typeof(SpriteRenderer))]
    public class ChestOpener : MonoBehaviour
    {
        [SerializeField] private Sprite _openedChest;

        private Chest _chest;
        private SpriteRenderer _spriteRenderer;

        private void Awake()
        {
            _chest = GetComponent<Chest>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
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
            ChangeSprite();
        }

        private void ChangeSprite()
        {
            _spriteRenderer.sprite = _openedChest;
        }
    }
}