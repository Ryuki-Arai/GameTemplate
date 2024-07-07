using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;

public class UnityAdsReward : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
{
    [SerializeField] string _androidAdUnitId = "Rewarded_Android";
    [SerializeField] string _iOSAdUnitId = "Rewarded_iOS";
    string _adUnitId = null; // �T�|�[�g����Ă��Ȃ��v���b�g�t�H�[���ł͂����null�̂܂܂ł�

    void Awake()
    {
        // ���݂̃v���b�g�t�H�[���̍L�����j�b�gID���擾���܂�:
        _adUnitId = (Application.platform == RuntimePlatform.IPhonePlayer)
            ? _iOSAdUnitId
            : _androidAdUnitId;
    }

    // �L����\�����鏀�����ł����Ƃ��ɂ��̃p�u���b�N���\�b�h���Ăяo���܂��B
    public void LoadAd()
    {
        // �d�v�I��������ɂ̂݃R���e���c�����[�h���܂��i���̗�ł́A�������͕ʂ̃X�N���v�g�ŏ�������܂��j�B
        Debug.Log("�����[�h�L���̃��[�h��: " + _adUnitId);
        Advertisement.Load(_adUnitId, this);
    }

    // �L��������Ƀ��[�h���ꂽ�ꍇ�A�{�^���Ƀ��X�i�[��ǉ����ėL���ɂ��܂�:
    public void OnUnityAdsAdLoaded(string adUnitId)
    {
        Debug.Log("�����[�h�L�������[�h����܂���: " + adUnitId);
    }

    // ���[�U�[���{�^�����N���b�N�����Ƃ��Ɏ��s���郁�\�b�h���������܂�:
    public void ShowAd()
    {
        // ���̌�A�L����\�����܂�:
        Advertisement.Show(_adUnitId, this);
    }

    // Show Listener �� OnUnityAdsShowComplete �R�[���o�b�N���\�b�h���������āA���[�U�[����V�𓾂����ǂ����𔻒f���܂�:
    public void OnUnityAdsShowComplete(string adUnitId, UnityAdsShowCompletionState showCompletionState)
    {
        if (adUnitId.Equals(_adUnitId) && showCompletionState.Equals(UnityAdsShowCompletionState.COMPLETED))
        {
            Debug.Log("Unity Ads�̕�V�t���L�����������܂���");
            // ��V��t�^���܂��B
        }
    }

    // Load �� Show Listener �̃G���[�R�[���o�b�N���������܂�:
    public void OnUnityAdsFailedToLoad(string adUnitId, UnityAdsLoadError error, string message)
    {
        Debug.Log($"�L�����j�b�g�̃��[�h�G���[ {adUnitId}: {error.ToString()} - {message}");
        // �G���[�̏ڍׂ��g�p���āA�ʂ̍L�������[�h���邩�ǂ����𔻒f���܂��B
    }

    public void OnUnityAdsShowFailure(string adUnitId, UnityAdsShowError error, string message)
    {
        Debug.Log($"�L�����j�b�g�̕\���G���[ {adUnitId}: {error.ToString()} - {message}");
        // �G���[�̏ڍׂ��g�p���āA�ʂ̍L�������[�h���邩�ǂ����𔻒f���܂��B
    }

    public void OnUnityAdsShowStart(string adUnitId) { }
    public void OnUnityAdsShowClick(string adUnitId) { }

    void OnDestroy()
    {
        // �{�^�����X�i�[���N���[���A�b�v���܂�:
    }
}
