using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour, IPointerDownHandler, IPointerUpHandler//, IPointerExitHandler
{
    private Vector2 _dragDir = Vector2.up;  // �巡���� ���� 
    public Action beginDragEvent = null;
    public Action endDragEvent = null;
    public Action stayClickEvent; 
    //public Action exitPointerEvent = null;

    [SerializeField]
    private Vector2 _startClickPos;
    [SerializeField]
    private Vector2 endPos;

    [SerializeField]
    private bool _isClick = false; // Ŭ�� ���ΰ� 

    private void Update()
    {
        if(_isClick)
        {
            Debug.Log("Ŭ����"); 
            stayClickEvent?.Invoke(); 
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
      
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _isClick = false; 
        endPos = eventData.position;
        Debug.Log("��" + endPos);
        if(endPos.y - _startClickPos.y > 100f)
        {
            Debug.Log("���� �巡��");
//            exitPointerEvent?.Invoke();
            endDragEvent?.Invoke();
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _isClick = true; 

        Debug.Log("Ŭ��");
        _startClickPos = eventData.position;
        Debug.Log("���� Ŭ��" + _startClickPos);
         beginDragEvent?.Invoke();
    }


    //    public void OnPointerExit(PointerEventData eventData)
    //  {
    //     exitPointerEvent?.Invoke(); 
    // }
}
