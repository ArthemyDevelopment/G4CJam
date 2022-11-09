using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class Movement2 : MonoBehaviour
{
    private Transform camera;

    private Vector3 movDirection;

    public float movSpeed;
    public float jumpForce;

    public GameObject Shadow;
    public Animator PlayerAnim;
    public SpriteRenderer PlayerSprite;

    [SerializeField] bool isGrounded;
    private Rigidbody rb;

    private Vector3 relativeForward;
    private Vector3 relativeRight;

    private Vector3 tempMovDirection;
    // Start is called before the first frame update

    

    void Start()
    {
        Application.targetFrameRate = 60;
        
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

        if (movDirection.x > 0)
            PlayerSprite.flipX = false;
        else if(movDirection.x<0)
            PlayerSprite.flipX = true;
        
        if (movDirection.magnitude !=0)
        {
            PlayerAnim.SetBool("IsMoving", true);
        }
        else if (movDirection.magnitude == 0)
        {
            PlayerAnim.SetBool("IsMoving", false);
            
        }
        
        if (movDirection.z > 0)
        {
            PlayerAnim.SetBool("IsFront", false);
        }
        else if (movDirection.z <= 0)
        {
            PlayerAnim.SetBool("IsFront", true);
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
                isGrounded = true;
            else
                isGrounded = false;
        }
        else
            isGrounded = false;
        
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
