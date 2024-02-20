using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataSaveUtility<T> : Singleton<DataSaveUtility<T>> where T : ScriptableObject
{
    private T data;     // json変換するデータのクラス
    public T Data => data;
    string filepath;                            // jsonファイルのパス
    string fileName = "PLD.json";              // jsonファイル名

    private static readonly string playerSaveDataObjectPath = "SaveData/SaveDataObject";

    //ファイルチェック、読み込み
    public void GetData()
    {
#if UNITY_EDITOR
        filepath = Application.dataPath + "/" + fileName;

#elif UNITY_ANDROID
    filepath = Application.persistentDataPath + "/" + fileName;    

#endif
        if (!File.Exists(filepath))
        {
            Debug.Log("データが存在しない");
            data = Resources.Load<T>(playerSaveDataObjectPath);
        }
        else
        {
            Debug.Log("データが存在する");
            StreamReader rd = new StreamReader(filepath);               // ファイル読み込み指定
            string json = rd.ReadToEnd();                           // ファイル内容全て読み込む
            rd.Close();                                             // ファイル閉じる

            T newData = default;
            JsonUtility.ToJson(newData);
            JsonUtility.FromJsonOverwrite(json, newData);
            data = newData;
        }
        
        Debug.Log(data is null);
    }

    // jsonとしてデータを保存
    public void Save()
    {
#if UNITY_EDITOR
        filepath = Application.dataPath + "/" + fileName;

#elif UNITY_ANDROID
    filepath = Application.persistentDataPath + "/" + fileName;    

#endif
        string json = JsonUtility.ToJson(data);                 // jsonとして変換
        StreamWriter wr = new StreamWriter(filepath, false);    // ファイル書き込み指定
        wr.WriteLine(json);                                     // json変換した情報を書き込み
        wr.Close();                                             // ファイル閉じる
    }
}
