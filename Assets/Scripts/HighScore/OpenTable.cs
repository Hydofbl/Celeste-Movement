using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class OpenTable : MonoBehaviour, IPointerDownHandler
{

    public GameObject submitField, tableField;
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
        submitField.SetActive(false);
        tableField.SetActive(true);
    }
}
