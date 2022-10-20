using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestartLevel : MonoBehaviour
{
    public Movement movement;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Restart()
    {
        StartCoroutine(RestartWithTransition());

    }
    public IEnumerator RestartWithTransition()
    {
        UIManager.Instance.StartTransition();
        yield return new WaitForSeconds(0.5f);
        movement.Restart();
        transform.parent = movement.transform;
        transform.localPosition = Vector3.zero;
        foreach(MovingObject movingObject in FindObjectsOfType<MovingObject>())
        {
            movingObject.Restart();
        }
        gameObject.SetActive(false);
        UIManager.Instance.EndTransition();
        yield return new WaitForSeconds(0.5f);
    }
}
