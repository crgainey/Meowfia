using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    /*The laser uses the mouse position to determine the endpoint. 
     I restricted the laser to only being used in the first person so 
     the player has more control of where the laser points.*/

    public Camera cam;
    public GameObject laserPoint;
    public GameObject enemyFollowObject;
    public Light pointLight;
    public LineRenderer lineRenderer;
    public float laserRange = 30f;

    //laser charges 
    public int numOfCharges = 3;

    public GameObject player;
    Vector3 originalPos;

    public AudioSource laserSound;

    CameraChange _camChange;

    private void Start()
    {
        originalPos = player.transform.position;

        //Finds Camera Monitor
        GameObject camMonitorObject = GameObject.FindWithTag("CamMonitor");
        if (camMonitorObject != null)
        {
            _camChange = camMonitorObject.GetComponent<CameraChange>();
        }
        if (camMonitorObject == null)
        {
            Debug.Log("Cannot find 'CamChange' script");
        }
    }

    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.LeftShift) && numOfCharges >= 0 || Input.GetKey(KeyCode.Joystick1Button4) && numOfCharges >=0)
        {
            LaserPointer();
        }
        //laser only works when Button is held down 
        else
        {
            lineRenderer.enabled = false;
            pointLight.enabled = false;
            enemyFollowObject.SetActive(false);

            enemyFollowObject.transform.position = originalPos;
            pointLight.transform.position = originalPos;
        } 
        LaserCharges();
    }

    void LaserPointer()
    {
        lineRenderer.SetPosition(0, laserPoint.transform.position);

        RaycastHit hit;
        var mousePos = Input.mousePosition;
        var rayMouse = cam.ScreenPointToRay(mousePos);

        if (Physics.Raycast(rayMouse.origin, rayMouse.direction, out hit, laserRange))
        {
            if (hit.collider)
            {
                lineRenderer.SetPosition(1, hit.point);
                enemyFollowObject.transform.position = hit.point;
                pointLight.transform.position = hit.point;
            }

        }
        // so laser will move even when not colliding
        else
        {
            var pos = rayMouse.GetPoint(laserRange);
            lineRenderer.SetPosition(1, pos);
        }
        lineRenderer.enabled = true;
        pointLight.enabled = true;
        enemyFollowObject.SetActive(true);

    }

    public void LaserCharges()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift)|| Input.GetKeyDown(KeyCode.Joystick1Button4))
        {
            //Debug.Log("Number of Charges" + numOfCharges);
            numOfCharges -= 1;
            laserSound.Play();
            
        }
        else if(Input.GetKeyUp(KeyCode.LeftShift)|| Input.GetKeyUp(KeyCode.Joystick1Button4))
        {
            laserSound.Stop();
        }
        
    }

}
