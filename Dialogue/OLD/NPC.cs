using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public Transform player;
    public float radius = 5f;

    public Dialogue dialogue;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.JoystickButton4))
        {
            float distance = Vector3.Distance(player.position, transform.position);
            if (distance < radius)
            {
               TriggerDialogue();
            }
        }
    }

    public void TriggerDialogue()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }
}
