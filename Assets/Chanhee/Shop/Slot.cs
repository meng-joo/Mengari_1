using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using JetBrains.Annotations;

public class Slot : MonoBehaviour
{
    [SerializeField] private ItemData _itemData;

    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private TextMeshProUGUI _costText;
    [SerializeField] private Image _image;

    [SerializeField] private Button _useBtn;
    [SerializeField] private Button _buyBtn;

    [SerializeField] private CanvasGroup _lockImage;

    public ItemData ITEMDATA
    {
        get => _itemData;
        set => SetItemData(value);
    }

    public void ReplaceUI()
    {
        if(_itemData.itemBuy)
        {
            _buyBtn.gameObject.SetActive(false);
            _useBtn.gameObject.SetActive(true);
        }

        else
        {
            _useBtn.gameObject.SetActive(false);
            _buyBtn.gameObject.SetActive(true);
        }
    }

    public void SetItemData(ItemData itemData)
    {
        _itemData = itemData;

        _nameText.text = _itemData.itemName;
        _costText.text = _itemData.itemCost.ToString();
        _image.sprite = _itemData.itemSprite;

        _useBtn.onClick.AddListener(() => UseBtn());
        _buyBtn.onClick.AddListener(() => BuyBtn());
        ReplaceUI();
        if (_itemData.itemUnlock)
        {
            // 아이템이 잠김
            _lockImage.alpha = 0.3f;
            _lockImage.blocksRaycasts = true;
            return;
        }
        else
        {
            _lockImage.alpha = 0;
            _lockImage.blocksRaycasts = false;
        }


    }

    public void UseBtn()
    {
        if (_itemData.itemUnlock) return;
        if (!_itemData.itemBuy) return;
        if (_itemData.itemBuy)
        {
            ItemData item = SaveManager.Instance.ITEMDATALIST.usingItemData;
            item.itemUse = false;

            SaveManager.Instance.ITEMDATALIST.usingItemData = _itemData;
            _itemData.itemUse = true;
        }
        else
        {
            // 실패;
        }
    }

    public void BuyBtn()
    {
        if (_itemData.itemUnlock) return;
        if (_itemData.itemCost <= SaveManager.Instance.USERDATA.userMoney)
        {
            SaveManager.Instance.USERDATA.userMoney -= _itemData.itemCost;
            SaveManager.Instance.SaveUserData();
            _itemData.itemBuy = true;
            ReplaceUI();
        }
        else
        {
            // 실패;
        }
    }


}
