using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour
{

    public Rigidbody rb;

    public float movementSpeed;
    public float maxSpeed;
    public float mouseSpeed;
    public float gravity;

    public bool jump, jumping;
    public float jumpTimer, jumpValue;
    public float jumpHeight;

    public bool onFloor;

    public MovementTech movementTech;

    void Update()
    {

        FloorCheck();

        switch (movementTech)
        {
            case MovementTech.Transform:
                TransformMovement();
                break;
            case MovementTech.Physics:
                PhysicsMovement();
                break;
        }

        //look with Keyboard
        transform.Rotate(Vector3.up, Input.GetAxis("HorizontalArrow") * mouseSpeed * Time.deltaTime);

        //look with Mouse
        transform.Rotate(Vector3.up, Input.GetAxis("Mouse X") * mouseSpeed * Time.deltaTime);
        //transform.Rotate(Vector3.left, Input.GetAxis("Mouse Y") * mouseSpeed * Time.deltaTime);
        //transform.Rotate(-Input.GetAxis("Mouse Y") * mouseSpeed * Time.deltaTime, Input.GetAxis("Mouse X") * mouseSpeed * Time.deltaTime, 0);



    }

    public void TransformMovement()
    {
        //Walking with transform
        transform.Translate(Input.GetAxis("Horizontal") * movementSpeed * Time.deltaTime, 0.0f, Input.GetAxis("Vertical") * movementSpeed * Time.deltaTime);

        /*
        if (onFloor)
            transform.Translate(Input.GetAxis("Horizontal") * movementSpeed * Time.deltaTime, 0.0f, Input.GetAxis("Vertical") * movementSpeed * Time.deltaTime);
        else
            transform.Translate(Input.GetAxis("Horizontal") * movementSpeed * Time.deltaTime, -0.5f, Input.GetAxis("Vertical") * movementSpeed * Time.deltaTime);
            */

        TransformJump();

    }

    public void TransformJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && onFloor)
        {
            jumping = true;
            jump = true;
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            jumping = false;
            jump = false;
            jumpTimer = 0;
        }

        if (jumpTimer > jumpValue)
            jumping = false;


        if (jumping == true && jumpTimer < jumpValue)
        {
            jumpTimer = jumpTimer + 1 * Time.deltaTime;
        }
        else if (jumpTimer > 0)
        {
            jumpTimer = jumpTimer - 1 * Time.deltaTime;
        }

        if (jumping)
            transform.Translate(0, jumpHeight * Time.deltaTime, 0);
    }

    public void FloorCheck()
    {

        Vector3 dwn = transform.TransformDirection(-Vector3.up);
        Vector3 from = new Vector3(transform.position.x, transform.position.y - 3f, transform.position.z);

        Debug.DrawRay(from, dwn, Color.green);

        if (Physics.Raycast(from, dwn, 2))
            onFloor = true;
        else
            onFloor = false;
    }

    public void PhysicsMovement()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        float moveUp = gravity;


        

        Vector3 forwardVel = transform.forward * movementSpeed * moveVertical;
        Vector3 horizontalVel = transform.right * movementSpeed * moveHorizontal;
        Vector3 jumpVel = new Vector3(0,0,0);



        if (Input.GetKey(KeyCode.Space))
        {
             jumpVel = Vector3.up * moveUp;
        }
        else {
             jumpVel = -Vector3.up * moveUp;
        }



        //Vector3 movement = new Vector3(moveHorizontal , -0.5f, moveVertical);
        //this.gameObject.GetComponent<Rigidbody>().velocity = movement * movementSpeed;

        //rb.AddRelativeForce(movement * movementSpeed * 100);

        //if (rb.velocity.magnitude < maxSpeed)
        //{
        //rb.velocity = new Vector3(moveHorizontal * movementSpeed, 0, moveVertical * movementSpeed);
        rb.velocity = forwardVel + horizontalVel + jumpVel;

        if (Input.GetKeyDown(KeyCode.Space) && onFloor)
            rb.AddRelativeForce(Vector3.up * 100);

        //}

    }
}

public enum MovementTech
{
    Transform,
    Physics
}
