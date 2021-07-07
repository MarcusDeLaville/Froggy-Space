using System;
using System.Collections;
using System.Collections.Generic;
using AppodealAds.Unity.Api;
using UnityEngine;
using UnityEngine.UI;

public class AdInvoker : MonoBehaviour
{
    // For test
    [SerializeField] private Button _adButton;
    
    private AdCallback _callback;
    
    private void Awake()
    {
        _callback = new AdCallback();
        AdSettings settings = new AdSettings(_callback);
    }

    private void OnEnable()
    {
        _callback.AdShown += OnAdShown;
        _adButton.onClick.AddListener(ShowAd);
    }

    private void OnDisable()
    {
        _callback.AdShown -= OnAdShown;
        _adButton.onClick.RemoveListener(ShowAd);
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