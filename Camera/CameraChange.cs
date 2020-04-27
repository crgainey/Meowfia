using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraChange : MonoBehaviour
{
    //changes between the camera
    public GameObject thirdCam;
    public GameObject firstCam;
    public int camMode;

    private void LateUpdate()
    {
        if(Input.GetKeyDown(KeyCode.LeftShift)|| Input.GetKeyDown(KeyCode.Joystick1Button4))
        {
            if(camMode == 1)
            {
                camMode = 0;
            }
            else
            {
                camMode += 1;
            }
        }

        if (Input.GetKeyUp(KeyCode.LeftShift)|| Input.GetKeyUp(KeyCode.Joystick1Button4))
        {
            if (camMode == 1)
            {
                camMode = 0;
            }
            else
            {
                camMode += 1;
            }
        }
        StartCoroutine(CamChange());
    }

    IEnumerator CamChange()
    {
        yield return new WaitForSeconds(0.01f);
        if( camMode == 0)
        {
            thirdCam.SetActive(true);
            firstCam.SetActive(false);
        }

        if(camMode == 1)
        {
            firstCam.SetActive(true);
            thirdCam.SetActive(false);
        }
    }
}
