using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{

    public Camera cam;
    public GameObject laserPoint;
    public GameObject enemyFollowObject;
    public Light pointLight;
    public LineRenderer lineRenderer;
    public float laserRange = 30f;

    public GameObject player;
    Vector3 originalPos;
   
    private void Start()
    {
        originalPos = player.transform.position;
    }

    void FixedUpdate()
    {
            if (Input.GetButton("Fire1"))
            {
                LaserPointer();
            }
        
        else
        {
            lineRenderer.enabled = false;
            pointLight.enabled = false;
            enemyFollowObject.SetActive(false);

            enemyFollowObject.transform.position = originalPos;
            pointLight.transform.position = originalPos;
        }
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
    
}
