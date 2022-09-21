using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour, IPointerClickHandler, IPointerUpHandler, IPointerExitHandler
{
    private Vector2 _dragDir = Vector2.up;  // 드래그할 방향 
    public Action beginDragEvent = null;
    public Action endDragEvent = null;
    public Action exitPointerEvent = null;

    [SerializeField]
    private Vector2 _startClickPos;
    [SerializeField]
    private Vector2 endPos; 

    public void OnPointerClick(PointerEventData eventData)
    {
        _startClickPos = eventData.position;
        beginDragEvent?.Invoke(); 
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        endPos = eventData.position; 
        if(endPos.y - _startClickPos.y > 500f)
        {
            Debug.Log("위로 드래그");
        }
        endDragEvent?.Invoke(); 
    }


    public void OnPointerExit(PointerEventData eventData)
    {
        exitPointerEvent?.Invoke(); 
     }
}
