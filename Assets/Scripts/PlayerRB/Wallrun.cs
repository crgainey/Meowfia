using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wallrun : MonoBehaviour
{
    private float moveSpeed = 6;
    private float turnSpeed = 90;

    private float smoothSpeed = 10; //smoothing speed
    private float gravity = 10; //gravity acceleration
    private bool isGrounded;
    private float deltaGround = 0.2f; //character is grounded up to this distance

    private float jumpSpeed = 10;
    private float jumpRange = 10; //range to detect thirdPersonCam wall

    private Vector3 surfaceNormal; //current surface normal

    private Vector3 myNormal;
    private float distGround; //distance from character position to the ground
    private bool jumping = false; //jumping to wall or not
    private float vertSpeed = 0; // vertical jump current speed

    private Transform myTransform;
    public BoxCollider boxCollider;
    public Rigidbody rB;

    private void Start()
    {
        myNormal = transform.up;
        myTransform = transform;
        rB.freezeRotation = true; //disable physics rotation
        distGround = boxCollider.size.y - boxCollider.center.y;

    }

    private void FixedUpdate()
    {
        //apply constant weight force according to character normal
        rB.AddForce(-gravity * rB.mass * myNormal);
    }

    private void Update()
    {
        //jump code - jump to wall or simple jump
        if (jumping) return; //abort update while jumping to a wall

        Ray ray;
        RaycastHit hit;

        if (Input.GetButtonDown("Jump"))
        {
            ray = new Ray(myTransform.position, myTransform.forward);

            if (Physics.Raycast(ray, out hit, jumpRange))
            {
                JumpToWall(hit.point, hit.normal);
            }
            else if (isGrounded)
            {
                rB.velocity += jumpSpeed * myNormal;
            }
        }



        myTransform.Rotate(0, Input.GetAxis("Horizontal") * turnSpeed * Time.deltaTime, 0);

        // update surface normal and isGrounded
        ray = new Ray(myTransform.position, -myNormal); //casts ray downards

        if (Physics.Raycast(ray, out hit))
        {
            isGrounded = hit.distance <= distGround + deltaGround;
            surfaceNormal = hit.normal;
        } else
        {
            isGrounded = false;
            surfaceNormal = Vector3.up;
        }
        myNormal = Vector3.Lerp(myNormal, surfaceNormal, smoothSpeed * Time.deltaTime);

        //find forward direction with new myNormal
        Vector3 myForward = Vector3.Cross(myTransform.right, myNormal);

        //align character to new myNormal while keeping the forward direction
        Quaternion targetRot = Quaternion.LookRotation(myForward, myNormal);
        myTransform.rotation = Quaternion.Lerp(myTransform.rotation, targetRot, smoothSpeed * Time.deltaTime);

        //move character back/forth with Verctical axis
        myTransform.Translate(0, 0, Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime);

    }

    private void JumpToWall(Vector3 point, Vector3 normal)
    {
        jumping = true;
        rB.isKinematic = true;
        Vector3 orgPos = myTransform.position;
        Quaternion orgRot = myTransform.rotation;
        Vector3 dstPos = point + normal * (distGround + 0.5f); //will jump to 0.5 above wall
        Vector3 myForward = Vector3.Cross(myTransform.right, normal);
        Quaternion dstRot = Quaternion.LookRotation(myForward, normal);

        StartCoroutine(jumpTime(orgPos, orgRot, dstPos, dstRot, normal));
    }

    private IEnumerator jumpTime(Vector3 orgPos, Quaternion orgRot, Vector3 dstPos, Quaternion dstRot, Vector3 normal)
    {
        for (float t = 0.0f; t < 1.0f;)
        {
            t += Time.deltaTime;
            myTransform.position = Vector3.Lerp(orgPos, dstPos, t);
            myTransform.rotation = Quaternion.Slerp(orgRot, dstRot, t);
            yield return null;
        }
        myNormal = normal;
        rB.isKinematic = false;
        jumping = false;
    }

 



}
