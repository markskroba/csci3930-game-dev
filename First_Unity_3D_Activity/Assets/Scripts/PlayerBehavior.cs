using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    //public GameObject Bullet;
    public Rigidbody Bullet;
    public float BulletSpeed = 100f;
    private bool _isShooting;

    public AudioClip throwSound = null;

    public float MoveSpeed = 10f;
    public float RotateSpeed = 75f;
    public float _vInput;
    public float _hInput;

    public float JumpVelocity = 5f;
    public bool _isJumping;

    public float DistanceToGround = 0.1f;
    public LayerMask GroundLayer;
    private CapsuleCollider _col;

    private Rigidbody _rb;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _col = GetComponent<CapsuleCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        _isJumping |= Input.GetKeyDown(KeyCode.J);
        _isShooting |= Input.GetKeyDown(KeyCode.Space);
        _vInput = Input.GetAxis("Vertical") * MoveSpeed;
        _hInput = Input.GetAxis("Horizontal") * RotateSpeed;
        //this.transform.Translate(Vector3.forward * _vInput * Time.deltaTime);
        //this.transform.Rotate(Vector3.up * _hInput * Time.deltaTime);
    }

    void FixedUpdate()
    {
        if (_isShooting)
        {
            Rigidbody newBullet = Instantiate(Bullet, this.transform.position + new Vector3(0, 0.75f, 0), this.transform.rotation * this.Bullet.transform.rotation);
       
            //Rigidbody BulletRB = newBullet.GetComponent<Rigidbody>();
            Physics.IgnoreCollision(newBullet.GetComponent<Collider>(), this.gameObject.GetComponent<Collider>());
            newBullet.velocity = this.transform.forward * BulletSpeed;

            if (throwSound)
            {
                AudioSource audioPlayer = newBullet.GetComponent<AudioSource>();
                if (audioPlayer != null)
                    audioPlayer.PlayOneShot(throwSound);
                else
                    Debug.Log("Your " + newBullet.gameObject.name + " prefab must have an audio source.");
            }
        }
        _isShooting = false;

        if (IsGrounded() && _isJumping)
        {
            _rb.AddForce(Vector3.up * JumpVelocity, ForceMode.Impulse);
        }
        _isJumping = false;
        Vector3 rotation = Vector3.up * _hInput;
        Quaternion angleRot = Quaternion.Euler(rotation * Time.fixedDeltaTime);
        _rb.MovePosition(this.transform.position + this.transform.forward * _vInput * Time.fixedDeltaTime);
        _rb.MoveRotation(_rb.rotation * angleRot);
    }

    public bool IsGrounded()
    {
        Vector3 capsuleBottom = new Vector3(_col.bounds.center.x,
            _col.bounds.min.y, _col.bounds.center.z);
        bool grounded = Physics.CheckCapsule(_col.bounds.center,
            capsuleBottom, DistanceToGround, GroundLayer,
            QueryTriggerInteraction.Ignore);

        return grounded;
    }
}
