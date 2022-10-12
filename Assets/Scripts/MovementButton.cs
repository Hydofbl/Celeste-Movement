using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MovementButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public bool isClicked;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
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
