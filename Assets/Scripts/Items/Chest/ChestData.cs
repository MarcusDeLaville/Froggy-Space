using SaveSystem;

namespace Items.Chest
{
    [System.Serializable]
    public struct ChestData : ISaveData
    {
        public bool IsOpened { get; }

        public ChestData(bool isOpened)
        {
            IsOpened = isOpened;
        }
    }
}