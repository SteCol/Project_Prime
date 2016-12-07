using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtDelay : MonoBehaviour {

    public GameObject target;
    public float damping;

    public float speed;

    void Update()
    {
        //SmoothLookAt
        Quaternion rotation = Quaternion.LookRotation(target.transform.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * damping);

        //movement

        float dist = Vector3.Distance(target.transform.position, transform.position);
        
        if (dist > 1)
            transform.Translate(transform.forward * (dist * speed));
    }
}
