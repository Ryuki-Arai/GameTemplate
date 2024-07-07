using UnityEngine;
using UnityEngine.Advertisements;

public class UnityAdsBanner : SingletonMonoBehaviour<UnityAdsBanner>
{
    [SerializeField] BannerPosition _bannerPosition = BannerPosition.BOTTOM_CENTER;

    [SerializeField] string _androidAdUnitId = "Banner_Android";
    [SerializeField] string _iOSAdUnitId = "Banner_iOS";
    string _adUnitId = null; // �T�|�[�g����Ă��Ȃ��v���b�g�t�H�[���ł͂����null�̂܂܂ł��B

    protected override void Awake()
    {
        base.Awake();
        // ���݂̃v���b�g�t�H�[���̍L�����j�b�gID���擾���܂�:
        _adUnitId = (Application.platform == RuntimePlatform.IPhonePlayer)
            ? _iOSAdUnitId
            : _androidAdUnitId;

        // �o�i�[�̈ʒu��ݒ肵�܂�:
        Advertisement.Banner.SetPosition(_bannerPosition);

        // LoadBanner() ���\�b�h���Ăяo���܂�:
        LoadBanner();
    }

    // Load Banner �{�^�����N���b�N���ꂽ�Ƃ��ɌĂяo�����\�b�h���������܂�:
    public void LoadBanner()
    {
        // SDK�Ƀ��[�h�C�x���g��ʒm����I�v�V������ݒ肵�܂�:
        BannerLoadOptions options = new BannerLoadOptions
        {
            loadCallback = OnBannerLoaded,
            errorCallback = OnBannerError
        };

        // �o�i�[�R���e���c���܂ލL�����j�b�g�����[�h���܂�:
        Advertisement.Banner.Load(_adUnitId, options);
    }

    // loadCallback �C�x���g���g���K�[���ꂽ�Ƃ��Ɏ��s����R�[�h���������܂�:
    void OnBannerLoaded()
    {
        Debug.Log("�o�i�[�����[�h����܂���");

        ShowBannerAd();
    }

    // load errorCallback �C�x���g���g���K�[���ꂽ�Ƃ��Ɏ��s����R�[�h���������܂�:
    void OnBannerError(string message)
    {
        Debug.Log($"�o�i�[�G���[: {message}");
        // �I�v�V�����Ƃ��āA�ʂ̍L�������[�h����Ȃǂ̒ǉ��̃R�[�h�����s���邱�Ƃ��ł��܂��B
    }

    // Show Banner �{�^�����N���b�N���ꂽ�Ƃ��ɌĂяo�����\�b�h���������܂�:
    void ShowBannerAd()
    {
        // SDK�ɕ\���C�x���g��ʒm����I�v�V������ݒ肵�܂�:
        BannerOptions options = new BannerOptions
        {
            clickCallback = OnBannerClicked,
            hideCallback = OnBannerHidden,
            showCallback = OnBannerShown
        };

        // ���[�h���ꂽ�o�i�[�L�����j�b�g��\�����܂�:
        Advertisement.Banner.Show(_adUnitId, options);
    }

    // Hide Banner �{�^�����N���b�N���ꂽ�Ƃ��ɌĂяo�����\�b�h���������܂�:
    void HideBannerAd()
    {
        // �o�i�[���\���ɂ��܂�:
        Advertisement.Banner.Hide();
    }

    void OnBannerClicked() { }
    void OnBannerShown() { }
    void OnBannerHidden() { }
}
