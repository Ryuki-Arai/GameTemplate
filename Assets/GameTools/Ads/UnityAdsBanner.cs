using UnityEngine;
using UnityEngine.Advertisements;

public class UnityAdsBanner : SingletonMonoBehaviour<UnityAdsBanner>
{
    [SerializeField] BannerPosition _bannerPosition = BannerPosition.BOTTOM_CENTER;

    [SerializeField] string _androidAdUnitId = "Banner_Android";
    [SerializeField] string _iOSAdUnitId = "Banner_iOS";
    string _adUnitId = null; // サポートされていないプラットフォームではこれはnullのままです。

    protected override void Awake()
    {
        base.Awake();
        // 現在のプラットフォームの広告ユニットIDを取得します:
        _adUnitId = (Application.platform == RuntimePlatform.IPhonePlayer)
            ? _iOSAdUnitId
            : _androidAdUnitId;

        // バナーの位置を設定します:
        Advertisement.Banner.SetPosition(_bannerPosition);

        // LoadBanner() メソッドを呼び出します:
        LoadBanner();
    }

    // Load Banner ボタンがクリックされたときに呼び出すメソッドを実装します:
    public void LoadBanner()
    {
        // SDKにロードイベントを通知するオプションを設定します:
        BannerLoadOptions options = new BannerLoadOptions
        {
            loadCallback = OnBannerLoaded,
            errorCallback = OnBannerError
        };

        // バナーコンテンツを含む広告ユニットをロードします:
        Advertisement.Banner.Load(_adUnitId, options);
    }

    // loadCallback イベントがトリガーされたときに実行するコードを実装します:
    void OnBannerLoaded()
    {
        Debug.Log("バナーがロードされました");

        ShowBannerAd();
    }

    // load errorCallback イベントがトリガーされたときに実行するコードを実装します:
    void OnBannerError(string message)
    {
        Debug.Log($"バナーエラー: {message}");
        // オプションとして、別の広告をロードするなどの追加のコードを実行することができます。
    }

    // Show Banner ボタンがクリックされたときに呼び出すメソッドを実装します:
    void ShowBannerAd()
    {
        // SDKに表示イベントを通知するオプションを設定します:
        BannerOptions options = new BannerOptions
        {
            clickCallback = OnBannerClicked,
            hideCallback = OnBannerHidden,
            showCallback = OnBannerShown
        };

        // ロードされたバナー広告ユニットを表示します:
        Advertisement.Banner.Show(_adUnitId, options);
    }

    // Hide Banner ボタンがクリックされたときに呼び出すメソッドを実装します:
    void HideBannerAd()
    {
        // バナーを非表示にします:
        Advertisement.Banner.Hide();
    }

    void OnBannerClicked() { }
    void OnBannerShown() { }
    void OnBannerHidden() { }
}
