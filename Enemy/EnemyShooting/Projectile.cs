using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;

    private Transform player;
    private Vector3 target;

    GameManager _gm;
    public ParticleSystem steam;

    public float damage = 1f;



    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
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
        steam.Play();
    }

    // Update is called once per frame
    void Update()
    {
        

        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

        if(transform.position.x == target.x && transform.position.y == target.y)
        {
            DestroyProjectile();
        }
    }

    void DestroyProjectile()
    {
        Destroy(gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            DestroyProjectile();
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //Enemy took 1 life;
            _gm.TakeDamage(damage);

        }
    }

}
