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

    public GameObject target;
    public GameObject eyeSight;

    int _nextpoint = 0;

    NavMeshAgent _agent;


    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        GoToNextPoint();
    }

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

        //if playerOrientation is a certain distance away follow
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
}
