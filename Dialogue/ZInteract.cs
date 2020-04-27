using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class ZInteract : MonoBehaviour
{
    Transform _player;
    public float radius = 5f;
    public Flowchart ZToInteract;

    void Start()
    {
        _player = GameObject.FindWithTag("Player").transform;
    }

    void Update()
    {
        float distance = Vector3.Distance(_player.position, transform.position);
        if (distance < radius)
        {
            ZToInteract.ExecuteBlock("ZToInteract");
                
        }
    }
}
