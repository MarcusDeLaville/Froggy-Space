
namespace SaveSystem
{
    public interface ISaveSystem
    {
        void Save(ISaveData data);
    
        ISaveData Load();
    }
}