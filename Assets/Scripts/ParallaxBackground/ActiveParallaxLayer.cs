using UnityEngine;
using UnityEngine.UI;

public class ActiveParallaxLayer : ParallaxLayer
{
    [SerializeField, Range(1f, 100f)] private float _smooth = 1f;

    [SerializeField] private PhysicsMovement _followObject;
    
    private void Update()
    {
        _imagePositionX += (_followObject.Velocity.x / _smooth) * Time.deltaTime;
        _image.uvRect = new Rect(_imagePositionX, 0, _image.uvRect.width, _image.uvRect.height);
    }
}
