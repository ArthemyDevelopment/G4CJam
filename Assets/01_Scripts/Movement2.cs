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
    

    [SerializeField] bool isGrounded;
    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
        
    }

    // Update is called once per frame
    void Update()
    {
        DetectFloor();
            
            if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
            {
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            }

            //ClampMove();
        
    }

    public void FixedUpdate()
    {
        
        
            if (camera == null)
                camera = Camera.main.transform;
            
            
            Vector3 relativeForward = camera.forward * Input.GetAxis("Vertical");
            Vector3 relativeRight = camera.right * Input.GetAxis("Horizontal");
            Vector3 tempMovDirection = relativeForward + relativeRight;

            
            if(rb==null)
                rb=GetComponent<Rigidbody>();
            
            movDirection = new Vector3(tempMovDirection.x,0, tempMovDirection.z);
            
            
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
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 15))
        {
            if (hit.distance <= 1.01)
                isGrounded = true;
            else
                isGrounded = false;
        }
        else
            isGrounded = false;
        
    }
}
