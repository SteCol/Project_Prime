using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {
    public float speed;
    public GameObject explosion;

	void Start () {
        Destroy(this.gameObject, 5);
	}
	
	void Update () {
        //transform.localPosition = new Vector3(transform.position.x + speed * Time.deltaTime, 0, 0);

        //this.transform.Translate(0, 0, speed * Time.deltaTime);

        this.transform.GetComponent<Rigidbody>().velocity = transform.forward * ( 0.5f + speed )* 100;
    }


    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Shootable")
        {
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<LockOn>().shootable.Remove(collision.gameObject);
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Pulser>().toPulse.Remove(collision.gameObject);

            //GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Autotarget>().inRange.Remove(collision.gameObject);
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<LockOn>().closestShootable = null;

            Destroy(collision.gameObject);
        }

        if (collision.gameObject.tag != "Bullet" && collision.gameObject.tag != "MainCamera")
        {
            GameObject clone = (GameObject)Instantiate(explosion, new Vector3(transform.position.x, transform.position.y, transform.position.z), transform.rotation);
            Destroy(this.gameObject);

        }
    }
}
