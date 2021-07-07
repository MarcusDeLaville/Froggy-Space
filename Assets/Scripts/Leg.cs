using System;
using UnityEngine;

public class Leg : MonoBehaviour
{
    [SerializeField] private Transform _hip;
    [SerializeField] private Transform _foot;

    [SerializeField] private ContactFilter2D _filter;
    
    private RaycastHit2D[] _hits = new RaycastHit2D[12];

    private void LateUpdate()
    {
        var hitsCount = Physics2D.Raycast(_hip.position, Vector2.down, _filter, _hits);

        if (hitsCount > 0)
        {
            var first = _hits[0];

            
            _foot.position = first.point;
            _foot.rotation = Quaternion.FromToRotation(Vector3.up, first.normal);
        }
    }
}
