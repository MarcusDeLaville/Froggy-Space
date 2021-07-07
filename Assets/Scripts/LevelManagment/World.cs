using UnityEngine;

namespace LevelManagment
{
    [CreateAssetMenu(fileName = "New world level set", menuName = "Level/Create new world", order = 1)]
    public class World : ScriptableObject
    {
        [SerializeField] private Level[] _levels;

        public Level[] Levels => _levels;
        
        public Level DefaultLevel => _levels?[0];
    }
}