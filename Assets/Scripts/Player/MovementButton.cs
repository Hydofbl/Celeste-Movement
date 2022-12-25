using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MovementButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public bool isClicked;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!isClicked)
        {
            isClicked = true;
            
        }
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        if (isClicked)
        {
            isClicked = false;
        }
    }
}
