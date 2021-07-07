using System;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace LevelManagment
{
    public static class LevelLoader
    {
        private const int DefaultWorldIndex = 0;
        
        private static GameLevelSet s_gameLevelSet;
        private static World s_activeWorld;
        private static Level s_activeLevel;
        
        [RuntimeInitializeOnLoadMethod]
        private static void Init()
        {
            s_gameLevelSet = Resources.Load<GameLevelSet>(@"Data/Levels/Game Level Set");
            if(s_gameLevelSet == null)
                throw new NullReferenceException("Не найдена база уровней для игры!");
            
            SceneManager.sceneLoaded += OnLevelLoaded;
            s_activeWorld = s_gameLevelSet.Worlds[DefaultWorldIndex];
            
            if (s_activeLevel == null)
            {
                string thisScenePath = SceneManager.GetActiveScene().path;
                s_activeLevel = s_activeWorld.Levels.FirstOrDefault(x => x.ScenePath == thisScenePath) 
                                ?? s_activeWorld.DefaultLevel;
            }
            
            Debug.Log($"<b><color=orange>[{nameof(LevelLoader)}]</color>::<color=green>initialized</color></b>");
        }
        
        public static void LoadMainMenu()
        {
            LoadLevel(s_activeWorld.DefaultLevel);
        }
        
        public static void LoadLevel(int levelIndex)
        {
            LoadLevel(GetLevelAtIndex(levelIndex));
        }

        public static void LoadNextLevel()
        {
            LoadLevel(s_activeLevel?.NextLevel 
                      ?? GetLevelAtIndex(s_activeLevel?.NextLevelIndex));
        }
        
        public static void LoadPreviousLevel()
        {
            LoadLevel(s_activeLevel?.PreviousLevel 
                      ?? GetLevelAtIndex(s_activeLevel?.PreviousLevelIndex));
        }
        
        private static void LoadLevel(Level level)
        {
            s_activeLevel = level;
            SceneManager.LoadScene(s_activeLevel.ScenePath);
        }

        private static Level GetLevelAtIndex(int? levelIndex)
        {
            if (!levelIndex.HasValue || levelIndex == -1)
                return s_activeWorld.DefaultLevel;
            
            if (levelIndex < -1 || levelIndex >= s_activeWorld.Levels.Length)
            {
                throw new ArgumentOutOfRangeException(
                    $"Индекс [{nameof(levelIndex)}] уровня находится за пределами доступных уровней");
            }

            return s_activeWorld.Levels[levelIndex.Value];
        }
        
        private static void OnLevelLoaded(Scene loadedScene, LoadSceneMode mode)
        {
            FindAndInvokeLoadedSceneHandler();
        }
        
        private static void FindAndInvokeLoadedSceneHandler()
        {
            GameObject handlerObject = SceneManager
                .GetActiveScene()
                .GetRootGameObjects()
                .FirstOrDefault(x => x.TryGetComponent<LoadedLevelHandler>(out _));
            
            if(handlerObject != null)
                handlerObject.GetComponent<LoadedLevelHandler>().OnLevelLoaded();
        }
    }
}