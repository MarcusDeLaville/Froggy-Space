using System;
using System.Linq;
using LevelManagment.Data;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace LevelManagment
{
    public class LevelLoader
    {
        private const int NotFoundSceneIndex = -1;
        private const int DefaultWorldIndex = 0;
        
        private static GameLevelSet s_gameLevelSet;
        private static World s_activeWorld;
        private static Level s_activeLevel;
        private static object s_dataForTransfer;

        public static GameLevelSet GameLevelSet => s_gameLevelSet;
        
        [RuntimeInitializeOnLoadMethod]
        private static void Init()
        {
            var localAsset = Resources.Load<GameLevelSet>(@"Data/Levels/Game Level Set");
            if(localAsset == null)
                throw new NullReferenceException("Не найдена база уровней для игры!");

            s_gameLevelSet = ScriptableObject.CreateInstance<GameLevelSet>();
            s_gameLevelSet.Worlds = localAsset.Worlds;
            s_gameLevelSet.DataLoaded += OnLevelSetUpdated;
            
            SetWorldAndCurrentLevel();
            
            Debug.Log($"<b><color=orange>[{nameof(LevelLoader)}]</color>::<color=green>initialized</color></b>");
        }



        public static void SetWorldAndCurrentLevel()
        {
            s_activeWorld = s_gameLevelSet.Worlds[DefaultWorldIndex];
            
            if (s_activeLevel == null && s_activeWorld != null)
            {
                string thisScenePath = SceneManager.GetActiveScene().path;
                s_activeLevel = s_activeWorld.Levels.FirstOrDefault(x => x.ScenePath == thisScenePath) 
                                ?? s_activeWorld.DefaultLevel;
            }
        }
        
        public static void LoadMainMenu()
        {
            LoadLevel(s_activeWorld.DefaultLevel);
        }
        
        public static void LoadLevelByIndex(int levelIndex)
        {
            LoadLevel(GetLevelAtIndex(levelIndex));
        }

        public static void LoadLevelByIndex<T>(int levelIndex, T param)
        {
            LoadLevel(GetLevelAtIndex(levelIndex), param);
        }
        
        public static void LoadPreviousLevel()
        {
            LoadLevel(s_activeLevel?.PreviousLevel 
                      ?? GetLevelAtIndex(s_activeLevel?.PreviousLevelIndex));
        }
        
        public static void LoadPreviousLevel<T>(T param)
        {
            Level level = s_activeLevel?.PreviousLevel ?? GetLevelAtIndex(s_activeLevel?.PreviousLevelIndex);
            LoadLevel(level, param);
        }
        
        public static void LoadNextLevel()
        {
            LoadLevel(s_activeLevel?.NextLevel 
                      ?? GetLevelAtIndex(s_activeLevel?.NextLevelIndex));
        }
        
        public static void LoadNextLevel<T>(T param)
        {
            Level level = s_activeLevel?.NextLevel ?? GetLevelAtIndex(s_activeLevel?.NextLevelIndex);
            LoadLevel(level, param);
        }
        
        private static void LoadLevel(Level level)
        {
            LoadLevel<object>(level, null);
        }

        private static void LoadLevel<T>(Level level, T param)
        {
            bool scenePathIsNull = string.IsNullOrEmpty(level.ScenePath);
            bool sceneDoesNotExist = SceneUtility.GetBuildIndexByScenePath(level.ScenePath) == NotFoundSceneIndex;
            
            if(scenePathIsNull || sceneDoesNotExist)
                throw new ArgumentNullException($"Узазанной в {nameof(level)} сцены по пути сцены, параметра {nameof(level.ScenePath)} не существует.");
            
            s_activeLevel = level;
            Action<AsyncOperation> levelHandler = default;
                
            levelHandler = asyncOperation =>
            {
                SceneManager.SetActiveScene(SceneManager.GetSceneByPath(s_activeLevel.ScenePath));
                FindAndInvokeLoadedSceneHandler(param);
                asyncOperation.completed -= levelHandler;
            };
            AsyncOperation sceneLoader = SceneManager.LoadSceneAsync(s_activeLevel.ScenePath);
            sceneLoader.completed += levelHandler;
        }
        
        private static Level GetLevelAtIndex(int? levelIndex)
        {
            if (!levelIndex.HasValue || levelIndex == NotFoundSceneIndex)
                return s_activeWorld.DefaultLevel;
            
            if (levelIndex < NotFoundSceneIndex || levelIndex >= s_activeWorld.Levels.Length)
            {
                throw new ArgumentOutOfRangeException(
                    $"Индекс [{nameof(levelIndex)}] уровня находится за пределами доступных уровней");
            }

            return s_activeWorld.Levels[levelIndex.Value];
        }
        
        private static void FindAndInvokeLoadedSceneHandler<T>(T param)
        {
            if(param is null)
                return;

            foreach (GameObject gameObject in SceneManager.GetActiveScene().GetRootGameObjects())
            foreach (ILoadedLevelEvent<T> levelEventComponent in gameObject.GetComponentsInChildren<ILoadedLevelEvent<T>>(gameObject))
                levelEventComponent?.OnLevelLoaded(param);
        }
        
        private static void OnLevelSetUpdated()
        {
            SetWorldAndCurrentLevel();
        }
    }
}