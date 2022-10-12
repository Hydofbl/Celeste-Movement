﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClimbButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private Movement _movementCs;
    // Start is called before the first frame update
    void Start()
    {
        _movementCs = FindObjectOfType<Movement>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void OnPointerDown(PointerEventData eventData)
    {
        _movementCs.climbButton = true;
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        _movementCs.climbButton = false;
    }
}
