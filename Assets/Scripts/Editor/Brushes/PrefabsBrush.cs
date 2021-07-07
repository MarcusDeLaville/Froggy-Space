using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace UnityEditor.Tilemaps
{
    [CustomGridBrush(false, true, false, "Prefabs Brush")]
    public class PrefabsBrush : BasePrefabBrush
    {
        [SerializeField] private bool _eraseAnyObjects;
        [SerializeField] private PrefabBrushElement[] _prefabs;
        private int _selectedIndex;

        public bool EraseAnyObjects
        {
            get => _eraseAnyObjects;
            set => _eraseAnyObjects = value;
        }

        public override void Paint(GridLayout grid, GameObject brushTarget, Vector3Int position)
        {
            if (IsPaletteEditing(brushTarget))
                return;
            
            List<GameObject> objectsInCell = GetObjectsInCell(grid, brushTarget.transform, position);

            bool existPrefabObjectInCell = objectsInCell.Any(objectInCell => _prefabs.Any(
                prefab => PrefabUtility.GetCorrespondingObjectFromSource(objectInCell) == prefab.Prefab));
            
            if (!existPrefabObjectInCell)
            {
                GameObject prefab = _prefabs[_selectedIndex].Prefab;
                m_Anchor = _prefabs[_selectedIndex].Anchor;
                InstantiatePrefabInCell(grid, brushTarget, position, prefab);
            }
        }

        public override void Erase(GridLayout grid, GameObject brushTarget, Vector3Int position)
        {
            if (IsPaletteEditing(brushTarget))
                return;

            GameObject selected = _prefabs[_selectedIndex].Prefab;
            foreach (GameObject objectInCell in GetObjectsInCell(grid, brushTarget.transform, position))
            {
                if (objectInCell != null)
                {
                    if (PrefabUtility.GetCorrespondingObjectFromSource(objectInCell) == selected || _eraseAnyObjects)
                        Undo.DestroyObjectImmediate(objectInCell);
                }
            }
        }

        public void SetIndex(int index)
            => _selectedIndex = Mathf.Clamp(index, index, _prefabs.Length - 1);

        private bool IsPaletteEditing(GameObject brushTarget)
            => brushTarget.layer == 31 || brushTarget == null;
        
        [Serializable]
        public class PrefabBrushElement
        {
            [SerializeField] private GameObject _prefab;
            [SerializeField] private Vector3 _anchor;

            public GameObject Prefab => _prefab;
        
            public Vector3 Anchor => _anchor;

            public static explicit operator GameObject(PrefabBrushElement element) => element._prefab;
        }
    }
}