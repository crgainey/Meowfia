using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class LevelOneInteract : MonoBehaviour
{
    public Transform player;
    public float radius = 5f;
    public Flowchart convo;
    public Flowchart convo2;

    public GameObject levelDoor;

    GameManager _gm;

    void Start()
    {
        levelDoor.SetActive(false);

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
        float distance = Vector3.Distance(player.position, transform.position);
       
        if (distance < radius)
        {
            if (_gm.numOfCatnip >= 1)
            {
                Debug.Log("GotNip");
                convo2.ExecuteBlock("Convo2");
                convo2.SetBooleanVariable("Convo2Bool", true);
                levelDoor.SetActive(true);
            }

            {
                Debug.Log("Interact");
                convo.ExecuteBlock("Convo1");
                convo.SetBooleanVariable("Convo1Bool", true);
            }
                
        }
    }

}
