using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class UserSaveData : MonoBehaviour
{
    public SaveManager _saveManager = null;

    public void Start()
    {
        LoadData();
    }
    public void LoadData()
    {
        Debug.Log("Load Data");

        UserName = _saveManager.LoadData(_playerPrefsUserName, "");
        UserMoney = _saveManager.LoadData(_playerPrefsUserMoney, 0);
        UserStage = _saveManager.LoadData(_playerPrefsUserStage, "");
        UserSkins = _saveManager.LoadListData<User_Skin>(_playerPrefsUserSkins);
    }
    private void Update()
    {
        // Test
        if (Input.GetKeyDown(KeyCode.P)) SaveData();
        if (Input.GetKeyDown(KeyCode.O)) ResetData();
    }

    public void SaveData()
    {
        Debug.Log("Save Data");

        UserName = _userName;
        UserMoney = _userMoney;
        UserStage = _userStage;
        UserSkins = _userSkins;
    }

    public void ResetData()
    {
        Debug.Log("Reset Data");

        _saveManager.DeleteData(_playerPrefsUserName);
        _saveManager.DeleteData(_playerPrefsUserMoney);
        _saveManager.DeleteData(_playerPrefsUserStage);
        _saveManager.DeleteData(_playerPrefsUserSkins);
    }



    [SerializeField, Tooltip("User Name")] private string _userName;
    public string UserName
    {
        get => _userName;
        set
        {
            _userName = value;
            _saveManager.SaveData(_playerPrefsUserName, _userName);
        }
    }
    private readonly string _playerPrefsUserName = "PLAYERPREFSUSERNAME";



    [SerializeField, Tooltip("User Money")] private int _userMoney;
    public int UserMoney
    {
        get => _userMoney;
        set
        {
            _userMoney = value;
            _saveManager.SaveData(_playerPrefsUserMoney, _userMoney);
        }
    }
    private readonly string _playerPrefsUserMoney = "PLAYERPREFSUSERMONEY";



    [SerializeField, Tooltip("User Stage")] private string _userStage;
    public string UserStage
    {
        get => _userStage;
        set
        {
            _userStage = value;
            _saveManager.SaveData(_playerPrefsUserStage, _userStage);
        }
    }
    private readonly string _playerPrefsUserStage = "PLAYERPREFSUSERSTAGE";



    [SerializeField, Tooltip("User Skins")] private List<User_Skin> _userSkins = new List<User_Skin>();
    public List<User_Skin> UserSkins
    {
        get => _userSkins;
        set
        {
            _userSkins = value;
            _saveManager.SaveData(_playerPrefsUserSkins, _userSkins);
        }
    }
    private readonly string _playerPrefsUserSkins = "PLAYERPREFSUSERSKIN";
}

[Serializable]
public class User_Skin
{
    [SerializeField, Tooltip("Skin ID")] private int _skinId;
    public int SkinId
    {
        get => _skinId;
        set => _skinId = value;
    }

    [SerializeField, Tooltip("Skin Name")] private string _skinName;
    public string SkinName
    {
        get => _skinName;
        set => _skinName = value;
    }

    // Buy true => Buy
    [SerializeField, Tooltip("Skin Buy")] private bool _skinBuy;
    public bool SkinBuy
    {
        get => _skinBuy;
        set => _skinBuy = value;
    }

    // UnLock true => UnLock
    [SerializeField, Tooltip("Skin UnLock")] private bool _unLockSkin;
    public bool UnLockSkin
    {
        get => _unLockSkin;
        set => _unLockSkin = value;
    }

    // Wear true => Wear
    [SerializeField, Tooltip("Skin Wear")] private bool _skinWear;
    public bool SkinWear
    {
        get => _skinWear;
        set => _skinWear = value;
    }

    [SerializeField, Tooltip("Skin Cost")] private int _skinCost;
    public int SkinCost
    {
        get => _skinCost;
        set => _skinCost = value;
    }
}
