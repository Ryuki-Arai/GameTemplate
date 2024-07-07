using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;

public class UnityAdsReward : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
{
    [SerializeField] string _androidAdUnitId = "Rewarded_Android";
    [SerializeField] string _iOSAdUnitId = "Rewarded_iOS";
    string _adUnitId = null; // サポートされていないプラットフォームではこれはnullのままです

    void Awake()
    {
        // 現在のプラットフォームの広告ユニットIDを取得します:
        _adUnitId = (Application.platform == RuntimePlatform.IPhonePlayer)
            ? _iOSAdUnitId
            : _androidAdUnitId;
    }

    // 広告を表示する準備ができたときにこのパブリックメソッドを呼び出します。
    public void LoadAd()
    {
        // 重要！初期化後にのみコンテンツをロードします（この例では、初期化は別のスクリプトで処理されます）。
        Debug.Log("リワード広告のロード中: " + _adUnitId);
        Advertisement.Load(_adUnitId, this);
    }

    // 広告が正常にロードされた場合、ボタンにリスナーを追加して有効にします:
    public void OnUnityAdsAdLoaded(string adUnitId)
    {
        Debug.Log("リワード広告がロードされました: " + adUnitId);
    }

    // ユーザーがボタンをクリックしたときに実行するメソッドを実装します:
    public void ShowAd()
    {
        // その後、広告を表示します:
        Advertisement.Show(_adUnitId, this);
    }

    // Show Listener の OnUnityAdsShowComplete コールバックメソッドを実装して、ユーザーが報酬を得たかどうかを判断します:
    public void OnUnityAdsShowComplete(string adUnitId, UnityAdsShowCompletionState showCompletionState)
    {
        if (adUnitId.Equals(_adUnitId) && showCompletionState.Equals(UnityAdsShowCompletionState.COMPLETED))
        {
            Debug.Log("Unity Adsの報酬付き広告が完了しました");
            // 報酬を付与します。
        }
    }

    // Load と Show Listener のエラーコールバックを実装します:
    public void OnUnityAdsFailedToLoad(string adUnitId, UnityAdsLoadError error, string message)
    {
        Debug.Log($"広告ユニットのロードエラー {adUnitId}: {error.ToString()} - {message}");
        // エラーの詳細を使用して、別の広告をロードするかどうかを判断します。
    }

    public void OnUnityAdsShowFailure(string adUnitId, UnityAdsShowError error, string message)
    {
        Debug.Log($"広告ユニットの表示エラー {adUnitId}: {error.ToString()} - {message}");
        // エラーの詳細を使用して、別の広告をロードするかどうかを判断します。
    }

    public void OnUnityAdsShowStart(string adUnitId) { }
    public void OnUnityAdsShowClick(string adUnitId) { }

    void OnDestroy()
    {
        // ボタンリスナーをクリーンアップします:
    }
}
