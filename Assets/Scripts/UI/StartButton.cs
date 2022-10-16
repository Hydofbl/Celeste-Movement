using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class StartButton : MonoBehaviour, IPointerDownHandler
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
            StartCoroutine(OpenGame());
        }
    }
    public IEnumerator OpenGame()
    {
        yield return new WaitForSeconds(0);
        FindObjectOfType<DropDown>().GetNextScene();
    }
}
