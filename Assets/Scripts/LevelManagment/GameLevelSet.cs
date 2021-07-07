using UnityEngine;

namespace LevelManagment
{
    [CreateAssetMenu(fileName = "Game Level Set", menuName = "Level/Create new Game Level Set", order = 1)]
    public class GameLevelSet : ScriptableObject
    {
        [SerializeField] private World[] _worlds;

        public World[] Worlds => _worlds;
    }
}