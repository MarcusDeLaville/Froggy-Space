using UnityEngine;

namespace UnityEditor.Tilemaps
{
    [CustomPropertyDrawer(typeof(PrefabsBrush.PrefabBrushElement))]
    public class PrefabBrushElementPropertyDrawer : PropertyDrawer
    {
        private const float HorizontalSpacing = 10f;
        private const float ElementHeight = 100f;
        private const float TopOffset = 5f;
        private const float BottomOffset = 5f;
        private const float ImageWidth = ElementHeight;
        private const float ImageHeight = ElementHeight - TopOffset - BottomOffset;
        
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            position.y += TopOffset;
            position.height -= TopOffset + BottomOffset;
            
            float textureFrameSize = 100f;
            float verticalLineOffset = EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
            
            SerializedProperty prefab = property.FindPropertyRelative("_prefab");
            SerializedProperty anchor = property.FindPropertyRelative("_anchor");
            
            Rect prefabLabelRect = new Rect(
                position.x, 
                position.y, 
                position.width - textureFrameSize - HorizontalSpacing, 
                EditorGUIUtility.singleLineHeight);
            
            Rect prefabRect = prefabLabelRect;
            prefabRect.y += verticalLineOffset;
            
            Rect anchorLabelRect = prefabRect;
            anchorLabelRect.y += verticalLineOffset;
            
            Rect anchorRect = anchorLabelRect;
            anchorRect.y += verticalLineOffset;
            
            Rect previewTextureRect = new Rect(
                position.x + prefabLabelRect.width + HorizontalSpacing, 
                position.y + EditorGUIUtility.standardVerticalSpacing, 
                ImageWidth, 
                ImageHeight - EditorGUIUtility.standardVerticalSpacing);
            
            // 
            EditorGUI.LabelField(prefabLabelRect, "Префаб");
            EditorGUI.PropertyField(prefabRect, prefab, GUIContent.none);
            
            // 
            EditorGUI.LabelField(anchorLabelRect, "Смещение");
            EditorGUI.PropertyField(anchorRect, anchor, GUIContent.none);
            
            // Превью спрайта
            Texture preview = GetPreviewTexture(prefab);
            EditorGUI.DrawTextureTransparent(previewTextureRect, preview, ScaleMode.ScaleToFit, 1f);
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label) => ElementHeight;

        private Texture GetPreviewTexture(SerializedProperty property)
        {
            var prefabObject = ((object)property.objectReferenceValue) as GameObject;
            Texture preview = EditorGUIUtility.whiteTexture;
            
            if (prefabObject != null && prefabObject.TryGetComponent(out SpriteRenderer renderer))
                preview = AssetPreview.GetAssetPreview(renderer.sprite);

            return preview;
        }
    }
}