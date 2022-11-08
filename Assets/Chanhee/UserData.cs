using System;
using UnityEngine;

[CreateAssetMenu(fileName = "UserData", menuName = "SO/User/UserData")]
public class UserData : ScriptableObject
{
    public int userId = 0;
    public string deviceId = string.Empty;
    public string userName = string.Empty;
    public int userMoney = 0;
    public int frontStageNumber = 1;
    public int backStageNumber = 1;

    public void SetUserData(User user)
    {
        this.userId = user.userId;
        this.deviceId = user.deviceId;
        this.userName = user.userName;
        this.userMoney = user.userMoney;
        this.frontStageNumber = user.frontStageNumber;
        this.backStageNumber = user.backStageNumber;
    }

    public void ResetUserData()
    {
        this.userId = 0;
        this.deviceId = string.Empty;
        this.userName = string.Empty;
        this.userMoney = 0;
        this.frontStageNumber = 1;
        this.backStageNumber = 1;
    }
}

[Serializable]
public class User
{
    public int userId;
    public string deviceId;
    public string userName;
    public int userMoney;
    public int frontStageNumber;
    public int backStageNumber;
}