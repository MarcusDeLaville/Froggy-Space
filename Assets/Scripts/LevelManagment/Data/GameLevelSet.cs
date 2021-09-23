using System;
using System.Runtime.Serialization;
using SaveSystem;
using UnityEngine;

namespace LevelManagment.Data
{
    [System.Serializable]
    [CreateAssetMenu(fileName = "Game Level Set", menuName = "Level/Create new Game Level Set", order = 1)]
    public class GameLevelSet : ScriptableObject, ISaveSurrogate<GameLevelSet>
    {
        [SerializeField] private World[] _worlds;

        public event Action DataSaved;
        
        public event Action DataLoaded;
        
        public World[] Worlds
        {
            get => _worlds;
            set => _worlds = value;
        }
        
        public SavedData<GameLevelSet> Save()
        {
            DataSaved?.Invoke();
            return new SavedData<GameLevelSet>(this);
        }
        
        public void Load(SavedData<GameLevelSet> data)
        {
            _worlds = data.GetData()._worlds;
            DataLoaded?.Invoke();
        }

        public SurrogateSelector GetSelector()
        {
            var surrogateSelector = new SurrogateSelector();
            surrogateSelector.AddSurrogate(
                typeof(GameLevelSet), 
                new StreamingContext(StreamingContextStates.All), 
                new GameLevelSetSurrogate());
            surrogateSelector.AddSurrogate(
                typeof(World), 
                new StreamingContext(StreamingContextStates.All), 
                new WorldSurrogate());

            return surrogateSelector;
        }
    }
    
    public class GameLevelSetSurrogate : ISerializationSurrogate
    {
        public void GetObjectData(object obj, SerializationInfo info, StreamingContext context)
        {
            var levelSet = (GameLevelSet)obj;
            info.AddValue("_worlds", levelSet.Worlds);
        }

        public object SetObjectData(object obj, SerializationInfo info, StreamingContext context, ISurrogateSelector selector)
        {
            var levelSet = (GameLevelSet)obj;
            levelSet.Worlds = info.GetValue("_worlds", typeof(World[])) as World[];
            return levelSet;
        }
    }
}