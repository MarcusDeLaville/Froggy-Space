namespace SaveSystem
{
    public class SavedData<T>
    {
        private T _data;
        
        public SavedData(T data)
        {
            _data = data;
        }

        public T GetData() => _data;
    }
}