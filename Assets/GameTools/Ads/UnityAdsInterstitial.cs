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
        // ���݂̃v���b�g�t�H�[���̍L�����j�b�gID���擾���܂�:
        _adUnitId = (Application.platform == RuntimePlatform.IPhonePlayer)
            ? _iOSAdUnitId
            : _androidAdUnitId;
    }

    // �L�����j�b�g�ɃR���e���c�����[�h���܂�:
    public void LoadAd()
    {
        // �d�v�I��������ɂ̂݃R���e���c�����[�h���܂��i���̗�ł́A�������͕ʂ̃X�N���v�g�ŏ�������܂��j�B
        Debug.Log("�C���X�e�L���̃��[�h��: " + _adUnitId);
        Advertisement.Load(_adUnitId, this);
    }

    // ���[�h���ꂽ�R���e���c���L�����j�b�g�ŕ\�����܂�:
    public async void ShowAd(float wait)
    {

        await UniTask.Delay(TimeSpan.FromSeconds(wait));
        // ����: �����L���R���e���c�����O�Ƀ��[�h����Ă��Ȃ��ꍇ�A���̃��\�b�h�͎��s���܂�
        Debug.Log("�C���X�e�L���̕\����: " + _adUnitId);
        Advertisement.Show(_adUnitId, this);
    }

    // Load Listener �� Show Listener �C���^�[�t�F�[�X�̃��\�b�h���������܂�:
    public void OnUnityAdsAdLoaded(string adUnitId)
    {
        // �L�����j�b�g���R���e���c�̃��[�h�ɐ��������ꍇ�A�K�v�ɉ����ăR�[�h�����s���܂��B
    }

    public void OnUnityAdsFailedToLoad(string _adUnitId, UnityAdsLoadError error, string message)
    {
        Debug.Log($"�L�����j�b�g�̃��[�h�G���[: {_adUnitId} - {error.ToString()} - {message}");
        // �L�����j�b�g�̃��[�h�Ɏ��s�����ꍇ�A�Ď��s�Ȃǂ̃R�[�h�����s����Ȃǂ̃I�v�V����������܂��B
    }

    public void OnUnityAdsShowFailure(string _adUnitId, UnityAdsShowError error, string message)
    {
        Debug.Log($"�L�����j�b�g�̕\���G���[ {_adUnitId}: {error.ToString()} - {message}");
        // �L�����j�b�g�̕\���Ɏ��s�����ꍇ�A�ʂ̍L�������[�h����Ȃǂ̃I�v�V����������܂��B
    }

    public void OnUnityAdsShowStart(string _adUnitId) { }
    public void OnUnityAdsShowClick(string _adUnitId) { }
    public void OnUnityAdsShowComplete(string _adUnitId, UnityAdsShowCompletionState showCompletionState) { }
}
