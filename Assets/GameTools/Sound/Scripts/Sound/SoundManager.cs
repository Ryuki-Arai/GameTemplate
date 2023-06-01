using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField]
    private SoundData[] _soundsData;

    private GameObject _playingBGM;

    /// <summary>
    /// 再生させたい音声を名前で呼び出す。
    /// </summary>
    /// <param name="soundName">呼び出したい音声データの名前</param>
    public void CallSound(string soundName)
    {
        //指定された名前の音声データをデータ配列から取得、無い場合はそのまま終了
        var soundData = GetSoundData(soundName);
        if (soundData == null)
        {
            Debug.LogError("サウンドがありません");
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
    /// BGMを停止させる。再開は不可。
    /// </summary>
    public void StopBGM()
    {
        Destroy(_playingBGM);
    }

    /// <summary>
    /// データ配列から該当の音声データを呼び出す。
    /// </summary>
    /// <param name="soundName">呼び出したい音声データの名前</param>
    /// <returns>音声データ。データが無い場合はNull。</returns>
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
    /// 音を再生する。
    /// </summary>
    /// <param name="soundData">音声データ</param>
    private void SoundPlay(SoundData soundData)
    {

        //オブジェクト生成＆データ初期化
        var soundObj = new GameObject(soundData.Name);
        AudioSource audioSource = soundObj.AddComponent<AudioSource>();

        audioSource.clip = soundData.Clip;
        audioSource.volume = soundData.Volume;
        audioSource.loop = soundData.IsLoop;
        audioSource.Play();
        if(soundData.Type == SoundType.BGM) _playingBGM = soundObj;

        //ループしない場合、音声終了後に削除
        if (!soundData.IsLoop)
        {
            Destroy(soundObj, soundData.Clip.length);
        }
    }
}
