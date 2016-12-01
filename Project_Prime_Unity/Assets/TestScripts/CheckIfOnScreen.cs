using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckIfOnScreen : MonoBehaviour {

    public Camera camera;
    public GameObject target;

    public bool onScreen;

	// Use this for initialization
	void Start () {
        camera = Camera.main;
        target = this.gameObject;
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 screenPoint = camera.WorldToViewportPoint(target.transform.position);
        onScreen = screenPoint.z > 0 && screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1;
    }
}
