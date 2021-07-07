using UnityEditorInternal;
using UnityEngine;

namespace UnityEditor.Tilemaps
{
    internal static class AssetCreation
    {
        [MenuItem("Assets/Create/2D/Brushes/Prefabs Brush", priority = 50)]
        private static void CreatePrefabsBrush()
            => ProjectWindowUtil.CreateAsset(ScriptableObject.CreateInstance<PrefabsBrush>(), "New Prefabs Brush.asset");
    }
    
    [CustomEditor(typeof(PrefabsBrush)), CanEditMultipleObjects]
    public class PrefabsBrushEditor : Editor
    {
        private SerializedProperty _prefabs;
        private ReorderableList _prefabsList;
        
        private PrefabsBrush _prefabsBrush => target as PrefabsBrush;
        
        public override void OnInspectorGUI()
        {
            serializedObject.UpdateIfRequiredOrScript();
            _prefabsBrush.EraseAnyObjects = EditorGUILayout.Toggle("Удалять любые объекты", _prefabsBrush.EraseAnyObjects);
            _prefabsList.DoLayoutList();
            serializedObject.ApplyModifiedPropertiesWithoutUndo();
        }
        
        private void OnEnable()
        {
            _prefabs = serializedObject.FindProperty("_prefabs");
            SetReorderableList();
        }
        
        private void SetReorderableList()
        {
            _prefabsList = new ReorderableList(serializedObject, _prefabs, true, true, true, true);
            _prefabsList.elementHeight = 100f;
            _prefabsList.drawHeaderCallback = (rect) => EditorGUI.LabelField(rect, "Префабы");;
            _prefabsList.drawElementCallback = ReorderableListElementDraw;
            _prefabsList.onSelectCallback = (list) => _prefabsBrush.SetIndex(list.index);;
        }

        private void ReorderableListElementDraw(Rect rect, int index, bool isactive, bool isfocused)
        {
            SerializedProperty element = _prefabsList.serializedProperty.GetArrayElementAtIndex(index);
            EditorGUI.PropertyField(rect, element, GUIContent.none);
        }
    }
}