using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;
public class CatnipPickUp : MonoBehaviour
{
    public Transform player;
    public float radius = 5f;
    public Flowchart catnipConvo;
    public Flowchart ZToInteract;
    float _catnipGive = 1;

    GameManager _gameManager;

    void Start()
    {
        //Finds the game manager script
        GameObject gameManagerObject = GameObject.FindWithTag("GameController");
        if (gameManagerObject != null)
        {
            _gameManager = gameManagerObject.GetComponent<GameManager>();
        }
        if (gameManagerObject == null)
        {
            Debug.Log("Cannot find 'GameManager' script");
        }

    }

    void Update()
    {
        float distance = Vector3.Distance(player.position, transform.position);
        if (distance < radius)
        {
            ZToInteract.ExecuteBlock("ZToInteract");

        }

        if (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.JoystickButton4))
        {
            if(_gameManager.numOfCatnip == 1)
            {
                return;
            }

            if (distance < radius)
            {
                catnipConvo.ExecuteBlock("TheNip");
                //Debug.Log("Got The NIPP");
                _gameManager.UpdateCatnip(_catnipGive);
            }
            
        }
    }

}
