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
    //public Action exitPointerEvent = null;

    [SerializeField]
    private Vector2 _startClickPos;
    [SerializeField]
    private Vector2 endPos; 

    public void OnPointerClick(PointerEventData eventData)
    {
      
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        endPos = eventData.position;
        Debug.Log("��" + endPos);
        if(endPos.y - _startClickPos.y > 500f)
        {
            Debug.Log("���� �巡��");
//            exitPointerEvent?.Invoke();
            endDragEvent?.Invoke();
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
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
