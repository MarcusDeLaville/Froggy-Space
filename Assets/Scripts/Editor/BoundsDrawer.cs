using Cinemachine;
using LevelManagment;
using UnityEngine;

namespace UnityEditor
{
    public class BoundsDrawer
    {
        [DrawGizmo(GizmoType.NotInSelectionHierarchy | GizmoType.NonSelected | GizmoType.Selected)]
        private static void DrawLevelEndTriggerBounds(LevelEndTrigger target, GizmoType gizmoType)
        {
            Transform targetTransform = target.transform;
            BoxCollider2D collider = target.GetComponent<BoxCollider2D>();
            
            Vector2 scale = targetTransform.localScale;
            Vector2 position = (Vector2)targetTransform.position + (collider.offset* scale);
            Vector2 size = collider.size * scale;
            
            Handles.color = Color.green;
            Handles.DrawWireCube(position, size);
        }
        
        [DrawGizmo(GizmoType.NotInSelectionHierarchy | GizmoType.NonSelected | GizmoType.Selected)]
        private static void DrawCameraBounds(CinemachineConfiner target, GizmoType gizmoType)
        {
            PolygonCollider2D collider = target.m_BoundingShape2D as PolygonCollider2D;

            if(collider == null)
                return;

            Vector2 position = collider.transform.position;
            Vector2[] lines = collider.GetPath(0);
            
            Handles.color = Color.blue;
            for (int i = 0; i < lines.Length; i++)
            {
                if(i == lines.Length - 1)
                    Handles.DrawLine(lines[i] + position, lines[0] + position);
                else
                    Handles.DrawLine(lines[i] + position, lines[i + 1] + position);
            }
        }
    }
}