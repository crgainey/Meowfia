using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    //Used for the First Person Camera

    public float mouseSensitivity = 100f; //moveSpeed that the mouse moves to look

    public Transform player;

    float _xRotation = 0f;
    void Start()
    {
        //Locks cursor on screen and hids it
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        
        //moves camera on the correcsponding axis
        //mouse x and y are build into unitys input system
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        //allows us to limit how much the playerOrientation can see on the axis
        _xRotation -= mouseY;
        _xRotation = Mathf.Clamp(_xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(_xRotation, 0f, 0f);
        //Roataes the playerOrientation to move with camera
        player.Rotate(Vector3.up * mouseX);
        
    }
}
