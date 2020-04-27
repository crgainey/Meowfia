using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FlyingBird : MonoBehaviour
{
    public Transform[] waypoints;
    int _nextpoint = 0;

    NavMeshAgent _agent;

    // Start is called before the first frame update
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        //GoToNextPoint();
    }

    // Update is called once per frame
    void Update()
    {
        if (!_agent.pathPending && _agent.remainingDistance < 0.2f)
        {
            GoToNextPoint();
        }
            
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
}
