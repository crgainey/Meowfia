using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SenturyGun : MonoBehaviour
{
    private float timeBtwShots;
    public float startTimeBtwShots;

    public GameObject projectile;
    public Transform shooter;

    public Transform target;


    // Start is called before the first frame update
    void Start()
    {
        timeBtwShots = startTimeBtwShots;
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            transform.LookAt(target);

        }
    }

    void shotDelay()
    {
        if (timeBtwShots <= 0)
        {
            Instantiate(projectile, shooter.position, Quaternion.identity);
            timeBtwShots = startTimeBtwShots;
        }
        else
        {
            timeBtwShots -= Time.deltaTime;
        }
    }
}
