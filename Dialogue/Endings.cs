using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class Endings : MonoBehaviour
{
    public Flowchart Good;
    public Flowchart Bad;

    GameManager _gameManager;

    // Start is called before the first frame update
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

    // Update is called once per frame
    void Update()
    {
        if (Good.GetBooleanVariable("Good") == true)
        {
            _gameManager.GoodEnding();
        }

        if (Bad.GetBooleanVariable("Badd") == true)
        {
            _gameManager.BadEnding();
        }
    }
}
