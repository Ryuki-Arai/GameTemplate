using Cysharp.Threading.Tasks;
using System;
using UnityEngine;
using UnityEngine.Advertisements;

public class UnityAdsInterstitial : SingletonMonoBehaviour<UnityAdsInterstitial>, IUnityAdsLoadListener, IUnityAdsShowListener
{
    [SerializeField] string _androidAdUnitId = "Interstitial_Android";
    [SerializeField] string _iOSAdUnitId = "Interstitial_iOS";
    string _adUnitId;

    override protected void Awake()
    {
        base.Awake();
        // 現在のプラットフォームの広告ユニットIDを取得します:
        _adUnitId = (Application.platform == RuntimePlatform.IPhonePlayer)
            ? _iOSAdUnitId
            : _androidAdUnitId;
    }

    // 広告ユニットにコンテンツをロードします:
    public void LoadAd()
    {
        // 重要！初期化後にのみコンテンツをロードします（この例では、初期化は別のスクリプトで処理されます）。
        Debug.Log("インステ広告のロード中: " + _adUnitId);
        Advertisement.Load(_adUnitId, this);
    }

    // ロードされたコンテンツを広告ユニットで表示します:
    public async void ShowAd(float wait)
    {

        await UniTask.Delay(TimeSpan.FromSeconds(wait));
        // 注意: もし広告コンテンツが事前にロードされていない場合、このメソッドは失敗します
        Debug.Log("インステ広告の表示中: " + _adUnitId);
        Advertisement.Show(_adUnitId, this);
    }

    // Load Listener と Show Listener インターフェースのメソッドを実装します:
    public void OnUnityAdsAdLoaded(string adUnitId)
    {
        // 広告ユニットがコンテンツのロードに成功した場合、必要に応じてコードを実行します。
    }

    public void OnUnityAdsFailedToLoad(string _adUnitId, UnityAdsLoadError error, string message)
    {
        Debug.Log($"広告ユニットのロードエラー: {_adUnitId} - {error.ToString()} - {message}");
        // 広告ユニットのロードに失敗した場合、再試行などのコードを実行するなどのオプションがあります。
    }

    public void OnUnityAdsShowFailure(string _adUnitId, UnityAdsShowError error, string message)
    {
        Debug.Log($"広告ユニットの表示エラー {_adUnitId}: {error.ToString()} - {message}");
        // 広告ユニットの表示に失敗した場合、別の広告をロードするなどのオプションがあります。
    }

    public void OnUnityAdsShowStart(string _adUnitId) { }
    public void OnUnityAdsShowClick(string _adUnitId) { }
    public void OnUnityAdsShowComplete(string _adUnitId, UnityAdsShowCompletionState showCompletionState) { }
}
