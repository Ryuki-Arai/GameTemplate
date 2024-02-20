using UnityEngine;
using UnityEngine.Advertisements;
using System.Collections;

public class UnityAdsBanner : MonoBehaviour, IUnityAdsInitializationListener
{
    [SerializeField] private string gameId = "5530181";
    [SerializeField] private string bannerId = "Banner_Android";
    [SerializeField] BannerPosition bannerPosition;
    public bool testMode = true;

    void Start()
    {
        Advertisement.Initialize(gameId, testMode, this);
        StartCoroutine(showBanner());
    }

    IEnumerator showBanner()
    {
        while (!Advertisement.isInitialized)
        {
            yield return new WaitForSeconds(0.3f); // 0.3秒後に広告表示
        }
        Advertisement.Banner.SetPosition(bannerPosition); //バナーを上部中央にセット
        Advertisement.Banner.Show(bannerId);
    }

    public void OnInitializationComplete()
    {
        Debug.Log("初期化成功");
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.LogError("初期化失敗");
    }
}
