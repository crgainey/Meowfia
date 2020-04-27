using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class KittyDialogue : MonoBehaviour
{
    public Transform player;
    public float radius = 5f;
    float zeroMice = 0;
    public Flowchart Convo1;

    public GameObject level3Door;

    GameManager _gm;

    void Start()
    {
        level3Door.SetActive(false);

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

    void Update()
    {
        float distance = Vector3.Distance(player.position, transform.position);
        
        if (distance < radius)
        {
            Convo1.ExecuteBlock("Convo");
            level3Door.SetActive(true);
            CatnipUpdate();

        }
        
    }

    void CatnipUpdate()
    {
        Debug.Log("CatnipUpdate");
        _gm.UpdateCatnip(_gm.numOfMice);
        _gm.ResetMice();

    }
}
