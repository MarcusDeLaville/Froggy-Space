using UnityEngine;

namespace Items
{
    [CreateAssetMenu(fileName = "Resource", menuName = "Resource/Create new resource", order = 51)]
    public class Resource : ScriptableObject
    {
        [SerializeField] private Sprite _icon;
        [SerializeField] private string _name;
        [SerializeField] private int _priority;

        public int Priority => _priority;
        public Sprite Icon => _icon;
        public string Name => _name;
    }
}