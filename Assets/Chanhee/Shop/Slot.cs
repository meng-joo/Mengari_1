using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using JetBrains.Annotations;
using Unity.VisualScripting;

public class Slot : MonoBehaviour
{
    [SerializeField] private ItemData _itemData;

    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private TextMeshProUGUI _costText;
    [SerializeField] public Image _image;

    [SerializeField] public Button _useBtn;
    [SerializeField] private Button _buyBtn;

    [SerializeField] private CanvasGroup _lockImage;

    private ShopManager _shop;

    public ItemData ITEMDATA
    {
        get => _itemData;
        set => SetItemData(value);
    }
    public void Awake()
    {
        _shop = FindObjectOfType<ShopManager>();
    }


    /// <summary>
    /// 갱신
    /// </summary>
    public void UnLockItem()
    {
        _itemData.itemUnlock = false;
        SetItemData();
    }

    public void Refresh()
    {
        for (int i = 0; i < _shop.ITEMDATADICTIONARY.Count; i++)
        {
            if (_shop.ITEMDATALIST.itemDataLists[i].itemUnlock)
                _shop.ITEMDATADICTIONARY[_shop.ITEMDATALIST.itemDataLists[i]].GetComponent<Slot>().UnLockItem();

            if (_shop.ITEMDATALIST.itemDataLists[i].itemUse)
            {
                _shop.ITEMDATADICTIONARY[_shop.ITEMDATALIST.itemDataLists[i]].GetComponent<Slot>()._image.color = Color.green;
                _shop.ITEMDATADICTIONARY[_shop.ITEMDATALIST.itemDataLists[i]].GetComponent<Slot>()._useBtn.gameObject.SetActive(false);
            }
            else
            {
                _shop.ITEMDATADICTIONARY[_shop.ITEMDATALIST.itemDataLists[i]].GetComponent<Slot>()._image.color = Color.red;
                _shop.ITEMDATADICTIONARY[_shop.ITEMDATALIST.itemDataLists[i]].GetComponent<Slot>()._useBtn.gameObject.SetActive(true);
            }
        }
    }

    public void ReplaceUI()
    {
        if (!_itemData.itemBuy)
        {
            Debug.Log("아이템 산 ui");
            _useBtn.gameObject.SetActive(false);
            _buyBtn.gameObject.SetActive(true);
        }

        else
        {
            Debug.Log("아이템 사용 ui");
            _buyBtn.gameObject.SetActive(false);
            _useBtn.gameObject.SetActive(true);
            Image img = _useBtn.gameObject.GetComponent<Image>();
            img.color = _itemData.itemUse == true ? Color.green : Color.white;
        }

    }

    public void SetItemData()
    {

        _nameText.text = _itemData.itemName;
        _costText.text = _itemData.itemCost.ToString();
        _image.sprite = _itemData.itemSprite;

        _useBtn.onClick.AddListener(() => UseBtn());
        _buyBtn.onClick.AddListener(() => BuyBtn());
        if (_itemData.itemUnlock)
        {
            _lockImage.enabled = false;
            _lockImage.blocksRaycasts = true;
            ReplaceUI();
            return;
        }
        else
        {
            _lockImage.alpha = 0;
            _lockImage.blocksRaycasts = false;
        }
        Refresh();
    }

    public void SetItemData(ItemData itemData)
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
            ReplaceUI();
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
        if (_itemData.itemBuy)
        {
            Debug.Log("A");
            ItemData item = SaveManager.Instance.ITEMDATALIST.usingItemData;
            if (item != null)
            {
                Debug.Log(item);
                item.itemUse = false;
                // Use가 취소되는거잖아
                
                item = null;
            }
            Debug.Log("B");
            SaveManager.Instance.ITEMDATALIST.usingItemData = _itemData;
            _itemData.itemUse = true;
            Debug.Log("C");
            ReplaceUI();
            Debug.Log("D");
            SetItemData();
            Debug.Log("E");
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
