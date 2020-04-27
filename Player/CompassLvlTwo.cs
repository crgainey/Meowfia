using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompassLvlTwo : MonoBehaviour
{
    public Transform player;

    public Vector3 offset;

    public Transform target, target2;

    // Start is called before the first frame update
    void Update()
    {
        LookAtTarget();
    }

    void LookAtTarget()
    {
        float desiredYAngle = target.eulerAngles.y;

        Quaternion rotation = Quaternion.Euler(0, desiredYAngle, 0);
        transform.position = player.position - (rotation * offset);
        transform.LookAt(target);
    }

    void LookAtTarget2()
    {
        float desiredYAngle = target2.eulerAngles.y;

        Quaternion rotation = Quaternion.Euler(0, desiredYAngle, 0);
        transform.position = player.position - (rotation * offset);
        transform.LookAt(target2);
    }
}
