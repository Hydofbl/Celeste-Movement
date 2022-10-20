using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ScoresButton : MonoBehaviour, IPointerDownHandler
{
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
        PlayerPrefs.SetInt("fromMenu", 1);
        StartCoroutine(FindObjectOfType<DropDown>().OpenHighScoresMenu());
    }
}
