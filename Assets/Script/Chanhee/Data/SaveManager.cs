using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class SaveManager : MonoBehaviour
{
    [SerializeField] UserSaveData _userSaveData;
    public UserSaveData UserSaveData
    {
        get => _userSaveData;
        set => _userSaveData = value;
    }


    public int LoadData(string str, int data)
    {
        return PlayerPrefs.GetInt(str, data);
    }
    public string LoadData(string str, string data)
    {
        return PlayerPrefs.GetString(str, data);
    }
    public List<T> LoadListData<T>(string str)
    {
        string data = PlayerPrefs.GetString(str, "");
        List<T> datas = JsonConvert.DeserializeObject<List<T>>(data);
        return datas;
    }


    public void SaveData(string str, int data)
    {
        PlayerPrefs.SetInt(str, data);
    }
    public void SaveData(string str, string data)
    {
        PlayerPrefs.SetString(str, data);
    }
    public void SaveData<T>(string str, List<T> data)
    {
        string jsonStr = JsonConvert.SerializeObject(data);
        PlayerPrefs.SetString(str, jsonStr);
    }

    public void DeleteData(string str)
    {
        PlayerPrefs.DeleteKey(str);
    }
}
