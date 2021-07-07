using UnityEngine;
using UnityEngine.UI;

public class AutomaticParallaxLayer : ParallaxLayer
{
    [SerializeField, Min(0)] private float _scrollSpeed = 0.1f;

    private void Update()
    {
        _imagePositionX += _scrollSpeed * Time.deltaTime;

        _image.uvRect = new Rect(_imagePositionX, 0, _image.uvRect.width, _image.uvRect.height);

    }
}
