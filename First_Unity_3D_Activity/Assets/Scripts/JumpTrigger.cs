using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpTrigger : MonoBehaviour
{
    public float jumpMultiplier = 2f;
    public AudioClip jumpSound = null;

    private bool _isJumping = false;
    private AudioSource audioPlayer = null;
    private PlayerBehavior player = null;

    void Start() {
        audioPlayer = this.GetComponent<AudioSource>();
    }

    void Update()
    {
        _isJumping |= Input.GetKeyDown(KeyCode.J);
    }

    void FixedUpdate()
    {
        if (player != null && _isJumping)
        {
            // The only addition to the PlayerBehavior is making IsGrounded() public,
            // that way it can be called here, preventing playing sound at any time during the jump
            // when J is pressed - now it only plays when player is grounded - right before jumping. 
            bool _isGrounded = player.IsGrounded();
            if (audioPlayer != null && _isGrounded)
            {
                Debug.Log(_isGrounded);
                audioPlayer.PlayOneShot(jumpSound);
                _isJumping = false;
            }
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player") {
            player = col.gameObject.GetComponent<PlayerBehavior>();
            float velocity = col.gameObject.GetComponent<PlayerBehavior>().JumpVelocity;
            velocity *= jumpMultiplier;
            col.gameObject.GetComponent<PlayerBehavior>().JumpVelocity = velocity;
        }   
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            player = null;
            col.gameObject.GetComponent<PlayerBehavior>().JumpVelocity /= jumpMultiplier;
        }
    }
}
