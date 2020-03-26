using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerRB : MonoBehaviour
{
    public float moveSpeed = 20f;
    public float dashSpeed = 30f;

    //jump & DoubleJump
    public float jumpHeight = 50f;
    public float maxJump = 2;
    public float currrentJumps = 0;

    public Transform groundCheck;
    public float groundDistance = 0.4f;//# is radius of the sphere
    public LayerMask groundMask;
    bool _isGrounded = true;

    Rigidbody _rb;

    float _gravityMultiplier = 5f;


    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        //makes sure there is no rotation on the model
        _rb.freezeRotation = true;
        Physics.gravity = new Vector3(0, Physics.gravity.y * _gravityMultiplier);
    }


    void Update()
    {
        Jump();

        if (Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine("Dash");
        }
    }

    void FixedUpdate()
    {
        _isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        //basic movement
        Vector3 move = (transform.forward * Input.GetAxis("Vertical")) + (transform.right * Input.GetAxis("Horizontal"));
        move = move.normalized * moveSpeed; //helps smooth the movement
        _rb.MovePosition(_rb.position + move *Time.deltaTime);
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump") && (_isGrounded || maxJump > currrentJumps))
        {

            _rb.AddForce(Vector3.up * jumpHeight, ForceMode.Impulse);
            _isGrounded = false;
            currrentJumps++;
        }

        if (_isGrounded == true)
        {
            currrentJumps = 0;//resets current jumps
        }
    }
    IEnumerator Dash()
    {
        moveSpeed += dashSpeed;
        yield return new WaitForSeconds(.3f);
        moveSpeed -= dashSpeed;
    }

}
