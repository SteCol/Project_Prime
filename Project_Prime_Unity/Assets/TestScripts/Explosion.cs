using UnityEngine;
using System.Collections;

public class Explosion : MonoBehaviour {

    public float timer;
    public float timerLimit;
    public float schale;


    // Use this for initialization
    void Start () {
        timer = 0;
        schale = 0;
	}
	
	// Update is called once per frame
	void Update () {
        timer = timer + 1 * Time.deltaTime;

        if (timer < timerLimit / 2)
        {
            schale = schale + 5 * Time.deltaTime;
        }
        else if (timer < timerLimit)
        {
            schale = schale - 5 * Time.deltaTime;
        }
        else {
            Destroy(this.gameObject);
        }

        transform.localScale = new Vector3(schale, schale, schale);
	
	}
}
