using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatnipPickUp : MonoBehaviour
{
    public Transform player;
    public float radius = 5f;

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
        if (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.JoystickButton4))
        {
            float distance = Vector3.Distance(player.position, transform.position);
            if (distance < radius)
            {
                Debug.Log("Got The NIPP");
                _gameManager.UpdateCatnip();
            }
        }
    }

}
