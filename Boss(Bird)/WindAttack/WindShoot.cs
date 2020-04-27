using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindShoot : MonoBehaviour
{
    private float timeBtwShots;
    public float startTimeBtwShots;

    public GameObject projectile;
    //public Transform shooter;

    Boss bossScript;


    // Start is called before the first frame update
    void Start()
    {
        timeBtwShots = startTimeBtwShots;

        GameObject bossObject = GameObject.FindWithTag("Boss");
        if (bossObject != null)
        {
            bossScript = bossObject.GetComponent<Boss>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(bossScript.end == false)
        {
            shotDelay();
        }
        
    }

    void shotDelay()
    {
        if (timeBtwShots <= 0)
        {
            Instantiate(projectile, transform.position, Quaternion.identity);
            timeBtwShots = startTimeBtwShots;
        }
        else
        {
            timeBtwShots -= Time.deltaTime;
        }
    }

}
