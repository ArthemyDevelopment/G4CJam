using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

[DefaultExecutionOrder(-5)]
public class Movement2 : SingletonManager<Movement2>
{
    private Transform camera;

    private Vector3 movDirection;
    public Transform StartPosition;

    public float StartMovSpeed;
    [HideInInspector]public float movSpeed;
    public float jumpForce;

    public GameObject Shadow;
    public Animator PlayerAnim;
    public Animator PlayerTransAnim;
    public SpriteRenderer PlayerSprite;
    public SpriteRenderer PlayerTransSprite;

    [SerializeField] bool isGrounded;
    private Rigidbody rb;

    private Vector3 relativeForward;
    private Vector3 relativeRight;

    private Vector3 tempMovDirection;
    // Start is called before the first frame update

    private void Awake()
    {
        init();
    }

    void Start()
    {
        Application.targetFrameRate = 60;
        movSpeed = StartMovSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        DetectFloor();
            
        ShadowCast();
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

        relativeForward = camera.forward * Input.GetAxis("Vertical");
        relativeRight = camera.right * Input.GetAxis("Horizontal");
        tempMovDirection = relativeForward + relativeRight;
          
            
        movDirection = new Vector3(tempMovDirection.x,0, tempMovDirection.z);

        SetAnimations();
        
    }

    void SetAnimations()
    {
        if (movSpeed == 0)
        {
            PlayerAnim.SetBool("IsMoving", false);
            return;
        }
        
        if (movDirection.x > 0)
        {
            PlayerSprite.flipX = false;
            PlayerTransSprite.flipX = false;
            
        }
        else if (movDirection.x < 0)
        {
            PlayerSprite.flipX = true;
            PlayerTransSprite.flipX = true;
            
        }
        
        if (movDirection.magnitude !=0)
        {
            PlayerAnim.SetBool("IsMoving", true);
            PlayerTransAnim.SetBool("IsMoving", true);
        }
        else if (movDirection.magnitude == 0)
        {
            PlayerAnim.SetBool("IsMoving", false);
            PlayerTransAnim.SetBool("IsMoving", false);
        }
        
        if (movDirection.z > 0)
        {
            PlayerAnim.SetBool("IsFront", false);
            PlayerTransAnim.SetBool("IsFront", false);
        }
        else if (movDirection.z <= 0)
        {
            PlayerAnim.SetBool("IsFront", true);
            PlayerTransAnim.SetBool("IsFront", true);
        }
    }

    public void FixedUpdate()
    {
        
        
            if (camera == null)
                camera = Camera.main.transform;
            
            
            relativeForward = camera.forward * Input.GetAxis("Vertical");
            relativeRight = camera.right * Input.GetAxis("Horizontal");
            tempMovDirection = relativeForward + relativeRight;
          
            
            movDirection = new Vector3(tempMovDirection.x,0, tempMovDirection.z);
            
            if(rb==null)
                rb=GetComponent<Rigidbody>();
            
            //rb.velocity= movDirection*movSpeed;
            rb.AddForce(movDirection * movSpeed, ForceMode.Impulse);

            ClampMove();



        
    }

    public void ResetPosition()
    {
        transform.position = StartPosition.position;
    }
    
    void ClampMove()
    {
        Vector3 temp = rb.velocity;
        temp.y = 0;
        temp = Vector3.ClampMagnitude(temp, movSpeed);
        temp.y = rb.velocity.y;
        rb.velocity = temp;
    }
    
    void DetectFloor()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, .2f))
        {
            if (hit.distance <= .1)
            {
                isGrounded = true;
                PlayerAnim.SetBool("IsJumping", false);
                PlayerTransAnim.SetBool("IsJumping", false);
            }
            else
            {
                isGrounded = false;
                PlayerAnim.SetBool("IsJumping", true);
                PlayerTransAnim.SetBool("IsJumping", true);
            }
        }
        else
        {
            isGrounded = false;
            PlayerAnim.SetBool("IsJumping", true);
            PlayerTransAnim.SetBool("IsJumping", true);
        }
        
    }

    void ShadowCast()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 15))
        {
            if(!Shadow.activeSelf)
                Shadow.SetActive(true);

            Shadow.transform.position = hit.point +(Vector3.up * 0.01f);
        }
        else if(Shadow.activeSelf)
            Shadow.SetActive(false);
        
    }
}
