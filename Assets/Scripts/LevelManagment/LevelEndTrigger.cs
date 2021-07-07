using UnityEngine;

namespace LevelManagment
{
    [RequireComponent(typeof(Collider2D))]
    public class LevelEndTrigger : MonoBehaviour
    {
#if UNITY_EDITOR
        private void OnDrawGizmos() { }
#endif
    }
}