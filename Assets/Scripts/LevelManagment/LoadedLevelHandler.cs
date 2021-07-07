using UnityEngine;

namespace LevelManagment
{
    public class LoadedLevelHandler : MonoBehaviour
    {
        public void OnLevelLoaded()
        {
            Debug.Log("<b><color=green>LEVEL LOADED!</color></b>");
        }
    }
}