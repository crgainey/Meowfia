﻿
using UnityEngine;

public class ShootHairball : MonoBehaviour
{

    public Rigidbody hairBall;
    public Transform hairballShooter;
    public float speed;
    public float timeBetweenShots = 2f;


    private float shootDelay;


    

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire2") && Time.time >= shootDelay)
        {
            Shoot();
            shootDelay = Time.time + timeBetweenShots;
        }

    }

    public void Shoot()
    {
        
        
            
            Rigidbody hairballInstance;
            hairballInstance = Instantiate(hairBall, hairballShooter.position, Quaternion.identity) as Rigidbody;
            hairballInstance.AddForce(hairballShooter.forward * speed);

            
        
        
    }

    

}