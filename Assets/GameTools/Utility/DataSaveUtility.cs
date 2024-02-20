using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataSaveUtility<T> : Singleton<DataSaveUtility<T>> where T : ScriptableObject
{
    private T data;     // json�ϊ�����f�[�^�̃N���X
    public T Data => data;
    string filepath;                            // json�t�@�C���̃p�X
    string fileName = "PLD.json";              // json�t�@�C����

    private static readonly string playerSaveDataObjectPath = "SaveData/SaveDataObject";

    //�t�@�C���`�F�b�N�A�ǂݍ���
    public void GetData()
    {
#if UNITY_EDITOR
        filepath = Application.dataPath + "/" + fileName;

#elif UNITY_ANDROID
    filepath = Application.persistentDataPath + "/" + fileName;    

#endif
        if (!File.Exists(filepath))
        {
            Debug.Log("�f�[�^�����݂��Ȃ�");
            data = Resources.Load<T>(playerSaveDataObjectPath);
        }
        else
        {
            Debug.Log("�f�[�^�����݂���");
            StreamReader rd = new StreamReader(filepath);               // �t�@�C���ǂݍ��ݎw��
            string json = rd.ReadToEnd();                           // �t�@�C�����e�S�ēǂݍ���
            rd.Close();                                             // �t�@�C������

            T newData = default;
            JsonUtility.ToJson(newData);
            JsonUtility.FromJsonOverwrite(json, newData);
            data = newData;
        }
        
        Debug.Log(data is null);
    }

    // json�Ƃ��ăf�[�^��ۑ�
    public void Save()
    {
#if UNITY_EDITOR
        filepath = Application.dataPath + "/" + fileName;

#elif UNITY_ANDROID
    filepath = Application.persistentDataPath + "/" + fileName;    

#endif
        string json = JsonUtility.ToJson(data);                 // json�Ƃ��ĕϊ�
        StreamWriter wr = new StreamWriter(filepath, false);    // �t�@�C���������ݎw��
        wr.WriteLine(json);                                     // json�ϊ�����������������
        wr.Close();                                             // �t�@�C������
    }
}
