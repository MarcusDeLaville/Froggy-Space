namespace LevelManagment
{
    public interface ILoadedLevelEvent<T>
    {
        void OnLevelLoaded(T param);
    }
}