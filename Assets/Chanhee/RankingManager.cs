using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RankingManager : MonoBehaviour
{
    private List<GameObject> _rankPanelList = new List<GameObject>();
    [SerializeField] private Transform _content = null;
    [SerializeField] private GameObject _rankPanel = null;

    private List<User> _userDataList = new List<User>();
    public void RefreshRankPanel()
    {
        if (_rankPanelList.Count != 0)
        {
            for (int i = 0; i < _rankPanelList.Count; i++)
            {
                Destroy(_rankPanelList[i]);
            }
            _rankPanelList.Clear();
        }

        StartCoroutine(LoadList());
    }

    IEnumerator LoadList()
    {
        SaveManager.Instance.RefreshUser();

        yield return new WaitUntil(() => !SaveManager.Instance.ISLOADING);

        _userDataList = SaveManager.Instance.USERDATALIST;

        for (int i = 0; i < _userDataList.Count; i++)
        {
            GameObject newRankPanel = Instantiate(_rankPanel, _content);
            newRankPanel.GetComponent<RankPanel>().Init(_userDataList[i]);
            _rankPanelList.Add(newRankPanel);
        }
    }
}
