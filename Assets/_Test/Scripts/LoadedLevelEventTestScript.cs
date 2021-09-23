using LevelManagment;
using UnityEngine;

public class LoadedLevelEventTestScript : MonoBehaviour, ILoadedLevelEvent<Vector3>
{

    public void OnLevelLoaded(Vector3 param)
    {
        transform.position = param;
    }
}
