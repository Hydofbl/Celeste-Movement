using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DisappearingTilemap : MonoBehaviour
{
    public float disappearMultiplier;
    public float offset;
    public float opacity;
    private Coroutine _disappearCoroutine,_appearCoroutine;
    private Collider2D _collider;
    private SpriteRenderer _spriteRenderer;
    private Color _spriteRendererColor;
    // Start is called before the first frame update
    void Start()
    {
        _collider=GetComponent<Collider2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRendererColor = _spriteRenderer.color;
        opacity = _spriteRendererColor.a;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (collision.gameObject.transform.position.y > transform.position.y + offset)
            {
                if(_disappearCoroutine!=null)
                {
                    StopCoroutine(_disappearCoroutine);
                }
                if (_appearCoroutine != null)
                {
                    StopCoroutine(_appearCoroutine);
                }
                _disappearCoroutine = StartCoroutine(Disappear());
            }
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (_disappearCoroutine != null)
            {
                StopCoroutine(_disappearCoroutine);
            }
            if (_appearCoroutine != null)
            {
                StopCoroutine(_appearCoroutine);
            }
            _appearCoroutine = StartCoroutine(Appear());
        }
    }
    private IEnumerator Disappear()
    {
        if (opacity > 0)
        {
            opacity -= Time.deltaTime * disappearMultiplier;
            AssignSpriteColor();
            yield return new WaitForFixedUpdate();
            _disappearCoroutine = StartCoroutine(Disappear());
        }
        else
        {
            _collider.enabled = false;
            opacity = 0;
            yield return new WaitForSeconds(3);
            opacity = 1;
            AssignSpriteColor();
            _collider.enabled = true;
        }
    }
    private IEnumerator Appear()
    {
        if (opacity>0 && opacity < 1)
        {
            opacity += Time.deltaTime * disappearMultiplier;
            AssignSpriteColor();
            yield return new WaitForFixedUpdate();
            _appearCoroutine = StartCoroutine(Appear());
        }
    }
    public void AssignSpriteColor()
    {
        _spriteRenderer.color = new Color(_spriteRendererColor.r, _spriteRendererColor.g, _spriteRendererColor.b, opacity);
        print(_spriteRendererColor.r);
    }
}
