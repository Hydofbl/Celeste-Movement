using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ResetButton : MonoBehaviour, IPointerDownHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
        PlayerPrefs.DeleteAll();
        FindObjectOfType<MusicButton>().CheckActivity();
        FindObjectOfType<SFXButton>().CheckActivity();
    }
}
