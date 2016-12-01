using UnityEngine;
using System.Collections;

public class CubeRotator : MonoBehaviour {
    public Vector3 rotationSpeed;

	void Update () {
        transform.Rotate(Vector3.right  * rotationSpeed.x* Time.deltaTime);
        transform.Rotate(Vector3.up * rotationSpeed.y * Time.deltaTime);
        transform.Rotate(Vector3.forward * rotationSpeed.z * Time.deltaTime);

    }
}
