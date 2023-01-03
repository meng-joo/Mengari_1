using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class RankingManager : MonoBehaviour
{
    private List<GameObject> _rankPanelList = new List<GameObject>();
    [SerializeField] private Transform _content = null;
    [SerializeField] private GameObject _rankPanel = null;
    [SerializeField] private Button _goBackBtn = null;

    private Sequence _seq;
    private List<User> _userDataList = new List<User>();

    public void Awake()
    {
        _goBackBtn.onClick.AddListener(GoBack);
    }

    public void GoBack()
    {
        _seq = DOTween.Sequence();
        _seq.Append(_rankPanel.gameObject.transform.DOMoveX(720f*3, 0.2f));
        _seq.AppendCallback(() =>
        {
            _seq.Kill();
        });
    }
    public void RefreshRankPanel()
    {
        _seq = DOTween.Sequence();

        _seq.Append(_rankPanel.gameObject.transform.DOMoveX(720f,0.2f));
        if (_rankPanelList.Count != 0)
        {
            for (int i = 0; i < _rankPanelList.Count; i++)
            {
                Destroy(_rankPanelList[i]);
            }
            _rankPanelList.Clear();
        }
        _seq.AppendCallback(() =>
        {
            _seq.Kill();
        });
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
            Debug.Log(_userDataList[i]);
            newRankPanel.GetComponent<RankPanel>().Init(_userDataList[i]);
            _rankPanelList.Add(newRankPanel);
        }
    }
}
