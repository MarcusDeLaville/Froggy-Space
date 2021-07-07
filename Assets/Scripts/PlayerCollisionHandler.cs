using System;
using Items;
using LevelManagment;
using UnityEngine;

public class PlayerCollisionHandler : MonoBehaviour
{
    public event Action<Resource> Picked;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out ResourceViewer resource))
        {
            Picked?.Invoke(resource.Item);
            other.gameObject.SetActive(false);
        }

        if (other.TryGetComponent(out LevelEndTrigger levelEndTrigger))
        {
            LevelLoader.LoadNextLevel();
        }
    }
}