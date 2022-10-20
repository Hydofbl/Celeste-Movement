﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ResumeButton : MonoBehaviour, IPointerDownHandler
{
    public GameObject pauseMenu;
    

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
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }
}
