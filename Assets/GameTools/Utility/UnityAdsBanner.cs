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
            yield return new WaitForSeconds(0.3f); // 0.3�b��ɍL���\��
        }
        Advertisement.Banner.SetPosition(bannerPosition); //�o�i�[���㕔�����ɃZ�b�g
        Advertisement.Banner.Show(bannerId);
    }

    public void OnInitializationComplete()
    {
        Debug.Log("����������");
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.LogError("���������s");
    }
}
