using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    [SerializeField] private GameObject _slot;

    private ItemDataList _itemDataList;
    public ItemDataList ITEMDATALIST
    {
        get => _itemDataList;
        set => _itemDataList = value;
    }

    [SerializeField] private Transform _content;

    [SerializeField] private Dictionary<ItemData, GameObject> _itemDataDictionary;
    public Dictionary<ItemData, GameObject> ITEMDATADICTIONARY
    {
        get => _itemDataDictionary;
        set => _itemDataDictionary = value;
    }

    private void Start()
    {
        _itemDataList = SaveManager.Instance.ITEMDATALIST;

        _itemDataDictionary = new Dictionary<ItemData, GameObject>();
        for (int i = 0; i < _itemDataList.itemDataLists.Count; i++)
        {
            GameObject newSlot = Instantiate(_slot, _content);
            ItemData itemData = _itemDataList.itemDataLists[i];

            _itemDataDictionary.Add(itemData, newSlot);
            newSlot.GetComponent<Slot>().ITEMDATA = itemData;
        }
    }
}
