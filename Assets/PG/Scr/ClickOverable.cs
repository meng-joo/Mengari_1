using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ClickOverable : MonoBehaviour, IPointerEnterHandler
{
    public UnityEvent _clickoverEvent; 
    public void OnPointerEnter(PointerEventData eventData)
    {
        _clickoverEvent.Invoke(); 
    }

    
}
