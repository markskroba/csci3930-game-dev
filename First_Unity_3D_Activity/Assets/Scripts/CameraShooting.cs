using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShooting : MonoBehaviour
{
    public Rigidbody Bullet;
    public AudioClip throwSound = null;

    public float BulletSpeed = 1f;
    private bool _isShooting;
    

    void Start()
    {
    }


    void Update()
    {
        _isShooting |= Input.GetKeyDown(KeyCode.Space);
    }

    void FixedUpdate()
    {
        if (_isShooting)
        {
            Rigidbody newBullet = Instantiate(Bullet,
                        this.transform.position + new Vector3(0, -0.5f, 0),
                        this.transform.rotation * this.Bullet.transform.rotation);
            Physics.IgnoreCollision(newBullet.GetComponent<Collider>(), GameObject.Find("Player").GetComponent<Collider>());
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
    }
}
