using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Effect : MonoBehaviour
{
    public Transform TargetPos;
    public Sequence sequence = DOTween.Sequence();
    private void Start()
    {
        sequence.Append(transform.DOMove(TargetPos.position, 1f).SetEase(Ease.OutCirc));
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {

        }
    }
}