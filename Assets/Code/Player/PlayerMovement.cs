using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Space]
    [Header("Animation")]


    private string previousState;
    private string currentState;
    public Animator animator;
    private const string P_IDLE = "P_Idle";
    private const string P_RUN = "P_Run";

    float xInput = 0, yInput = 0, speed = 5;
    bool mouseLeft, canShoot;
    Vector3 mousePos, mouseVector;
    public Transform gunSprite, gunTip;
    public SpriteRenderer gunRend;
    public GameObject bulletPrefab;
    private Vector2 playerInput;
    private Rigidbody2D rb;
    private int side = 1;
    private Animator anim;
    public SpriteRenderer sr;



    public float moveSpeed = 5;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        GetInput();
        GetMouseInput();
        Movement();
    }

    void GetInput()
    {
        xInput = Input.GetAxis("Horizontal");
        yInput = Input.GetAxis("Vertical"); //capture wasd and arrow controls

        GetMouseInput();
    }
    void GetMouseInput()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition); //position of cursor in world
        mousePos.z = transform.position.z; //keep the z position consistant, since we're in 2d
        mouseVector = (mousePos - transform.position).normalized; //normalized vector from player pointing to cursor
        mouseLeft = Input.GetMouseButton(0); //check left mouse button
        if (mouseVector.x > 0)
        {
            side = 1;
            Flip(side);
        }
        if (mouseVector.x < 0)
        {
            side = -1;
            Flip(side);
        }
    }
    void Movement()
    {
        bool isMoving = Input.GetButton("Horizontal") || Input.GetButton("Vertical");
        Vector3 tempPos = transform.position;
        tempPos += new Vector3(xInput, yInput, 0) * speed * Time.deltaTime; //move the player based on inpupt captures
        transform.position = tempPos;
        if (isMoving)
        {
            ChangeAnimState(P_RUN);
        }
        else
        {
            ChangeAnimState(P_IDLE);
        }
        //FindObjectOfType<AudioManager>().Play("Footstep");
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

    private void Flip(int side)
    {

        bool state = (side == 1) ? false : true;
        sr.flipX = state;
    }
    void ChooseFootstep()
    {
        float RandomValue = UnityEngine.Random.value;
        if (RandomValue <= 0.2)
        {
            FindObjectOfType<AudioManager>().Play("Footstep");
        }
        else if (RandomValue >= 0.2 && RandomValue < 0.4)
        {
            FindObjectOfType<AudioManager>().Play("BulletHit2");
        }
        else if (RandomValue >= 0.4 && RandomValue < 0.8)
        {
            FindObjectOfType<AudioManager>().Play("BulletHit3");
        }
        else if (RandomValue >= 0.8 && RandomValue <= 1)
        {
            FindObjectOfType<AudioManager>().Play("BulletHit4");
        }
    }
}
