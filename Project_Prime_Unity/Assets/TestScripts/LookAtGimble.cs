using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtGimble : MonoBehaviour {

    public GameObject toFollow;
    public Vector3 matchRotation;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (transform.position != toFollow.transform.position)
            transform.position = toFollow.transform.position;

        transform.eulerAngles = new Vector3(0, toFollow.transform.eulerAngles.y, 0);
		
	}
}
