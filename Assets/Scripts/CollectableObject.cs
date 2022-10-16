using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableObject : MonoBehaviour
{
    public GameObject pogToMove;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            MovePog();
        }
    }
    private void MovePog()
    {
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<Collider2D>().enabled = false;
        GameObject pogGO = Instantiate(pogToMove, UIManager.Instance.pogImageMovePos.transform);
        pogGO.transform.position = Camera.main.WorldToScreenPoint(transform.position);
        pogGO.transform.DOLocalMove(Vector3.zero, 0.75f).OnComplete(() => {
            UIManager.Instance.IncreasePogScore(1);
            Destroy(pogGO);
            Destroy(gameObject);
        });
    }
}
