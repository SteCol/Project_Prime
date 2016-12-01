using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Autotarget : MonoBehaviour {

    public float accuracy;
    public float range;
    public Pulser pulser;

    public GameObject[] shootableArray;
    public List<GameObject> shootable;

    public List<GameObject> inRange;
    public List<float> inRangeDist;
    //public List<float> inRangeSort;
    public int inRangeSortValue;
    public float acclimatisationRate;
    public float minRange;
    public float damping;
    public GameObject weapon;

    public List<GameObject> guns;

    public GameObject rangeCirkle;
    public GameObject lockOnCirkle;
    public GameObject lookAt;



    private IEnumerator coroutine;

    // Use this for initialization
    void Start () {

        shootableArray = GameObject.FindGameObjectsWithTag("Shootable");
       // shootable = shootableArray;

        foreach (GameObject shootableObj in GameObject.FindGameObjectsWithTag("Shootable"))
        {

            shootable.Add(shootableObj);
        }

        //coroutine = GetDistance();
        //StartCoroutine(coroutine);

    }

    // Update is called once per frame
    void Update() {


        GetDistance();
        if (Input.GetKeyDown("f"))
        {
            //StartCoroutine(coroutine);
        }


        if (inRange.Count > 1) {
            minRange = minRange - acclimatisationRate;
        }
        else if (inRange.Count < 1  && minRange < range)
        {
            minRange = minRange + acclimatisationRate;
        }

        DrawLine();
        DrawLockOnCirkle();
        DrawRangeCirkle();
        
        SmoothAim();
    }

    void SmoothAim() {
        foreach (GameObject g in guns)
        {
            if (inRange.Count > 0)
            {

                Quaternion rotation = Quaternion.LookRotation(inRange[0].transform.position - g.transform.position);
                g.transform.rotation = Quaternion.Slerp(g.transform.rotation, rotation, Time.deltaTime * damping);
            }
            else
            {
                Quaternion rotation = Quaternion.LookRotation(lookAt.transform.position - g.transform.position);
                g.transform.rotation = Quaternion.Slerp(g.transform.rotation, rotation, Time.deltaTime * damping);

            }
        }
    }

    void DrawLine() {
        GetComponent<LineRenderer>().SetPosition(0, /*new Vector3(transform.position.x, transform.position.y - 1f, transform.position.z)*/ weapon.transform.position);
        if (inRange.Count > 0)
            GetComponent<LineRenderer>().SetPosition(1, inRange[0].transform.position);
        else
            GetComponent<LineRenderer>().SetPosition(1, /*new Vector3(transform.position.x, transform.position.y - 1f, transform.position.z)*/ weapon.transform.position);
    }

    void GetDistance() {
        inRange.Clear();
        inRangeDist.Clear();

        for (int i = 0; i < shootable.Count; i++)
        {
            if (shootable[i].GetComponent<CheckIfOnScreen>() != null && shootable[i].tag == "Shootable" &&  shootable[i].GetComponent<CheckIfOnScreen>().onScreen == true)
            {
                float dist = Vector3.Distance(this.transform.position, shootable[i].transform.position);
                if (dist < minRange)
                {
                    inRange.Add(shootable[i]);

                    inRangeDist.Add(dist);
                }
            }
        }
    }

    void DrawRangeCirkle() {
        //Range Cirkle

        if (inRangeDist.Count > 0)
        {
            if (rangeCirkle.active == false)
                rangeCirkle.active = true;
            rangeCirkle.transform.localScale = new Vector3( inRangeDist[0] / 60, inRangeDist[0] / 60, inRangeDist[0] / 60);
        }
        else if (rangeCirkle.active == true)
        {
            rangeCirkle.active = false;
        }
    }

    void DrawLockOnCirkle()
    {
        //Lockon Cirkle

        if (inRangeDist.Count > 0)
        {
            if (lockOnCirkle.active == false)
                lockOnCirkle.active = true;
            lockOnCirkle.transform.position = inRange[0].transform.position;
            lockOnCirkle.transform.LookAt(this.transform.position);
        }
        else if (lockOnCirkle.active == true)
        {
            lockOnCirkle.active = false;
        }
    }
}
