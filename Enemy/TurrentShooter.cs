using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TurrentShooter : MonoBehaviour
{
    public float damage = 1;
    public float range = 200f;
    public float radius = 5f;

    public GameObject shotPoint;
    public Transform shot;
    public ParticleSystem flash;
    public GameObject project;

    //public AudioSource bangAudio;

    public GameObject player;
    GameManager _gm;
    //public GameObject Controller;
    public GameObject THIS_ONE;

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

    void Update()
    {
        float distance = Vector3.Distance(player.transform.position, transform.position);
        if (distance < radius)
        {
            Shoot();
        }
        // Rotate the camera every frame so it keeps looking at the target
        THIS_ONE.transform.LookAt(player.transform);
    }

    public void Shoot()
    {
        RaycastHit hit;
        if (Physics.Raycast(shotPoint.transform.position, shotPoint.transform.forward, out hit, range)) //checks if we hit something
        {
            Debug.Log(hit.transform.name);
            Debug.DrawRay(shotPoint.transform.position, shotPoint.transform.forward, Color.green * 50000);
            PlayerControllerRB player = hit.transform.GetComponent<PlayerControllerRB>();
            if (player != null)
            {
                flash.Play();
                Instantiate(project, shot.position, Quaternion.identity);
                _gm.TakeDamage(damage);
                //bangAudio.Play();
            }
        }
    }
}
