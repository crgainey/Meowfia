using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerControllerRB : MonoBehaviour
{
    public Transform thirdPersonCam;
    public GameObject playerModel;
    public float moveSpeed = 20f;
    public float dashSpeed = 30f;
    public float rotateSpeed = 10f;

    //jump & DoubleJump
    public float jumpHeight = 35f;
    public float maxJump = 2;
    public float currrentJumps = 0;

    public Transform groundCheck;
    public float groundDistance = 0.4f;//# is radius of the sphere
    public LayerMask groundMask;
    bool _isGrounded = true;

    Rigidbody _rb;

    CameraChange _camChange;

    public AudioSource jumpLandSound;

    public Animator anim;

    //[SerializeField] ParticleSystem jumpParticle;
    //[SerializeField] ParticleSystem dashParticle;
    
    void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;

        _rb = GetComponent<Rigidbody>();
        //makes sure there is no rotation on the model
        _rb.freezeRotation = true;
        Physics.gravity = new Vector3(0, Physics.gravity.y, 0);

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
        if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Joystick1Button1))
        {
            StartCoroutine("Dash");
        }

    }

    void FixedUpdate()
    {
        Jump();
        Movement();
    }

    void Movement()
    {
        //basic movement
        Vector3 move = (transform.forward * Input.GetAxis("Vertical")) + (transform.right * Input.GetAxis("Horizontal"));
        move = move.normalized * moveSpeed; //helps smooth the movement
        _rb.MovePosition(_rb.position + move * Time.deltaTime);

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
        _isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (Input.GetButtonDown("Jump") && (_isGrounded || maxJump > currrentJumps))
        {
            currrentJumps++;
            _rb.AddForce(Vector3.up * jumpHeight, ForceMode.Impulse);
            _isGrounded = false;
            //jumpParticle.Play();
            
        }

        Invoke("jumpReset", 1f);
        Debug.Log(Physics.gravity);
    }

    private void jumpReset()
    {
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
        //dashParticle.Play();
    }
}
