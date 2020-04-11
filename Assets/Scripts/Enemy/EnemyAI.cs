using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public Transform[] waypoints;
    public Transform player;
    public Transform laser;

    public float radius = 10f;
    public float range = 15f;
    public float damage = 1;

    public GameObject target;
    public GameObject eyeSight;

    int _nextpoint = 0;

    NavMeshAgent _agent;
    PlayerControllerRB _playerController;
    GameManager _gm;

    public AudioSource angryCat;

   
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
            GoToNextPoint();


        //Finds the player script
        GameObject playerObject = GameObject.FindWithTag("Player");
        if (playerObject != null)
        {
            _playerController = playerObject.GetComponent<PlayerControllerRB>();
        }
        if (playerObject == null)
        {
            Debug.Log("Cannot find 'Player' script");
        }

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

    //Used for waypoints paths
    void GoToNextPoint()
    {
        //Returns if no points are setup
        if (waypoints.Length == 0)
            return;

        //sets path for agent
        _agent.destination = waypoints[_nextpoint].position;

        //cycles thru the wp
        _nextpoint = (_nextpoint + 1) % waypoints.Length;

    }

    public void FollowPlayer()
    {
        Debug.Log("Following Player");
        _agent.SetDestination(player.transform.position);
        angryCat.Play();
    }

    public void FollowLaser()
    {
        Debug.Log("Following Laser");
        _agent.SetDestination(laser.transform.position);
    }

    void Update()
    {
        //chooses the next point when agent is close to current wp
        if (!_agent.pathPending && _agent.remainingDistance < 0.2f)
            GoToNextPoint();

        //if player is a certain distance away follow
        float distance = Vector3.Distance(player.position, transform.position);
        if (distance < radius)
        {
            FollowPlayer();
        }

        //if laser is near
        float laserDistance = Vector3.Distance(laser.position, transform.position);
        if (laserDistance < radius)
        {
            FollowLaser();

        }

        RaycastHit hit;
        if (Physics.Raycast(eyeSight.transform.position, eyeSight.transform.forward, out hit, range))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.blue);
            if (hit.transform.gameObject == target)
            {
                FollowPlayer();
                Debug.Log("I Hit: " + hit.transform.name);

            }
        }
    }
    //Removes a life from player when collided
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //Debug.Log("Life Lost");
            _gm.TakeDamage(damage);

        }
    }
}
