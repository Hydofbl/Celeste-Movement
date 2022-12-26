using System.Collections;
using UnityEngine;
using DG.Tweening;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    private static Movement _instance;
    private Collision coll;
    [HideInInspector]
    public Rigidbody2D rb;
    [HideInInspector]
    public static Movement Instance => _instance;
    private AnimationScript anim;
    private DialogInteraction dialogInteraction;
    private SoundManagerScript sound;
    private Vector3 _startPos;

    [Space]
    [Header("Stats")]
    public float speed = 10;
    public float jumpForce = 50;
    public float slideSpeed = 5;
    public float wallJumpLerp = 10;
    public float dashSpeed = 20;

    private float x, y,  xRaw, yRaw;

    [Space]
    [Header("Booleans")]
    public bool canMove;
    public bool wallGrab;
    public bool wallJumped;
    public bool wallSlide;
    public bool isDashing;

    [Space]

    private bool groundTouch;
    private bool hasDashed;

    public int side = 1;

    [Space]
    [Header("Polish")]
    public ParticleSystem dashParticle;
    public ParticleSystem jumpParticle;
    public ParticleSystem wallJumpParticle;
    public ParticleSystem slideParticle;
    public Animator deathAnimation;
    public DynamicJoystick dynamicJoystick;

    [Space]
    [Header("Buttons")]
    public bool jumpPressed;
    public bool dashPressed;
    public bool isHanging;
    public string deviceName;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this);
        }
        else
        {
            _instance = this;
        }
    }

    void Start()
    {
        Application.targetFrameRate = 60;
        coll = GetComponent<Collision>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<AnimationScript>();
        dialogInteraction = GetComponent<DialogInteraction>();
        sound = GetComponentInChildren<SoundManagerScript>();
        _startPos = transform.position;
    }

    void Update()
    {
        if (dialogInteraction.DialogUI.IsOpen) return;

        Vector2 dir = new Vector2(x, y);
        CheckDeath();
        Walk(dir);
        Climb(dir);
        anim.SetHorizontalMovement(x, y, rb.velocity.y);

        if (coll.onWall && isHanging && canMove)
        {
            if (side != coll.wallSide)
                anim.Flip(side * -1);
            wallGrab = true;
            wallSlide = false;
        }

        if (!isHanging || !coll.onWall || !canMove)
        {
            wallGrab = false;
            wallSlide = false;
        }

        if (jumpPressed)
        {
            anim.SetTrigger("jump");

            if (coll.onGround)
                Jump(Vector2.up, false);
            if (coll.onWall && !coll.onGround)
                WallJump();
        }

        if (dashPressed && !hasDashed)
        {
            if (xRaw != 0 || yRaw != 0)
                Dash(xRaw, yRaw);
        }

        if (coll.onGround && !isDashing)
        {
            wallJumped = false;
            GetComponent<BetterJumping>().enabled = true;
        }

        if (wallGrab && !isDashing)
        {
            rb.gravityScale = 0;
            if (x > .2f || x < -.2f)
                rb.velocity = new Vector2(rb.velocity.x, 0);

            float speedModifier = y > 0 ? .5f : 1;

            rb.velocity = new Vector2(rb.velocity.x, y * (speed * speedModifier));
        }
        else
        {
            rb.gravityScale = 3;
        }

        if (coll.onWall && !coll.onGround)
        {
            if (x != 0 && !wallGrab)
            {
                wallSlide = true;
                WallSlide();
            }
        }

        if (!coll.onWall || coll.onGround)
            wallSlide = false;

        if (coll.onGround && !groundTouch)
        {
            GroundTouch();
            groundTouch = true;
        }

        if (!coll.onGround && groundTouch)
        {
            groundTouch = false;
        }

        WallParticle(y);

        if (wallGrab || wallSlide || !canMove)
            return;

        if (x > 0)
        {
            side = 1;
            anim.Flip(side);
        }
        if (x < 0)
        {
            side = -1;
            anim.Flip(side);
        }
    }

    void GroundTouch()
    {
        hasDashed = false;
        isDashing = false;

        side = anim.sr.flipX ? -1 : 1;

        jumpParticle.Play();
        
        if(PlayerPrefs.GetInt("sfx") == 1)
            sound.PlaySound("land");
    }

    private void Dash(float x, float y)
    {
        if (!canMove)
            return;
        Camera.main.transform.DOComplete();
        Camera.main.transform.DOShakePosition(.2f, .5f, 14, 90, false, true);
        FindObjectOfType<RippleEffect>().Emit(Camera.main.WorldToViewportPoint(transform.position));

        hasDashed = true;

        anim.SetTrigger("dash");

        rb.velocity = Vector2.zero;
        Vector2 dir = new Vector2(x, y);

        rb.velocity += dir.normalized * dashSpeed;
        StartCoroutine(DashWait());
    }

    IEnumerator DashWait()
    {
        FindObjectOfType<GhostTrail>().ShowGhost();
        StartCoroutine(GroundDash());
        DOVirtual.Float(14, 0, .8f, RigidbodyDrag);

        dashParticle.Play();
        if(PlayerPrefs.GetInt("sfx") == 1)
            sound.PlaySound("dash");
        rb.gravityScale = 0;
        GetComponent<BetterJumping>().enabled = false;
        wallJumped = true;
        isDashing = true;

        yield return new WaitForSeconds(.3f);

        dashParticle.Stop();
        rb.gravityScale = 3;
        GetComponent<BetterJumping>().enabled = true;
        wallJumped = false;
        isDashing = false;
    }

    IEnumerator GroundDash()
    {
        yield return new WaitForSeconds(.15f);
        if (coll.onGround)
            hasDashed = false;
    }

    private void WallJump()
    {
        if ((side == 1 && coll.onRightWall) || side == -1 && !coll.onRightWall)
        {
            side *= -1;
            anim.Flip(side);
        }

        StopCoroutine(DisableMovement(0));
        StartCoroutine(DisableMovement(.1f));

        Vector2 wallDir = coll.onRightWall ? Vector2.left : Vector2.right;

        Jump((Vector2.up / 1.5f + wallDir / 1.5f), true);

        wallJumped = true;
    }

    private void WallSlide()
    {
        if (coll.wallSide != side)
            anim.Flip(side * -1);

        if (!canMove)
            return;

        bool pushingWall = false;
        if ((rb.velocity.x > 0 && coll.onRightWall) || (rb.velocity.x < 0 && coll.onLeftWall))
        {
            pushingWall = true;
        }
        float push = pushingWall ? 0 : rb.velocity.x;

        rb.velocity = new Vector2(push, -slideSpeed);
        
        if(PlayerPrefs.GetInt("sfx") == 1)
            sound.PlaySound("slide");
    }

    private void Walk(Vector2 dir)
    {
        if (!canMove)
            return;

        if (wallGrab)
            return;

        if (!wallJumped)
        {
            rb.velocity = new Vector2(dir.x * speed, rb.velocity.y);
        }
        else
        {
            rb.velocity = Vector2.Lerp(rb.velocity, (new Vector2(dir.x * speed, rb.velocity.y)), wallJumpLerp * Time.deltaTime);
        }
        
        if(PlayerPrefs.GetInt("sfx") == 1)
            sound.PlaySound("walk");
    }
    
    private void Climb(Vector2 dir)
    {
        if (!canMove)
            return;

        if (wallGrab)
            return;

        if (coll.onIvy && dir.y != 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, dir.y * speed);
        }
        /* else
         {
             rb.velocity = Vector2.Lerp(rb.velocity, (new Vector2(dir.x * speed, rb.velocity.y)), wallJumpLerp * Time.deltaTime);
         }*/
    }
    
    private void CheckDeath()
    {
        if (coll.onDeath && canMove)
        {
            canMove = false;
            deathAnimation.gameObject.SetActive(true);
            deathAnimation.enabled = true;
            deathAnimation.GetComponent<SpriteRenderer>().enabled = true;
            deathAnimation.Play(0);
            deathAnimation.transform.SetParent(null);
            gameObject.SetActive(false);
        }
    }
    
    private void Jump(Vector2 dir, bool wall)
    {
        if (!canMove)
            return;
        slideParticle.transform.parent.localScale = new Vector3(ParticleSide(), 1, 1);
        ParticleSystem particle = wall ? wallJumpParticle : jumpParticle;

        rb.velocity = new Vector2(rb.velocity.x, 0);
        rb.velocity += dir * jumpForce;

        particle.Play();
        
        if(PlayerPrefs.GetInt("sfx") == 1)
            sound.PlaySound("jump");
    }

    IEnumerator DisableMovement(float time)
    {
        canMove = false;
        yield return new WaitForSeconds(time);
        canMove = true;
    }

    void RigidbodyDrag(float x)
    {
        rb.drag = x;
    }

    void WallParticle(float vertical)
    {
        var main = slideParticle.main;

        if (wallSlide || (wallGrab && vertical < 0))
        {
            slideParticle.transform.parent.localScale = new Vector3(ParticleSide(), 1, 1);
            main.startColor = Color.white;
        }
        else
        {
            main.startColor = Color.clear;
        }
    }

    int ParticleSide()
    {
        int particleSide = coll.onRightWall ? 1 : -1;
        return particleSide;
    }
    
    public void Restart()
    {
        gameObject.SetActive(true);
        transform.position = _startPos;
        coll.onDeath = false;
        coll.onGround = false;
        coll.onIvy = false;
        coll.onLeftWall = false;
        coll.onRightWall = false;
        coll.onWall = false;
        canMove = true;
    }

    #region Events
    public void MoveEvent(InputAction.CallbackContext context)
    {
        Vector2 dir = context.ReadValue<Vector2>();
        Vector2 dirNorm = dir.normalized;

        x = dir.x;
        y = dir.y;

        xRaw = dirNorm.x;
        yRaw = dirNorm.y;

        deviceName = context.control.device.displayName;
    }

    public void JumpEvent(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            jumpPressed = true;
        }

        if (context.canceled)
        {
            jumpPressed = false;
        }
    }

    public void DashEvent(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            dashPressed = true;
        }

        if (context.canceled)
        {
            dashPressed = false;
        }
    }

    public void HangEvent(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            isHanging = true;
        }

        if (context.canceled)
        {
            isHanging = false;
        }
    }
    #endregion
}
