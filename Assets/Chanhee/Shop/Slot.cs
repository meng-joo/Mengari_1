using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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

    void SetItemData(ItemData itemData)
    {
        _itemData = itemData;

        _nameText.text = _itemData.itemName;
        _costText.text = _itemData.itemCost.ToString();
        _image.sprite = _itemData.itemSprite;

        _useBtn.onClick.AddListener(() => UseBtn());
        _buyBtn.onClick.AddListener(() => BuyBtn());

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
        if (_itemData.itemBuy)
        {
            // 교체;
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
            // buy;
        }
        else
        {
            // 실패;
        }
    }


}
