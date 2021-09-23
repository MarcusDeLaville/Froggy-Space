using LevelManagment;
using UnityEngine;

namespace UI
{
    public class ButtonActions : MonoBehaviour
    {
        public void LoadLevel(int levelIndex)
        {
            LevelLoader.LoadLevelByIndex(levelIndex);
        }

        public void LoadMainMenu()
        {
            LevelLoader.LoadMainMenu();
        }
        
        public void LoadNextLevel()
        {
            LevelLoader.LoadNextLevel();
        }

        public void LoadNextLevelWithParam()
        {
            LevelLoader.LoadNextLevel(new Vector3(10, 5, 0));
        }
        
        
        public void LoadPreviousLevel()
        {
            LevelLoader.LoadPreviousLevel();
        }
    }
}