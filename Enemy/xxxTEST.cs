using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class xxxTest : MonoBehaviour
{
    public Transform[] waypoints;
    public Transform player;
    public Transform laser;

    float _radius = 10f;
    float _range = 15f;
    public float damage = 1;

    public GameObject target;
    public GameObject eyeSight;

    int _nextpoint = 0;

    NavMeshAgent _agent;
    GameManager _gm;

    public AudioSource purrSound;
    public AudioClip[] angryCat;
    AudioSource _audioSource;

    public Animator anim;
    float _walkAnimSpeed = 1.5f;
    float _chaseAnimSpeed = 3f;

    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        GoToNextPoint();

        GameObject gmObject = GameObject.FindWithTag("GameController");
        if (gmObject != null)
        {
            _gm = gmObject.GetComponent<GameManager>();
        }
        if (gmObject == null)
        {
            Debug.Log("Cannot find 'GameController' script");
        }

        _audioSource = GetComponent<AudioSource>();

    }

    void Update()
    {
        //chooses the next point when agent is close to current wp
        if (!_agent.pathPending && _agent.remainingDistance < 0.2f)
            GoToNextPoint();

   

        //if laser is near
        float laserDistance = Vector3.Distance(laser.position, transform.position);
        if (laserDistance < _radius)
        {
            purrSound.Play();
            FollowLaser();
        }

        RaycastHit hit;
        if (Physics.Raycast(eyeSight.transform.position, eyeSight.transform.forward, out hit, _range))
        {
            //Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.blue);
            if (hit.transform.gameObject == target)
            {
               
                Debug.Log("I Hit: " + hit.transform.name);

            }
        }
    }

    //Used for waypoints paths
    void GoToNextPoint()
    {
        _agent.speed = 3f;
        anim.speed = _walkAnimSpeed;
        //Returns if no points are setup
        if (waypoints.Length == 0)
            return;

        //sets path for agent
        _agent.destination = waypoints[_nextpoint].position;

        //cycles thru the wp
        _nextpoint = (_nextpoint + 1) % waypoints.Length;

    }

    public void FollowLaser()
    {
        _agent.speed = 10f;
        Debug.Log("Following Laser");
        _agent.SetDestination(laser.transform.position);

    }


}
