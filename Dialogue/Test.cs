using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class Test : MonoBehaviour
{
    public Transform player;
    public float radius = 5f;
    public Flowchart ZFlowchart;
    float zeroMice = 0;


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


    void Update()
    {
        float distance = Vector3.Distance(player.position, transform.position);

        if (distance < radius)
        {
            ZFlowchart.ExecuteBlock("ZFlowchart");
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
