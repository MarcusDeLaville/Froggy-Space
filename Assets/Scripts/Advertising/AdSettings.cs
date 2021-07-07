using AppodealAds.Unity.Api;

public class AdSettings
{
    private const string AppKey = "a2f87efd2573875c199f9062a3151a85620372094af3835d";

    public AdSettings(AdCallback callback)
    {
        int adTypes = Appodeal.INTERSTITIAL;
        Appodeal.initialize(AppKey, adTypes, true);
        Appodeal.setInterstitialCallbacks(callback);
    }
}