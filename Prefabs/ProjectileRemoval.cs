using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileRemoval : MonoBehaviour
{
    Boss bossScript;

    public float damage = 1f;

    public ParticleSystem hit;
    public AudioSource hitSound;

    // Start is called before the first frame update
    void Start()
    {
        GameObject gmObject = GameObject.FindWithTag("Boss");
        if (gmObject != null)
        {
            bossScript = gmObject.GetComponent<Boss>();
        }
        if (gmObject == null)
        {
            Debug.Log("Cannot find 'Boss' script");
        }
        Destroy(gameObject, 8f);
    }

    

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Boss")
        {
            Debug.Log("HittheBird");
            //Enemy took 1 life;
            bossScript.Damaged(damage);
            hit.Play();
            hitSound.Play();
            Destroy(gameObject);
        }
    }
    
}
