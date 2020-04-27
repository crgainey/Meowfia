using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerControllerRB : MonoBehaviour
{
    public Transform thirdPersonCam;
    public GameObject playerModel;
    public float moveSpeed = 20f;
    public float dashDistance = 50f;
    float _rotateSpeed = 10f;
    float _walkAnimSpeed = 1.5f;

    public Rigidbody _rb;

    CameraChange _camChange;

    public Animator anim;
    public ParticleSystem dashParticle;

    bool _canDash = true;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;

        _rb = GetComponent<Rigidbody>();
        //makes sure there is no rotation on the model
        _rb.freezeRotation = true;

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
        if (Input.GetKeyDown(KeyCode.E) && _canDash == true || Input.GetKeyDown(KeyCode.Joystick1Button1) && _canDash == true)
        {
            _rb.AddRelativeForce(Vector3.forward * dashDistance, ForceMode.Impulse);
            dashParticle.Play();
            StartCoroutine(DashTimer());
            _canDash = false;
            anim.SetBool("isDashing", true);
        }
        else
        {
            anim.SetBool("isDashing", false);
        }
        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            anim.speed = _walkAnimSpeed;
            anim.SetBool("isWalking", true);
        }
        else
        {
            anim.SetBool("isWalking", false);
        }
    }

    void FixedUpdate()
    {
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
                playerModel.transform.rotation = Quaternion.Slerp(playerModel.transform.rotation, newRotation, _rotateSpeed * Time.deltaTime);
            }
        }
    }
    IEnumerator DashTimer()
    {
        yield return new WaitForSeconds(3f);
        _canDash = true;
    }


}
