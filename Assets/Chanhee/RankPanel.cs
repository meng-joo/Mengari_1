using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RankPanel : MonoBehaviour
{
    TextMeshProUGUI _userNameText = null;
    TextMeshProUGUI _userStageText = null;

    private void Awake()
    {
        _userNameText = transform.Find("UserNameText").GetComponent<TextMeshProUGUI>();
        _userStageText = transform.Find("UserStageText").GetComponent<TextMeshProUGUI>();
    }

    public void Init(User user)
    {
        _userNameText.text = user.userName;
        _userStageText.text = $"{user.frontStageNumber}-{user.backStageNumber}";
    }
}
