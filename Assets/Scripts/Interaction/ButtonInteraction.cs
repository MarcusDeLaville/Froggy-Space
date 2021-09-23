using UnityEngine;
using UnityEngine.UI;

namespace Interaction
{
    public class ButtonInteraction : MonoBehaviour
    {
        [SerializeField] private GameObject _buttonObject;
    
        private Button _button;
    
        private void Awake()
        {
            _button = _buttonObject.GetComponent<Button>();
        }

        private void OnEnable()
        {
            _button.onClick.AddListener(OnButtonClick);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(OnButtonClick);
        }

        private void Start()
        {
            _buttonObject.SetActive(false);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out IInteractable interactable))
            {
                _buttonObject.SetActive(true);
                interactable.OnWentInt(_button);
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.TryGetComponent(out IInteractable interactable))
            {
                _buttonObject.SetActive(false);
                interactable.OnReleased(_button);
            }
        }

        private void OnButtonClick()
        {
            _buttonObject.SetActive(false);
        }
    }
}