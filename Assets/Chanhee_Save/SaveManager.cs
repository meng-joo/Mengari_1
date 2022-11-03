using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

[Serializable]
public struct ReturnUser
{
    public bool success;
    public UserSave msg;
}

[Serializable]
public struct ReturnShop
{
    public bool success;
    public List<ShopInfo> msg;
}

public class SaveManager : MonoSingleton<SaveManager>
{
    #region Server
    private const string URL = "http://localhost:50000";
    private string DEVICEID = string.Empty;
    #endregion

    [SerializeField] UserSave user;
    [SerializeField] List<ShopInfo> shopInfos;

    public Image image;

    private void Start()
    {
        DEVICEID = SystemInfo.deviceUniqueIdentifier;

        LoadUserData();
        LoadShopData();
        Invoke("SetShopData", 1f);
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            PlayerPrefs.DeleteAll();
            Debug.Log("키 삭제");
        }
    }

    private void OnApplicationQuit()
    {
        WWWForm form = new WWWForm();

        form.AddField("deviceId", DEVICEID);
        form.AddField("userName", user.userName);
        form.AddField("userInfo", user.userInfo);

        SaveUserData(form);
    }

    public void LoadUserData()
    {
        WWWForm form = new WWWForm();

        form.AddField("deviceId", DEVICEID);
        LoadData("/userInfo", form, (success, json) =>
        {
            if (success)
            {
                ReturnUser returnUser = JsonUtility.FromJson<ReturnUser>(json);
                user = returnUser.msg;

                if (!returnUser.success)
                {
                    form = new WWWForm();

                    form.AddField("deviceId", DEVICEID);
                    form.AddField("userName", "");
                    form.AddField("userInfo", "");

                    SaveUserData(form);
                }

                string data = JsonConvert.SerializeObject(user);
                PlayerPrefs.SetString("USERINFO", data);
            }
            else
            {
                string data = PlayerPrefs.GetString("USERINFO", "");
                user = JsonConvert.DeserializeObject<UserSave>(data);
                return;
            }

            user.deviceId = DEVICEID;
        });
    }

    public void LoadShopData()
    {
        LoadData("/shopInfo", null, (success, json) =>
        {
            if (success)
            {
                ReturnShop returnShop = JsonUtility.FromJson<ReturnShop>(json);
                shopInfos = returnShop.msg;

                string data = JsonConvert.SerializeObject(shopInfos);
                PlayerPrefs.SetString("SHOPINFO", data);
            }
            else
            {
                string data = PlayerPrefs.GetString("SHOPINFO", "");
                shopInfos = JsonConvert.DeserializeObject<List<ShopInfo>>(data);
            }
        });
    }

    public void SetShopData()
    {
        // Item 생성
        foreach (ShopInfo item in shopInfos)
        {
            StartCoroutine(ImageCoroutine($"/image/{item.skinName}", (success, sprite) =>
            {
                if (success)
                {
                    Debug.Log("clear");
                    item.sprite = sprite;
                    image.sprite = item.sprite;
                }
                else
                {
                    Debug.Log("error");
                }
            }));
        }

        //string data = JsonConvert.SerializeObject(shopInfos);
        //PlayerPrefs.SetString("SHOPINFO", data);
    }

    public void SaveUserData(WWWForm form = null)
    {
        SaveData("/user", form, (success, json) =>
        {
            if (success)
            {
                return;
            }
            else
            {
                string data = JsonConvert.SerializeObject(user);
                PlayerPrefs.SetString("USERINFO", data);
            }
        });
    }

    void LoadData(string uri, WWWForm form = null, Action<bool, string> Callback = null)
    {
        StartCoroutine(DataCoroutine(uri, form, Callback));
    }
    void SaveData(string uri, WWWForm form = null, Action<bool, string> Callback = null)
    {
        StartCoroutine(DataCoroutine(uri, form, Callback));
    }

    public IEnumerator DataCoroutine(string uri, WWWForm form = null, Action<bool, string> Callback = null)
    {
        UnityWebRequest req = null;
        if (form == null)
            req = UnityWebRequest.Get($"{URL}{uri}");
        else
            req = UnityWebRequest.Post($"{URL}{uri}", form);

        yield return req.SendWebRequest();

        if (req.result == UnityWebRequest.Result.Success)
        {
            Callback(true, req.downloadHandler.text);
        }
        else
        {
            Callback(false, "");
        }
    }

    public IEnumerator ImageCoroutine(string uri, Action<bool, Sprite> Callback)
    {

        UnityWebRequest req = UnityWebRequestTexture.GetTexture($"{URL}{uri}");

        yield return req.SendWebRequest();

        if (req.result == UnityWebRequest.Result.Success)
        {
            Texture2D texture = ((DownloadHandlerTexture)req.downloadHandler).texture as Texture2D;

            Rect rect = new Rect(0, 0, texture.width, texture.height);
            Vector2 pivot = new Vector2(0.5f, 0.5f);
            Sprite s = Sprite.Create(texture, rect, pivot);

            Callback(true, s);
        }
        else
        {
            Callback(false, null);
        }
    }
}
