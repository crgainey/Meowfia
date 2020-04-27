using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public Transform thirdPersonCam;
    public GameObject playerModel;

    public float moveSpeed = 10f;
    public float dashSpeed = 30;
    public float jumpHeight = 15;
    public float maxJump = 2;
    public float currrentJumps = 0;
    public float rotateSpeed = 10f;

    float _gravity = -50f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;//# is _radius of the sphere
    public LayerMask groundMask;

    Vector3 _velocity;
    bool _isGrounded;

    public CharacterController controller;

    CameraChange _camChange;
    void Start()
    {

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
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine("Dash");
        }
    }
    void FixedUpdate()
    {
        Movement();
        Jump();
    }

    void Movement()
    {
        //moves the player bases on  where the mouse is looking.
        Vector3 move = (transform.forward * Input.GetAxis("Vertical")) + (transform.right * Input.GetAxis("Horizontal"));
        move = move.normalized * moveSpeed; //helps smooth the movement
        controller.Move(move * Time.deltaTime);

        if (_camChange.camMode == 0)
        {
            //moves the player based on camera look
            if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
            {
                transform.rotation = Quaternion.Euler(0f, thirdPersonCam.rotation.eulerAngles.y, 0f);
                Quaternion newRotation = Quaternion.LookRotation(new Vector3(move.x, 0f, move.z));
                playerModel.transform.rotation = Quaternion.Slerp(playerModel.transform.rotation, newRotation, rotateSpeed * Time.deltaTime);
            }
        }
    }

    void Jump()
    {
        //checks in sphere to see if we hit the ground mask and tells if isGrounded true or false
        _isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (_isGrounded && _velocity.y < 0)
        {
            _velocity.y = -2f;
        }

        if (Input.GetButtonDown("Jump") && (_isGrounded || maxJump > currrentJumps))
        {
            _velocity.y = Mathf.Sqrt(jumpHeight * -2f * _gravity);
            _isGrounded = false;
            currrentJumps++;
        }

        if(_isGrounded == true)
        {
            //_isGrounded = true;
            currrentJumps = 0;//resets current jumps
        }
        _velocity.y += _gravity * Time.deltaTime;
        controller.Move(_velocity * Time.deltaTime);
    }
    IEnumerator Dash()
    {
        moveSpeed += dashSpeed;
        yield return new WaitForSeconds(.3f);
        moveSpeed -= dashSpeed;
    }


}
