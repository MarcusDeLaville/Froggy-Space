using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR
namespace Items.Chest
{
    [CustomEditor(typeof(ChestSaveDeleter))]
    public class ChestSavesEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            ChestSaveDeleter chestDeleter = (ChestSaveDeleter)target;
        
            if (GUILayout.Button("Delete chest's states saves"))
            {
                chestDeleter.DeleteSaves();
                Debug.Log("Deleted!");
            }
        }
    }
}
#endif