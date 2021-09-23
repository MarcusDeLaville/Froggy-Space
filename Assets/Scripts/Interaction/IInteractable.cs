using UnityEngine.UI;

namespace Interaction
{
    public interface IInteractable
    {
        void OnWentInt(Button button);
    
        void OnReleased(Button button);
    }
}