using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyShoot : MonoBehaviour
{

    public Transform enemyShooter;//where object shoots from on the enemy
    public GameObject projectile;

   
    private float shootCooldown;
    public float startShootCooldown;


    void Start()
    {
        shootCooldown = startShootCooldown;
    }

    // Update is called once per frame
    void Update()
    {
        Shoot();
    }

    

    public void Shoot()
    {
        if(shootCooldown <= 0)
        {
            GameObject bullet = Instantiate(projectile, enemyShooter.position, Quaternion.identity);
            shootCooldown = startShootCooldown;
            
        }
        else
        {
            shootCooldown -= Time.deltaTime;
        }
    }

}
