using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR
namespace Inventory
{
    [CustomEditor(typeof(PlayerInventory))]
    public class PlayerInventoryInspector : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            PlayerInventory inventory = (PlayerInventory)target;
            if (GUILayout.Button("Save Inventory"))
            {
                inventory.SaveInventory();
                Debug.Log("Saved!");
            }

            if (GUILayout.Button("Load Inventory"))
            {
                inventory.LoadInventory();
                Debug.Log("Loaded!");
            }

            if (GUILayout.Button("Clear Inventory"))
            {
                inventory.ClearInventory();
                Debug.Log("Cleared!");
            }
        }
    }
}
#endif