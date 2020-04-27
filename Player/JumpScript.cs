using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpScript : MonoBehaviour
{
    public float jumpHeight = 30f;
    public float maxJump = 2;
    public float currrentJumps = 0;

    public Transform groundCheck;
    public float groundDistance = 0.4f;//# is _radius of the sphere
    public LayerMask groundMask;
    bool _isGrounded = true;
    
    public Animator anim;
    Rigidbody _rb;

    public AudioSource jumpSound;
    public ParticleSystem jumpParticle;
   
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        _isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        
        if (Input.GetButtonDown("Jump") && (maxJump > currrentJumps))
        {
            currrentJumps++;
            Jump();
            jumpSound.Play();
            jumpParticle.Play();
        }
      
        if(currrentJumps == 2 && _isGrounded == true)
        {
            StartCoroutine(JumpReset());
        }

        //ANIMATIONS
        if (Input.GetButtonDown("Jump"))
        {
            anim.SetBool("isJumping", true);
        }
        else
        {
            anim.SetBool("isJumping", false);
        }
       
    }

    void Jump()
    {
        _rb.AddForce(Vector3.up * jumpHeight, ForceMode.Impulse);
        _isGrounded = false;
        //Debug.Log("Jumps = " + currrentJumps);

    }

    IEnumerator JumpReset()
    {
        yield return new WaitForSeconds(0.1f);
            currrentJumps = 0;//resets current jumps
        //Debug.Log("ResetJumps");
        
    }
}
