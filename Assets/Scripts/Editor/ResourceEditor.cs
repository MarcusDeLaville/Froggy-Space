using UnityEngine;

namespace UnityEditor
{
    [CustomEditor(typeof(Items.Resource))]
    public class ResourceEditor : Editor
    {
        private const float SpriteFieldHeight = 64f;
        private const float SpriteFieldWidth = 64f;

        private SerializedProperty _name;
        private SerializedProperty _priority;
        private SerializedProperty _icon;

        private void OnEnable()
        {
            _name = serializedObject.FindProperty("_name");
            _priority = serializedObject.FindProperty("_priority");
            _icon = serializedObject.FindProperty("_icon");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            DrawTargetObject();
            serializedObject.ApplyModifiedProperties();

            if (GUI.changed)
            {
                SceneManagement.EditorSceneManager.MarkSceneDirty(
                    UnityEngine.SceneManagement.SceneManager.GetActiveScene());
            }
        }

        private void DrawTargetObject()
        {
            EditorGUILayout.BeginHorizontal();
            DrawFields();
            DrawIcon();
            EditorGUILayout.EndHorizontal();
        }

        private void DrawFields()
        {
            EditorGUILayout.BeginVertical();
            EditorGUILayout.PropertyField(_name, new GUIContent("Наименование"));
            EditorGUILayout.PropertyField(_priority, new GUIContent("Приоритет"));
            EditorGUILayout.EndVertical();
        }

        private void DrawIcon()
        {
            _icon.objectReferenceValue = EditorGUILayout.ObjectField(
                _icon.objectReferenceValue,
                typeof(Sprite),
                allowSceneObjects: false,
                GUILayout.Height(SpriteFieldHeight),
                GUILayout.Width(SpriteFieldWidth));
        }
    }
}