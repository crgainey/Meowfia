using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootHairball : MonoBehaviour
{

    public GameObject hairBall;
    public float speed;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {



        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            Shoot();
        }

    }

    public void Shoot()
    {
        //Every bullet instantiated will be called "instHB"
        GameObject instHB = Instantiate(hairBall, transform.position, Quaternion.identity) as GameObject;
        Rigidbody instHBRigidbody = instHB.GetComponent<Rigidbody>();
        instHBRigidbody.AddForce(transform.forward * speed);
    }

}
