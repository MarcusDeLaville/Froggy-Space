using AppodealAds.Unity.Api;
using UnityEngine;

public class Advertisement : MonoBehaviour
{
    private AdCallback _callback;

    public AdCallback Callback => _callback;
    
    private void Awake()
    {
        _callback = new AdCallback();
        AdSettings settings = new AdSettings(_callback);
    }

    private void OnEnable()
    {
        _callback.AdShown += OnAdShown;
    }

    private void OnDisable()
    {
        _callback.AdShown -= OnAdShown;
    }

    public void ShowAd()
    {
        if (Appodeal.canShow(Appodeal.INTERSTITIAL) == true && Appodeal.isPrecache(Appodeal.INTERSTITIAL) == false)
            Appodeal.show(Appodeal.INTERSTITIAL);
    }

    private void OnAdShown()
    {
        Debug.Log("Succesed!");
    }
}