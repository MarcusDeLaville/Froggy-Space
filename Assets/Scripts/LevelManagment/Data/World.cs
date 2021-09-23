using System.Runtime.Serialization;
using UnityEngine;

namespace LevelManagment.Data
{
    [System.Serializable]
    [CreateAssetMenu(fileName = "New world level set", menuName = "Level/Create new world", order = 1)]
    public class World : ScriptableObject
    {
        [SerializeField] private Level[] _levels;

        public Level[] Levels
        {
            get => _levels;
            set => _levels = value;
        }

        public Level DefaultLevel => _levels?[0];
        
    }

    public class WorldSurrogate : ISerializationSurrogate
    {
        public void GetObjectData(object obj, SerializationInfo info, StreamingContext context)
        {
            var world = (World)obj;
            info.AddValue("_levels", world.Levels);
        }

        public object SetObjectData(object obj, SerializationInfo info, StreamingContext context, ISurrogateSelector selector)
        {
            var world = (World)obj;
            world.Levels = info.GetValue("_levels", typeof(Level[])) as Level[];
            return world;
        }
    }
}