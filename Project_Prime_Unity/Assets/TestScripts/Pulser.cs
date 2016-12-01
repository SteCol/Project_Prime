using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Pulser : MonoBehaviour {
    public List<GameObject> toPulse;
    public FlickerTypes flickerType; 
    public float minPulse, maxPulse, value;


	// Use this for initialization
	void Start () {
        
            
    }
	
	// Update is called once per frame
	void Update () {
        /*
        GameObject[] allObjects = UnityEngine.Object.FindObjectsOfType<GameObject>();
        foreach (GameObject go in allObjects) {
            if (go.GetComponent<Material>() != null)
            {
                Renderer rend = go.GetComponent<Renderer>();
                go.GetComponent<Renderer>().material.shader = Shader.Find("UCLA Game Lab/Wireframe/Double-Sided");
                go.GetComponent<Renderer>().material.SetFloat("_Thickness", Random.Range(0, 20));
            }
        }
        */

        if (flickerType == FlickerTypes.Independent)
        {
            for (int i = 0; i < toPulse.Count; i++)
            {
                Renderer rend = toPulse[i].GetComponent<Renderer>();
                //toPulse[i].GetComponent<Renderer>().material.shader = Shader.Find("UCLA Game Lab/Wireframe/Double-Sided");
                toPulse[i].GetComponent<Renderer>().material.SetFloat("_Thickness", Random.Range(minPulse, maxPulse));

            }
        }

        if (flickerType == FlickerTypes.Synced)
        {
            float value = Random.Range(minPulse, maxPulse);
            for (int i = 0; i < toPulse.Count; i++)
            {
                Renderer rend = toPulse[i].GetComponent<Renderer>();
                //toPulse[i].GetComponent<Renderer>().material.shader = Shader.Find("UCLA Game Lab/Wireframe/Double-Sided");
                toPulse[i].GetComponent<Renderer>().material.SetFloat("_Thickness", value);

            }
        }

    }
}

public enum FlickerTypes{
    Synced,
    Independent
}