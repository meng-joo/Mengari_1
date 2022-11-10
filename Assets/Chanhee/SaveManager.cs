using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[Serializable]
public struct ReturnUser
{
    public bool success;
    public User msg;
}

[Serializable]
public struct ReturnMsg
{
    public bool success;
    public string msg;
}

public class SaveManager : MonoSingleton<SaveManager>
{
    #region SERVER
    private readonly string URL = "http://172.31.3.19:50000";

    private string DEVICEID = string.Empty;
    private bool isLoading = false;
    #endregion

    [SerializeField] UserData userData;
    public UserData USERDATA
    {
        get => userData;
        set => userData = value;
    }
    [SerializeField] private ItemDataList itemDataList;
    public ItemDataList ITEMDATALIST
    {
        get => itemDataList;
        set => itemDataList = value;
    }


    private void Start()
    {
        DEVICEID = SystemInfo.deviceUniqueIdentifier;

        StartCoroutine(A());

        if (!isLoading)
        {
            isLoading = true;
            LoadUserData();
        }
    }

    IEnumerator A()
    {
        UnityWebRequest req = UnityWebRequest.Get($"{URL}/userList");

        yield return req.SendWebRequest();
    }

    private void OnApplicationQuit()
    {
        SaveUserData();
    }

    public void LoadUserData()
    {
        WWWForm form = new WWWForm();
        form.AddField("deviceId", DEVICEID);

        LoadData("/userInfo", (success, data) =>
        {
            // 서버 연결
            if (success)
            {
                ReturnUser returnUser = JsonUtility.FromJson<ReturnUser>(data);
                // 값이 있음
                if (returnUser.success == true)
                {
                    userData.SetUserData(returnUser.msg);
                }
                // 없음
                else
                {
                    userData.ResetUserData();

                    isLoading = true;
                    SaveUserData();
                }
            }
        }, form);

        isLoading = false;
    }

    public void SaveUserData()
    {
        WWWForm form = new WWWForm();
        form.AddField("deviceId", DEVICEID);
        form.AddField("userName", userData.userName);
        form.AddField("userMoney", userData.userMoney);
        form.AddField("frontStageNumber", userData.frontStageNumber);
        form.AddField("backStageNumber", userData.backStageNumber);

        SaveData("/user", (success, data) =>
        {
            // 서버 연결
            if (success)
            {
                ReturnMsg returnMsg = JsonUtility.FromJson<ReturnMsg>(data);
                if (returnMsg.success == true)
                {
                    Debug.Log("Save");
                    return;
                }
                else
                {
                    Debug.Log(returnMsg.msg);
                    return;
                }
            }
        }, form);

        isLoading = false;
    }

    public void LoadData(string uri, Action<bool, string> Callback, WWWForm form = null)
    {
        StartCoroutine(LoadCoroutine(uri, Callback, form));
    }
    private IEnumerator LoadCoroutine(string uri, Action<bool, string> Callback, WWWForm form = null)
    {
        UnityWebRequest req = UnityWebRequest.Post($"{URL}{uri}", form);

        yield return req.SendWebRequest();

        if (req.result == UnityWebRequest.Result.Success)
        {
            Callback(true, req.downloadHandler.text);
        }
        else
        {
            Callback(false, req.error);
        }

        isLoading = false;
    }
    public void SaveData(string uri, Action<bool, string> Callback, WWWForm form = null)
    {
        StartCoroutine(SaveCoroutine(uri, Callback, form));
    }
    private IEnumerator SaveCoroutine(string uri, Action<bool, string> Callback, WWWForm form = null)
    {
        UnityWebRequest req = UnityWebRequest.Post($"{URL}{uri}", form);

        yield return req.SendWebRequest();

        if (req.result == UnityWebRequest.Result.Success)
        {
            Callback(true, req.downloadHandler.text);
        }
        else
        {
            Callback(false, req.error);
        }
    }


}