using UnityEngine;

namespace Items.Chest
{
    [RequireComponent(typeof(ChestInteraction))]
    [RequireComponent(typeof(SpriteRenderer))]
    public class ChestOpener : MonoBehaviour
    {
        [SerializeField] private Sprite _openedChest;

        private ChestInteraction _chestInteraction;
        private SpriteRenderer _spriteRenderer;

        private void Awake()
        {
            _chestInteraction = GetComponent<ChestInteraction>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
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
            ChangeSprite();
        }

        public void ChangeSprite()
        {
            _spriteRenderer.sprite = _openedChest;
        }
    }
}