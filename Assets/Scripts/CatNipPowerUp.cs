using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatNipPowerUp : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(Pickup(other));
        }
    }

    IEnumerator Pickup(Collider player)
    {
        Debug.Log("Picked Up");
        PlayerControllerRB playerSpeed = player.GetComponent<PlayerControllerRB>();
        playerSpeed.moveSpeed += 50f;

        yield return new WaitForSeconds(5f);

        //returns moveSpeed to normal
        playerSpeed.moveSpeed -= 50f;
        Destroy(gameObject);
    }
}
