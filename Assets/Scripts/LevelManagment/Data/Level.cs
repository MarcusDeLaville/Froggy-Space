using UnityEngine;

namespace LevelManagment.Data
{
    [System.Serializable]
    public class Level
    {
        [SerializeField] private string _scenePath;
        [SerializeField] private int _levelIndex;
        [SerializeField] private int _previousLevelIndex;
        [SerializeField] private int _nextLevelIndex;
        [SerializeField] private bool _isLocked = true;
        [SerializeField] private bool _isPassed;
        [SerializeField] private Level _previousLevel;
        [SerializeField] private Level _nextLevel;
        
        public string ScenePath => _scenePath;

        public int LevelIndex
        {
            get => _levelIndex;
            set => _levelIndex = value;
        }

        public Level NextLevel => _nextLevel;
        
        public int NextLevelIndex => _nextLevelIndex;

        public Level PreviousLevel => _previousLevel;
        
        public int PreviousLevelIndex => _previousLevelIndex;

        public void SetPreviousLevel(Level previous)
        {
            _previousLevel = previous;
            _previousLevelIndex = previous?.LevelIndex ?? -1;
        }

        public void SetNextLevel(Level next)
        {
            _nextLevel = next;
            _nextLevelIndex = next?.LevelIndex ?? -1;
        }
    }
}