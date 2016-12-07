using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockOn : MonoBehaviour {

    [Header("General info")]

    public Camera playerCamera;

    public bool lockOn;
    private float mouseSpeed;

    [Header("All the lists")]
    public GameObject[] shootableArray;
    public List<GameObject> shootable;
    public List<GameObject> shootableOnScreen;
    public List<Vector2> screenLocations;
    public List<float> distanceFromCenter;

    [Header("Lock on to stuff")]
    public GameObject closestShootable;
    public float damping;
    public float cameraZoomLevel;
    public GameObject lockOnCirkle;
    public GameObject lookAt, lookAtB;
    public GameObject lookAtGimble;
    public float manualAimSlerp;
    public float LockOnCircleSize;

    [Header("Guns")]
    public List<GameObject> guns;




    // Use this for initialization
    void Start () {
        mouseSpeed = GetComponent<Movement>().mouseSpeed;

        playerCamera = Camera.main;

        cameraZoomLevel = playerCamera.fieldOfView;

        shootableArray = GameObject.FindGameObjectsWithTag("Shootable");

        foreach (GameObject shootableObj in GameObject.FindGameObjectsWithTag("Shootable"))
        {
            shootable.Add(shootableObj);
        }
    }
	
	// Update is called once per frame
	void Update () {

        /*
        if (closestShootable == null) {
            closestShootable = this.gameObject;
        }
        */



        if (Input.GetMouseButtonDown(1)) {
            lockOn = true;
        }
        

        if (Input.GetMouseButtonUp(1))
        {
            lockOn = false;
            LockOnCircleSize = 0;
        }
        

        if (lockOn == true) {
            CheckIfOnScreen();
            GetLocationOnViewPort();
        }

        DrawLockOnCirkle();
        DrawLine();
        CameraZoom();

        SmoothAim();

        if (closestShootable == null && lockOn == true)
        {
            lookAtB.transform.Translate(0, Input.GetAxis("Mouse Y") * mouseSpeed / 50, 0);
            manualAimSlerp = 0;
            //lookAtGimble.transform.Rotate(Vector3.up * Input.GetAxis("Mouse Y") * Time.deltaTime, Space.World);
            //lookAtGimble.GetComponent<LookAtGimble>().matchRotation = new Vector3(lookAtGimble.GetComponent<LookAtGimble>().matchRotation.x, lookAtGimble.GetComponent<LookAtGimble>().matchRotation.y, Input.GetAxis("Mouse Y"));
        }
        else {
            Vector3 newPos = new Vector3(lookAtB.transform.position.x, 0, lookAtB.transform.position.z);
            if (manualAimSlerp < 1)
                manualAimSlerp = manualAimSlerp + 1 * Time.deltaTime;
            lookAtB.transform.position = Vector3.Slerp(lookAtB.transform.position, newPos, manualAimSlerp);

        }
    }

    void CheckIfOnScreen() {
        shootableOnScreen.Clear();

        for (int i = 0; i < shootable.Count; i++)
        {
            if (shootable[i].GetComponent<CheckIfOnScreen>() != null && shootable[i].GetComponent<CheckIfOnScreen>().onScreen == true && lockOn == true)
            {
                shootableOnScreen.Add(shootable[i]);
            }
        }
    }

    void GetDistance() { //For future use to limit range.
        for (int i = 0; i < shootableOnScreen.Count; i++) {
            float dist = Vector3.Distance(transform.position, shootableOnScreen[i].transform.position);
        }
    }

    void GetLocationOnViewPort() {
        screenLocations.Clear();
        distanceFromCenter.Clear();

        //Converting the 3D position to on-screen coordinates.
        for (int i = 0; i < shootableOnScreen.Count; i++)
        {
            screenLocations.Add( Camera.main.WorldToScreenPoint(shootableOnScreen[i].transform.position));
        }

        for (int i = 0; i < screenLocations.Count; i++)
        {
            //Converting coordinates 'from bottom left' to 'from center'.
            screenLocations[i] = new Vector2(screenLocations[i].x - (Screen.width / 2), screenLocations[i].y - (Screen.height / 2));

            //Make all values positive.
            if (screenLocations[i].x < 0) { screenLocations[i] = new Vector2 ( -screenLocations[i].x, screenLocations[i].y); }
            if (screenLocations[i].y < 0) { screenLocations[i] = new Vector2(screenLocations[i].x, -screenLocations[i].y); }


            //Convert to single distance vector.
            float distance = (screenLocations[i].x + screenLocations[i].y);
            distanceFromCenter.Add(distance);
        }

        //Find closest GameObject to center.
        for (int i = 0; i < distanceFromCenter.Count; i++) {
            if (distanceFromCenter[i] == Mathf.Min(distanceFromCenter.ToArray())){
                closestShootable = shootableOnScreen[i];
            }
        }
    }



    void CameraZoom()
    {
        if (closestShootable != null && lockOn == true)
        {
            playerCamera.GetComponent<Camera>().fieldOfView = cameraZoomLevel - 5;
            //SmoothLookAt
            Quaternion rotation = Quaternion.LookRotation(closestShootable.transform.position - playerCamera.transform.position);
            playerCamera.transform.rotation = Quaternion.Slerp(playerCamera.transform.rotation, rotation, Time.deltaTime * damping / 3);
        }
        else
        {
            playerCamera.GetComponent<Camera>().fieldOfView = cameraZoomLevel;
            //Camera.main.transform.eulerAngles = new Vector3(0,Camera.main.transform.eulerAngles.y,0);

            Quaternion rotation = Quaternion.LookRotation(lookAtB.transform.position - playerCamera.transform.position);
            playerCamera.transform.rotation = Quaternion.Slerp(playerCamera.transform.rotation, rotation, Time.deltaTime * damping / 3);
        }
    }

    void SmoothAim()
    {
        foreach (GameObject g in guns)
        {
            if (closestShootable != null && closestShootable.GetComponent<CheckIfOnScreen>().onScreen == true && lockOn)
            {
                //SmoothLookAt
                Quaternion rotation = Quaternion.LookRotation(closestShootable.transform.position - g.transform.position);
                g.transform.rotation = Quaternion.Slerp(g.transform.rotation, rotation, Time.deltaTime * damping);
            }
            else
            {
                Quaternion rotation = Quaternion.LookRotation(lookAt.transform.position - g.transform.position);
                g.transform.rotation = Quaternion.Slerp(g.transform.rotation, rotation, Time.deltaTime * damping);

            }
        }
    }

    void DrawLine()
    {
        GetComponent<LineRenderer>().SetPosition(0, guns[0].transform.position);
        if (closestShootable != null && closestShootable.GetComponent<CheckIfOnScreen>().onScreen == true && lockOn)
            GetComponent<LineRenderer>().SetPosition(1, closestShootable.transform.position);
        else
            GetComponent<LineRenderer>().SetPosition(1,  guns[0].transform.position);
    }

    void DrawLockOnCirkle()
    {
        //Lockon Cirkle

        if (LockOnCircleSize < 1)
            LockOnCircleSize = LockOnCircleSize + 2 * Time.deltaTime;

        float dist = Vector3.Distance(transform.position, lockOnCirkle.transform.position);

        lockOnCirkle.transform.localScale = new Vector3((dist * LockOnCircleSize) / 80, (dist * LockOnCircleSize) / 80, (dist * LockOnCircleSize) / 80);


        if (closestShootable != null && closestShootable.GetComponent<CheckIfOnScreen>().onScreen == true && lockOn)
        {
            if (lockOnCirkle.active == false)
                lockOnCirkle.active = true;
            lockOnCirkle.transform.position = closestShootable.transform.position;
            lockOnCirkle.transform.LookAt(this.transform.position);
        }
        else if (lockOnCirkle.active == true)
        {
            lockOnCirkle.active = false;
        }
    }
}
