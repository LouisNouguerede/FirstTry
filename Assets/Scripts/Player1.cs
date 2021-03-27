using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1 : MonoBehaviour
{
    [SerializeField] private Transform groundCheckTransform;
    [SerializeField] private LayerMask player1Mask;

    private int healthPoints;
    private Rigidbody rigidBody;
    private bool jumpKeyPressed;
    private bool dashKeyPressed;
    private float horizontalInput;
    private int jumpToken;
    private int dashToken;
    

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        dashToken = 1;
        healthPoints = 2;
        jumpKeyPressed = false;
        dashKeyPressed = false;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Z))
        {
            jumpKeyPressed = true;
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            dashKeyPressed = true;
        }

        horizontalInput = Input.GetAxis("Horizontal1");
    }

    private void FixedUpdate()
    {
        //HorizontalMovement
        rigidBody.velocity = new Vector3(horizontalInput * 5, rigidBody.velocity.y, rigidBody.velocity.z);

        //Dash management
        if (dashKeyPressed && dashToken > 0 )
        {
            if (horizontalInput > 0)
            {
                rigidBody.AddForce(Vector3.right * 100, ForceMode.Impulse);
                dashToken--;
            }
            else if (horizontalInput < 0)
            {
                rigidBody.AddForce(Vector3.left * 100, ForceMode.Impulse);
                dashToken--;
            }
            else if (horizontalInput == 0)
            {

            }
            dashKeyPressed = false;
        }else if (dashKeyPressed && dashToken == 0)
        {
            dashKeyPressed = false;
        }
            
        //Jump management
        if (Physics.OverlapSphere(groundCheckTransform.position, 0.1f, player1Mask).Length == 1 )
        {
            jumpToken = 3;
        }
        if (jumpKeyPressed && jumpToken > 0)
        {
            rigidBody.AddForce(Vector3.up * 5, ForceMode.Impulse);          
            jumpKeyPressed = false;
            jumpToken--;
        }else if(jumpKeyPressed && jumpToken ==0)
        {
            jumpKeyPressed = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //layer 8 == DashCapsuleLayer 
        if(other.gameObject.layer == 8)
        {
            dashToken++;
            
        }
    }
}
