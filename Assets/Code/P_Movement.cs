using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class P_Movement : MonoBehaviour
{
    #region Variables

    private P_Collision coll;
    [HideInInspector]
    public Rigidbody2D rb;

    [Space]
    [Header("Animation")]
    private P_ScriptAnimation anim;
    private string previousState;
    private string currentState;
    public Animator animator;
    private const string P_IDLE = "P_Idle";
    private const string P_RUN = "P_Run";
    private const string P_ATTACK = "P_Attack";
    private const string P_JUMP = "P_Jump";
    private const string P_SPRINT = "P_Sprint";
    private const string P_SLIDE = "P_Slide";
    private const string P_3 = "P_Jump";
    private const string P_4 = "P_Jump";
    private const string P_5 = "P_Jump";
    private const string P_6 = "P_Jump";
    private const string P_7 = "P_Jump";
    [Space]
    [Header("Stats")]
    public float speed = 10;
    public float sprintSpeed = 12;
    public float jumpForce = 50;
    public float slideSpeed = 5;
    public float wallJumpLerp = 10;
    public float dashSpeed = 20;

    [Space]
    [Header("Booleans")]
    public bool canMove;
    public bool wallGrab;
    public bool wallJumped;
    public bool wallSlide;
    public bool isDashing;
    private int groundMask;
    private bool canJump;
    private bool canDash;
    private bool canSprint;
    private bool canSlide;
    private bool groundTouch;
    private bool hasDashed;
    private bool isGrounded;
    [Space]
    [Header("Wall Side")]
    public int side = 1;

    [Space]
    [Header("World Settings")]
    public float gravityScale = 3f;
    [Space]
    [Header("FX")]
    public ParticleSystem dashFX;
    public ParticleSystem wallSlideFX;
    public ParticleSystem landingFX;

    #endregion Variables

    private void Start()
    {
        animator = GetComponent<Animator>();
        coll = GetComponent<P_Collision>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<P_ScriptAnimation>();
    }

    void Update()
    {
        GetInputs();
    }

    private void GetInputs()
    {
        bool isMoving = Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow);

        if (Input.GetButton("Sprint"))
        {
            canSprint = true;
            if (Input.GetButton("Crouch") && isMoving)
            {
                canSlide = true;
            }
            else
            {
                canSlide = false;
            }
        }
        else
        {
            canSprint = false;
        }

        if (Input.GetButton("Dash"))
        {
            canDash = true;
        }
        else
        {
            canDash = false;
        }


        if (Input.GetButton("Jump"))
        {
            canJump = true;
        }
        else
        {
            canJump = false;
        }
    }

    void FixedUpdate()
    {
        // UPDATE VARIABLES //
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        float xRaw = Input.GetAxisRaw("Horizontal");
        float yRaw = Input.GetAxisRaw("Vertical");
        Vector2 dir = new Vector2(x, y);

        if (canSprint)
        {
            Sprint(dir);
        }
        else
        {

            Walk(dir);
        }
        anim.SetHorizontalMovement(x, y, rb.velocity.y);


        if (coll.onGround)
        {
            if (xRaw != 0)
            {

                if (canSprint)
                {
                    //Sprint(dir);

                    float temp = rb.velocity.x;
                    if (temp < 0)
                    {
                        temp = temp * -1;
                    }
                    Debug.Log(temp);
                    if (canSlide && temp > 9)
                    {
                        ChangeAnimState(P_SLIDE);

                    }
                    else
                    {
                        ChangeAnimState(P_SPRINT);

                    }
                }
                else
                {
                    ChangeAnimState(P_RUN);
                }
            }
            else
            {
                ChangeAnimState(P_IDLE);
            }
        }


        // START WALL GRAB / SLIDE //
        if (coll.onWall && Input.GetButton("Fire3") && canMove)
        {
            if (side != coll.wallSide)
            {
                anim.Flip(side * -1);
            }
            wallGrab = true;
            wallSlide = false;
        }

        // END WALL GRAB / SLIDE //
        if (Input.GetButtonUp("Fire3") || !coll.onWall || !canMove)
        {
            wallGrab = false;
            wallSlide = false;
        }

        // END WALL JUMP //
        if (coll.onGround && !isDashing)
        {
            wallJumped = false;
            GetComponent<P_Jumping>().enabled = true;
        }

        // SET GRAVITY SCALE FOR WALL WALL CLIMB //
        if (wallGrab && !isDashing)
        {
            rb.gravityScale = 0;
            if (x > .2f || x < -.2f)
                rb.velocity = new Vector2(rb.velocity.x, 0);

            float speedModifier = y > 0 ? .5f : 1;

            rb.velocity = new Vector2(rb.velocity.x, y * (speed * speedModifier));

            anim.Flip(side);
        }
        else
        {
            rb.gravityScale = 3;
        }

        // START WALL SLIDE //
        if (coll.onWall && !coll.onGround)
        {
            if (x != 0 && !wallGrab)
            {
                wallSlide = true;
                WallSlide();
            }
        }

        // END WALL SLIDE //
        if (!coll.onWall || coll.onGround)
        {
            wallSlide = false;
        }

        // JUMP //

        if (canJump)
        {
            ChangeAnimState(P_JUMP);
            if (coll.onGround)
            {
                Jump(Vector2.up, false);
            }
            if (coll.onWall && !coll.onGround)
            {
                WallJump();
            }
        }

        // DASH //
        if (canDash && !hasDashed)
        {
            if (xRaw != 0 || yRaw != 0)
            {
                CreateDashFX();
                Dash(xRaw, yRaw);
            }
        }

        // SET GROUND TOUCH //
        if (coll.onGround && !groundTouch)
        {
            GroundTouch();
            groundTouch = true;
        }

        // RESET TOUCH GROUND //
        if (!coll.onGround && groundTouch)
        {
            groundTouch = false;
        }

        // HANDLE ERRORS //
        if (wallGrab || wallSlide || !canMove)
        {
            return;
        }

        //if (!wallGrab || !wallSlide ||  !canMove)
        //{
        //    return;
        //}

        // Handle Sides
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


    private void Sprint(Vector2 dir)
    {
        if (!canMove)
        {

            return;
        }

        if (wallGrab)
        {
            return;
        }

        if (!wallJumped)
        {
            rb.velocity = new Vector2(dir.x * sprintSpeed, rb.velocity.y);

        }
        else
        {
            rb.velocity = Vector2.Lerp(rb.velocity, (new Vector2(dir.x * sprintSpeed, rb.velocity.y)), wallJumpLerp * Time.deltaTime);
        }
    }

    private void ChangeAnimState(string newState)
    {
        if (currentState == newState)
        {
            return;
        }
        animator.Play(newState);

        currentState = newState;
    }

    private void GroundTouch()
    {
        hasDashed = false;
        isDashing = false;
    }

    private void WallSlide()
    {
        if (coll.wallSide != side)
        {
            anim.Flip(side * -1);
        }
        if (!canMove)
        {
            return;
        }

        bool pushingWall = false;
        if ((rb.velocity.x > 0 && coll.onRightWall) || (rb.velocity.x < 0 && coll.onLeftWall))
        {
            pushingWall = true;
        }
        float push = pushingWall ? 0 : rb.velocity.x;

        rb.velocity = new Vector2(push, -slideSpeed);
    }

    private void WallJump()
    {
        if ((side == 1 && coll.onRightWall) || side == -1 && !coll.onRightWall)
        {
            side *= -1;
            anim.Flip(side);
        }

        StopCoroutine(DisableMovement(0));
        StartCoroutine(DisableMovement(.05f));

        Vector2 wallDir = coll.onRightWall ? Vector2.left : Vector2.right;

        Jump((Vector2.up / 1.5f + wallDir / 1.5f), true);

        wallJumped = true;
    }

    private void Walk(Vector2 dir)
    {
        if (!canMove)
        {

            return;
        }

        if (Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.RightArrow))
        {
            return;
        }


        if (wallGrab)
        {
            return;
        }

        if (!wallJumped)
        {
            rb.velocity = new Vector2(dir.x * speed, rb.velocity.y);

        }
        else
        {
            rb.velocity = Vector2.Lerp(rb.velocity, (new Vector2(dir.x * speed, rb.velocity.y)), wallJumpLerp * Time.deltaTime);
        }
    }

    private void Jump(Vector2 dir, bool wall)
    {
        rb.velocity = new Vector2(rb.velocity.x, 0);
        rb.velocity += dir * jumpForce;
    }

    private void Dash(float x, float y)
    {
        hasDashed = true;

        rb.velocity = Vector2.zero;
        Vector2 dir = new Vector2(x, y);

        rb.velocity += dir.normalized * dashSpeed;

        StartCoroutine(DashWait());
    }

    private IEnumerator DashWait()
    {
        StartCoroutine(GroundDash());
        DOVirtual.Float(14, 0, .8f, RigidbodyDrag);

        rb.gravityScale = 0;
        GetComponent<P_Jumping>().enabled = false;
        wallJumped = true;
        isDashing = true;

        yield return new WaitForSeconds(.3f);

        rb.gravityScale = 3;
        GetComponent<P_Jumping>().enabled = true;
        wallJumped = false;
        isDashing = false;
    }

    private IEnumerator GroundDash()
    {
        yield return new WaitForSeconds(.15f);
        if (coll.onGround)
            hasDashed = false;
    }

    private IEnumerator DisableMovement(float time)
    {
        canMove = false;
        yield return new WaitForSeconds(time);
        canMove = true;
    }

    private void RigidbodyDrag(float x)
    {
        rb.drag = x;
    }

    private void CreateDashFX()
    {
        dashFX.Play();
    }
}