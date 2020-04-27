using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindProjectile : MonoBehaviour
{
    public float speed;

    private Transform player;
    private Vector3 target;

    GameManager _gm;

    public float damage = 2f;

    public ParticleSystem dust;

    // Start is called before the first frame update
    void Start()
    {
        
        player = GameObject.FindGameObjectWithTag("WindDestination").transform;
        target = new Vector3(player.position.x, player.position.y, player.position.z);

       

        GameObject gmObject = GameObject.FindWithTag("GameController");
        if (gmObject != null)
        {
            _gm = gmObject.GetComponent<GameManager>();
        }
        if (gmObject == null)
        {
            Debug.Log("Cannot find 'GameController' script");
        }

        dust.Play();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        dust.Play();
        /*if (transform.position.x == target.x && transform.position.y == target.y && transform.position.z == target.z)
        {
            StartCoroutine(DestroyWind());
        }
        */
        StartCoroutine(DestroyWind());
    }

    void DestroyProjectile()
    {
        Destroy(gameObject);
    }

    /*void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("WindDestination"))
        {
            
            DestroyProjectile();
        }
    }
    */
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //Enemy took 2 lifes;
            //_gm.TakeDamage(damage);

        }
    }

    IEnumerator DestroyWind()
    {
        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
    }

}
