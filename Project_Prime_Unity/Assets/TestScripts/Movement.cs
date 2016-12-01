using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {

    public float movementSpeed;
    public float mouseSpeed;

    void Update () {
        transform.Translate(Input.GetAxis("Horizontal") * movementSpeed * Time.deltaTime, 0, Input.GetAxis("Vertical") * movementSpeed * Time.deltaTime);

        //look with Keyboard
        transform.Rotate(Vector3.up, Input.GetAxis("HorizontalArrow") * mouseSpeed * Time.deltaTime);

        //look with Mouse
        transform.Rotate(Vector3.up, Input.GetAxis("Mouse X") * mouseSpeed * Time.deltaTime);
        //transform.Rotate(Vector3.left, Input.GetAxis("Mouse Y") * mouseSpeed * Time.deltaTime);
        //transform.Rotate(-Input.GetAxis("Mouse Y") * mouseSpeed * Time.deltaTime, Input.GetAxis("Mouse X") * mouseSpeed * Time.deltaTime, 0);



    }
}
