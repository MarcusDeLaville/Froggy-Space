using LevelManagment;
using LevelManagment.Data;
using UnityEngine;

namespace UnityEditor
{
    [CustomPropertyDrawer(typeof(Level))]
    public class LevelCustomEditor : PropertyDrawer
    {
        private const float StandardVerticalSpacing = 2f;
        private const float SingleLineHeight = 18f;
        private const float ElementOffset = SingleLineHeight+ StandardVerticalSpacing;
        private const float TopOffset = 5f;

        private SerializedProperty _scenePath;
        private SerializedProperty _previousLevelIndex;
        private SerializedProperty _nextLevelIndex;
        private SerializedProperty _isLocked;
        private SerializedProperty _isPassed;

        private Rect _propertyRect;
        
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            GetPropertyReferences(property);

            Rect scenePickerRect = position;
            scenePickerRect.height = SingleLineHeight;
            DrawScenePickerField(scenePickerRect);

            Rect previousLevelRect = GetModifedRect(scenePickerRect);
            DrawPreviousLevelField(previousLevelRect);

            Rect nextLevelRect = GetModifedRect(previousLevelRect);
            DrawNextLevelField(nextLevelRect);

            Rect isLockedRect = GetModifedRect(nextLevelRect);
            isLockedRect.width /= 2;
            DrawIsLockedToggle(isLockedRect);

            Rect isPassedRect = isLockedRect;
            isPassedRect.x += isLockedRect.width;
            DrawIsPassedToggle(isPassedRect);
        }
        
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            //return SingleLineHeight * 2 +  EditorGUIUtility.standardVerticalSpacing;
            return 100f;
        }

        private Rect GetModifedRect(Rect original)
        {
            return new Rect(original) { y = original.y + ElementOffset };
        }

        private void GetPropertyReferences(SerializedProperty property)
        {
            _scenePath = property.FindPropertyRelative("_scenePath");
            _previousLevelIndex = property.FindPropertyRelative("_previousLevelIndex");
            _nextLevelIndex = property.FindPropertyRelative("_nextLevelIndex");
            _isLocked = property.FindPropertyRelative("_isLocked");
            _isPassed = property.FindPropertyRelative("_isPassed");
        }
        
        private void DrawScenePickerField(Rect fieldRect)
        {
            EditorGUI.BeginChangeCheck();
            SceneAsset scene = AssetDatabase.LoadAssetAtPath<SceneAsset>(_scenePath.stringValue);
            SceneAsset newScene = (SceneAsset)EditorGUI.ObjectField(fieldRect, "Сцена", scene, typeof(SceneAsset), false);

            if (EditorGUI.EndChangeCheck())
                _scenePath.stringValue = AssetDatabase.GetAssetPath(newScene);
        }

        private void DrawPreviousLevelField(Rect fieldRect)
        {
            int value = _previousLevelIndex.intValue;
            
            if(value < 0)
                EditorGUI.LabelField(fieldRect, "Предыдущий уровень", "Дальше нет уровней");
            else
                EditorGUI.TextField(fieldRect, "Предыдущий уровень", $"Уровень - {value}");
        }
        
        private void DrawNextLevelField(Rect fieldRect)
        {
            int value = _nextLevelIndex.intValue;
            
            if(value < 0)
                EditorGUI.LabelField(fieldRect, "Следующий уровень", "Дальше нет уровней");
            else
                EditorGUI.TextField(fieldRect, "Следующий уровень", $"Уровень - {value}");
        }

        private void DrawIsLockedToggle(Rect toggleRect)
        {
            _isLocked.boolValue = EditorGUI.ToggleLeft(toggleRect, "Заблокирован", _isLocked.boolValue);
        }
        
        private void DrawIsPassedToggle(Rect toggleRect)
        {
            _isPassed.boolValue = EditorGUI.ToggleLeft(toggleRect, "Пройден", _isPassed.boolValue);
        }
    }
}