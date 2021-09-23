using Interaction;
using UnityEngine;
using UnityEngine.UI;

namespace Items.Chest
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class ChestCollisionHandler : MonoBehaviour, IInteractable
    {
        private BoxCollider2D _collider;
        private ChestInteraction _chestInteraction;
        
        private void Awake()
        {
            _collider = GetComponent<BoxCollider2D>();
            _collider.enabled = false;
        }

        private void OnEnable()
        {
            _collider.enabled = true;
        }

        private void OnDisable()
        {
            _collider.enabled = false;
        }

        public void Init(ChestInteraction interaction)
        {
            _chestInteraction = interaction;
        }

        public void OnWentInt(Button button)
        {
            button.onClick.AddListener(_chestInteraction.Interact);
            button.onClick.AddListener(OnButtonClick);
        }

        public void OnReleased(Button button)
        {
            button.onClick.RemoveListener(_chestInteraction.Interact);
            button.onClick.RemoveListener(OnButtonClick);
        }

        private void OnButtonClick()
        {
            enabled = false;
        }
    }
}