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

public class SaveManager : MonoBehaviour
{
    #region SERVER
    private readonly string URL = "http://localhost:50000";

    private string DEVICEID = string.Empty;
    private bool isLoading = false;
    #endregion

    [SerializeField] UserData userData;

    private void Start()
    {
        DEVICEID = SystemInfo.deviceUniqueIdentifier;

        if (!isLoading)
        {
            isLoading = true;
            LoadUserData();
        }
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
            // ���� ����
            if (success)
            {
                ReturnUser returnUser = JsonUtility.FromJson<ReturnUser>(data);
                // ���� ����
                if (returnUser.success == true)
                {
                    userData.SetUserData(returnUser.msg);
                }
                // ����
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
        form.AddField("frontStageNumber", userData.frontStageNumber);
        form.AddField("backStageNumber", userData.backStageNumber);

        SaveData("/user", (success, data) =>
        {
            // ���� ����
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