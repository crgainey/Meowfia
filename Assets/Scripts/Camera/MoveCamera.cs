using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    //This is for the Third Person Camera
    public Transform player,target;

    public Vector3 offset;

    public float rotateSpeed;

    float _xRotation;
    void Start()
    {
        //Locks cursor on screen and hids it
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

    }

    void Update()
    {
        //this rotates the player based off the mouse
        float mouseX = Input.GetAxis("Mouse X") * rotateSpeed;
        player.Rotate(0, mouseX, 0);

        float mouseY = Input.GetAxis("Mouse Y") * rotateSpeed;
        target.Rotate(-mouseY, 0, 0);

        float desiredYAngle = player.eulerAngles.y;
        float desiredXAngle = target.eulerAngles.x;

        Quaternion rotation = Quaternion.Euler(desiredXAngle, desiredYAngle, 0);
        transform.position = player.position - (rotation * offset);

        if(transform.position.y < player.position.y)
        {
            transform.position = new Vector3(transform.position.x, player.position.y, transform.position.z);
        }

        transform.LookAt(player);

        if (Input.GetKey("escape"))
            Application.Quit();
    }
   

}
