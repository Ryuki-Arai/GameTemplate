using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField]
    private SoundData[] _soundsData;

    private GameObject _playingBGM;

    /// <summary>
    /// �Đ��������������𖼑O�ŌĂяo���B
    /// </summary>
    /// <param name="soundName">�Ăяo�����������f�[�^�̖��O</param>
    public void CallSound(string soundName)
    {
        //�w�肳�ꂽ���O�̉����f�[�^���f�[�^�z�񂩂�擾�A�����ꍇ�͂��̂܂܏I��
        var soundData = GetSoundData(soundName);
        if (soundData == null)
        {
            Debug.LogError("�T�E���h������܂���");
            return;
        }

        switch (soundData.Type)
        {
            case SoundType.BGM:
                if (_playingBGM)
                {
                    Destroy(_playingBGM);
                }
                SoundPlay(soundData);
                break;
            case SoundType.ME:
            case SoundType.SE:
                SoundPlay(soundData);
                break;
        }
    }

    /// <summary>
    /// BGM���~������B�ĊJ�͕s�B
    /// </summary>
    public void StopBGM()
    {
        Destroy(_playingBGM);
    }

    /// <summary>
    /// �f�[�^�z�񂩂�Y���̉����f�[�^���Ăяo���B
    /// </summary>
    /// <param name="soundName">�Ăяo�����������f�[�^�̖��O</param>
    /// <returns>�����f�[�^�B�f�[�^�������ꍇ��Null�B</returns>
    private SoundData GetSoundData(string soundName)
    {
        for(int i = 0; i < _soundsData.Length; i++)
        {
            if (_soundsData[i].Name == soundName)
            {
                return _soundsData[i];
            }
        }
        return null;
    }

    /// <summary>
    /// �����Đ�����B
    /// </summary>
    /// <param name="soundData">�����f�[�^</param>
    private void SoundPlay(SoundData soundData)
    {

        //�I�u�W�F�N�g�������f�[�^������
        var soundObj = new GameObject(soundData.Name);
        AudioSource audioSource = soundObj.AddComponent<AudioSource>();

        audioSource.clip = soundData.Clip;
        audioSource.volume = soundData.Volume;
        audioSource.loop = soundData.IsLoop;
        audioSource.Play();
        if(soundData.Type == SoundType.BGM) _playingBGM = soundObj;

        //���[�v���Ȃ��ꍇ�A�����I����ɍ폜
        if (!soundData.IsLoop)
        {
            Destroy(soundObj, soundData.Clip.length);
        }
    }
}
