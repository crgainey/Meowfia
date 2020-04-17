using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevelTest : MonoBehaviour
{
    public Transform player;
    public float radius = 5f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.JoystickButton4))
        {
            float distance = Vector3.Distance(player.position, transform.position);
            if (distance < radius)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);//restarts current scene
            }
        }
    }
}
