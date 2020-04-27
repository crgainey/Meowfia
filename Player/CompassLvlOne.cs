using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class CompassLvlOne : MonoBehaviour
{
    public Transform player;

    public Vector3 offset;

    public Transform target1, target2, target3;
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

    void Update()
    {
        LookAtTarget1();
        if (convo.GetBooleanVariable("Convo1Bool") == true)
        {
            LookAtTarget2();
        }

        if (_gm.numOfCatnip == 1)
        {
            LookAtTarget1();
        }

        if(convo2.GetBooleanVariable("Convo2Bool") == true)
        {
            LookAtTarget3();
        }
    }

    void LookAtTarget1()
    {
        float desiredYAngle = target1.eulerAngles.y;

        Quaternion rotation = Quaternion.Euler(0, desiredYAngle, 0);
        transform.position = player.position - (rotation * offset);
        transform.LookAt(target1);
    }

    void LookAtTarget2()
    {
        float desiredYAngle = target2.eulerAngles.y;

        Quaternion rotation = Quaternion.Euler(0, desiredYAngle, 0);
        transform.position = player.position - (rotation * offset);
        transform.LookAt(target2);
    }
    void LookAtTarget3()
    {
        float desiredYAngle = target3.eulerAngles.y;

        Quaternion rotation = Quaternion.Euler(0, desiredYAngle, 0);
        transform.position = player.position - (rotation * offset);
        transform.LookAt(target3);
    }

}
