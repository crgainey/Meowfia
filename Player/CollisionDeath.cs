using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDeath : MonoBehaviour
{
    GameManager _gm;
    public float damage = 1;

    void Start()
    {
        GameObject gmObject = GameObject.FindWithTag("GameController");
        if (gmObject != null)
        {
            _gm = gmObject.GetComponent<GameManager>();
        }
        if (gmObject == null)
        {
            Debug.Log("Cannot find 'GameController' script");
        }
    }
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //Debug.Log("Enemy took life");
            _gm.TakeDamage(damage);

        }
    }
}
