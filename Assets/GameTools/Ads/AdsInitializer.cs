using UnityEngine;
using UnityEngine.Advertisements;

public class AdsInitializer : MonoBehaviour, IUnityAdsInitializationListener
{
    [SerializeField] string _androidGameId;
    [SerializeField] string _iOSGameId;
    [SerializeField] UnityAdsBanner _adsBunner;
    [SerializeField] UnityAdsInterstitial _adsInterstitial;
    [SerializeField] UnityAdsReward _adsReward;
    [SerializeField] bool _testMode = true;
    private string _gameId;

    void Awake()
    {
        InitializeAds();
    }

    public void InitializeAds()
    {
#if UNITY_IOS
        _gameId = _iOSGameId;
#elif UNITY_ANDROID
        _gameId = _androidGameId;
#elif UNITY_EDITOR
        _gameId = _androidGameId; // エディタでの機能テスト用
#endif
        if (!Advertisement.isInitialized && Advertisement.isSupported)
        {
            Advertisement.Initialize(_gameId, _testMode, this);
        }
    }

    public void OnInitializationComplete()
    {
        _adsBunner?.LoadBanner();
        _adsInterstitial?.LoadAd();
        _adsReward?.LoadAd();
        Debug.Log("Unity Adsの初期化が完了しました。");
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.Log($"Unity Adsの初期化に失敗しました: {error.ToString()} - {message}");
    }
}
