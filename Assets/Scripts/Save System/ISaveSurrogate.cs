using System;
using System.Runtime.Serialization;

namespace SaveSystem
{
    public interface ISaveData<T>
    {
        event Action DataSaved;
        event Action DataLoaded;
        SavedData<T> Save();
        void Load(SavedData<T> data);
    }

    public interface ISaveSurrogate<T> : ISaveData<T>
    {
        SurrogateSelector GetSelector();
    }
}