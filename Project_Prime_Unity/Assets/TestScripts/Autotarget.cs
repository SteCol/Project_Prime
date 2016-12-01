using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Autotarget : MonoBehaviour {

    public float accuracy;
    public float range;
    public Pulser pulser;

    public List<GameObject> inRange;
    public List<float> inRangeDist;
    public List<float> inRangeSort;
    public int inRangeSortValue;
    public float acclimatisationRate;
    public float minRange;
    public float damping;
    public GameObject weapon;

    public List<GameObject> guns;

    public GameObject cirkle;


    private IEnumerator coroutine;

    // Use this for initialization
    void Start () {

        //coroutine = GetDistance();
        //StartCoroutine(coroutine);

    }

    // Update is called once per frame
    void Update() {

        DrawCirkle();

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
        //Debug.Log("Starting core routine");
        inRange.Clear();
        inRangeDist.Clear();
        for (int i = 0; i < pulser.toPulse.Count; i++)
        {
            if (pulser.toPulse[i].tag == "Shootable")
            {
                float dist = Vector3.Distance(this.transform.position, pulser.toPulse[i].transform.position);
                if (dist < minRange)
                {
                    inRange.Add(pulser.toPulse[i]);

                    //inRange.Insert(inRange.Count, pulser.toPulse[i]);
                    inRangeDist.Add(dist);
                    //yield return new WaitForSeconds(0.1f);
                }
            }
        }
        //Debug.Log("Finished core routine");
    }

    void DrawCirkle() {
        cirkle.transform.localScale = new Vector3(minRange/2, minRange/2, minRange/2);
    }
}
