using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wallrun : MonoBehaviour
{
    private bool isWallR = false;
    private bool isWallL = false;
    private RaycastHit hitR;
    private RaycastHit hitL;
    private int jumpCount = 0;
    private PlayerControllerRB cc;
    private Rigidbody rb;





    // Start is called before the first frame update
    void Start()
    {
        cc = GetComponent<PlayerControllerRB>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            RightWallRun();
            LeftWallRun();
        }
    }

    private void RightWallRun()
    {
        if (Physics.Raycast(transform.position, transform.right, out hitR, 1))
        {
            if(hitR.transform.tag == "wall")
            {
                isWallR = true;
                isWallL = false;
                jumpCount += 1;
                rb.useGravity = false;
                StartCoroutine(afterRun());
            }
        }
    }

    private void LeftWallRun()
    {
        if (Physics.Raycast(transform.position, -transform.right, out hitL, 1))
        {
            if(hitL.transform.tag == "wall")
            {
                isWallL = true;
                isWallR = false;
                jumpCount += 1;
                rb.useGravity = false;
                StartCoroutine(afterRun());
            }
        }
    }

    IEnumerator afterRun()
    {
        yield return new WaitForSeconds(0.5f);
        isWallL = false;
        isWallR = false;
        rb.useGravity = true;
    }
}
