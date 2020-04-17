using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class Interact : MonoBehaviour
{
    public Transform player;
    public float radius = 5f;
    public Flowchart convo;
    public Flowchart convo2;

    GameManager _gm;

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
   
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.JoystickButton4))
        {
            float distance = Vector3.Distance(player.position, transform.position);
            if (distance < radius)
            {
                Debug.Log("Interact");
                convo.ExecuteBlock("Convo1");
            }
        }

        if(_gm.numOfCatnip >= 1)
        {
            if (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.JoystickButton4))
            {
                float distance = Vector3.Distance(player.position, transform.position);
                if (distance < radius)
                {
                    Debug.Log("GotNip");
                    convo2.ExecuteBlock("Convo2");
                }
            }
        }
    }
}
