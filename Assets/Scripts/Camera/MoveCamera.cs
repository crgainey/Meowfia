using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    //This is for the Third Person Camera

    public Transform player,target,cam;
    public Vector3 offset;

    public LayerMask wall;

    public float rotateSpeed = 2;

    void Start()
    {
        //Locks cursor on screen and hids it
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        //moves the thirdPersonCam and attaches to the player. this is so the player can move withouit the camera freaking out
        target.transform.position = player.transform.position;
        target.transform.parent = null;// this stops the camera from spinning around the player because the thirdPersonCam is on the camera object
    }

    void LateUpdate()
    {
        //this rotates the player based off the mouse
        float mouseX = Input.GetAxis("Mouse X") * rotateSpeed;
        target.Rotate(0, mouseX, 0);

        float mouseY = Input.GetAxis("Mouse Y") * rotateSpeed;
        target.Rotate(-mouseY, 0, 0);
    
        //Limit camera rotation
        if (target.rotation.eulerAngles.x > 45f && target.rotation.eulerAngles.x < 180f)
        {
            target.rotation = Quaternion.Euler(45f, 0, 0);
        }

        if(target.rotation.eulerAngles.x > 180f && target.rotation.eulerAngles.x < 315)
        {
            target.rotation = Quaternion.Euler(315f, 0f, 0f);
        }
        

        float desiredYAngle = target.eulerAngles.y;
        float desiredXAngle = target.eulerAngles.x;

        Quaternion rotation = Quaternion.Euler(desiredXAngle, desiredYAngle, 0);
        transform.position = player.position - (rotation * offset);

        if(transform.position.y < player.position.y)
        {
            transform.position = new Vector3(transform.position.x, player.position.y, transform.position.z);
        }

        transform.LookAt(player);

        /*shoots rays out and will move the camera to the point where it hits so its not going through objects
        RaycastHit hit;
        if (Physics.Linecast(thirdPersonCam.transform.position, cam.transform.position, out hit))
        {
            cam.transform.position = hit.point;
        }*/
        

        if (Input.GetKey("escape"))
            Application.Quit();
    }
   

}
