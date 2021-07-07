using LevelManagment;
using UnityEngine;

namespace UI
{
    public class ButtonActions : MonoBehaviour
    {
        public void LoadLevel(int levelIndex)
        {
            LevelLoader.LoadLevel(levelIndex);
        }

        public void LoadMainMenu()
        {
            LevelLoader.LoadMainMenu();
        }
        
        public void LoadNextLevel()
        {
            LevelLoader.LoadNextLevel();
        }
        
        public void LoadPreviousLevel()
        {
            LevelLoader.LoadPreviousLevel();
        }
    }
}