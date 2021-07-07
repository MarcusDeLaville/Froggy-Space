using System;
using AppodealAds.Unity.Common;

public class AdCallback : IInterstitialAdListener
{
    private bool _isShown;

    public event Action AdShown;
    
    public void onInterstitialLoaded(bool isPrecache)
    {
        
    }

    public void onInterstitialFailedToLoad()
    {
        
    }

    public void onInterstitialShowFailed()
    {
        _isShown = false;
    }

    public void onInterstitialShown()
    {
        _isShown = true;
    }

    public void onInterstitialClosed()
    {
        if(_isShown == true)
            AdShown?.Invoke();
        _isShown = false;
    }

    public void onInterstitialClicked()
    {
        
    }

    public void onInterstitialExpired()
    {
        
    }
}