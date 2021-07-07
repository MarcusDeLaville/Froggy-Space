using UnityEngine;
using UnityEngine.UI;

public class ParallaxLayer : MonoBehaviour
{
    protected float _imagePositionX;
    protected RawImage _image;
    
    private void Start()
    {
        _image = GetComponent<RawImage>();
    }
}
