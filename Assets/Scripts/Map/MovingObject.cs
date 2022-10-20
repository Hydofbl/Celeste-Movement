using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : MonoBehaviour
{
    public float offset;
    public float moveTime;
    public Vector3 targetPosAddValue;
    private float opacity;
    private Collider2D _collider;
    private SpriteRenderer _spriteRenderer;
    private Coroutine _moveCoroutine;
    private bool _atTargetPos = false;
    private bool _isMoving = false;
    private Vector3 _startPos;
    private Vector3 _targetPos;
    void Start()
    {
        _collider = GetComponent<Collider2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _startPos = transform.position;
        _targetPos = _startPos + targetPosAddValue;
    }

    void Update()
    {

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (collision.gameObject.transform.position.y > transform.position.y + offset)
            {
                collision.transform.SetParent(transform);
               // Move();
            }
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (collision.gameObject.transform.position.y > transform.position.y + offset)
            {
                collision.transform.SetParent(transform);
                if (!_atTargetPos)
                {
                    transform.position = Vector3.Lerp(transform.position, _targetPos, moveTime / Vector2.Distance(transform.position, _targetPos) * Time.deltaTime);
                    if (Vector2.Distance(transform.position, _targetPos) < 0.5f)
                    {
                        _atTargetPos = true;

                    }
                }
                else
                {
                    transform.position = Vector3.Lerp(transform.position, _startPos, moveTime / Vector2.Distance(transform.position, _startPos) * Time.deltaTime);
                    if (Vector2.Distance(transform.position, _startPos) < 0.5f)
                    {
                        _atTargetPos = false;

                    }
                }
            }
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
                collision.transform.SetParent(null);
           /* StopMove();
            _isMoving = false;*/
        }
    }
    public void Restart()
    {
        transform.position = _startPos;
        _atTargetPos = false;
    }
}
