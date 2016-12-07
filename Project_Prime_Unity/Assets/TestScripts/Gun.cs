using UnityEngine;
using System.Collections;

public class Gun : MonoBehaviour
{

    public GameObject bulletPrefab;
    public float shotValue;
    public float reloadValue;
    public float reloadSpeed;
    public GameObject barrel;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        CalculateShotValue();
        BarrelAnimation();


        if (Input.GetMouseButtonUp(0) && reloadValue > 1)
        {
            GameObject clone = (GameObject)Instantiate(bulletPrefab, new Vector3(transform.position.x, transform.position.y , transform.position.z), transform.rotation);
            clone.transform.localScale = new Vector3(shotValue + 0.1f, shotValue + 0.1f, shotValue + 0.1f);
            clone.GetComponent<Bullet>().speed = shotValue;
            shotValue = 0f;
            reloadValue = 0f;

        }

        barrel.transform.localPosition = new Vector3(0, 0, reloadValue / 5);

        //recoil
        //transform.localPosition = new Vector3(0, 0, reloadValue / 20);


        barrel.transform.localScale = new Vector3(shotValue + 1f, barrel.transform.localScale.y, shotValue + 1f);
    }

    void BarrelAnimation() {
        if (reloadValue < 1)
            reloadValue = reloadValue + reloadSpeed * Time.deltaTime;
    }

    void CalculateShotValue()
    {
        if (Input.GetMouseButton(0) && reloadValue > 1)
        {
            if (shotValue < 1)
                shotValue = shotValue + 1 * Time.deltaTime;
        }
        
    }


}