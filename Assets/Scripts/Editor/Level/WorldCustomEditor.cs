using LevelManagment;
using UnityEditorInternal;
using UnityEngine;

namespace UnityEditor
{
   [CustomEditor(typeof(World))]
    public class WorldCustomEditor : Editor
    {
        private SerializedProperty _levelsArray;
        private ReorderableList _levelList;
        
        private Level[] _levels => ((World) serializedObject.targetObject).Levels;
        
        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            if (GUILayout.Button("Сохранить"))
            {
                World thisWorld = serializedObject.targetObject as World;
                FillLevelFields(thisWorld);
                EditorUtility.SetDirty(serializedObject.targetObject);
            }
            
            _levelList.DoLayoutList();
            serializedObject.ApplyModifiedPropertiesWithoutUndo();
        }
        
        private void OnEnable()
        {
            _levelsArray = serializedObject.FindProperty("_levels");
            SetLevelIndexForEachLevel(_levels);
            SetReorderableList();
        }
        
        private void FillLevelFields(World world)
        {
            int length = world.Levels.Length;
            for (int i = 0; i < length; i++)
            {
                if (length <= 2)
                {
                    world.Levels[i].SetPreviousLevel(null);
                    world.Levels[i].SetNextLevel(null);
                }
                else
                {
                    world.Levels[i].SetPreviousLevel(i <= 1 
                        ? null : world.Levels[i - 1]);
                    world.Levels[i].SetNextLevel(i == 0 || i == length - 1 
                        ? null : world.Levels[i + 1]);
                }
            }
        }

        private void SetLevelIndexForEachLevel(Level[] levels)
        {
            for (int i = 0; i < levels.Length; i++)
                levels[i].LevelIndex = i;
        }
        
        private void SetReorderableList()
        {
            _levelList = new ReorderableList(serializedObject, _levelsArray, true, true, true, true);
            _levelList.elementHeight = 100f;
            _levelList.drawHeaderCallback = (rect) => EditorGUI.LabelField(rect, "Уровни");;
            _levelList.drawElementCallback = ReorderableListElementDraw;
            _levelList.onChangedCallback = ReorderableListOnChanged;
        }

        private void ReorderableListOnChanged(ReorderableList list)
        { 
            SetLevelIndexForEachLevel(_levels);
        }

        private void ReorderableListElementDraw(Rect rect, int index, bool isactive, bool isfocused)
        {
            SerializedProperty element = _levelList.serializedProperty.GetArrayElementAtIndex(index);
           
            DrawElementLabel(rect, index);
            DrawProperty(rect, element);
        }
        
        private void DrawElementLabel(Rect mainRect, int index)
        {
            Rect labelRect = mainRect;
            labelRect.height = EditorGUIUtility.singleLineHeight;
            
            if(index == 0)
                EditorGUI.LabelField(labelRect, "Главное меню");
            else
                EditorGUI.LabelField(labelRect, $"Уровень - {index}");
        }

        private void DrawProperty(Rect mainRect, SerializedProperty property)
        {
            Rect propertyRect = mainRect;
            propertyRect.y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
            EditorGUI.PropertyField(propertyRect, property, GUIContent.none);
        }
    }
}