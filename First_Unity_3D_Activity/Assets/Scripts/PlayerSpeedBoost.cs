using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpeedBoost : MonoBehaviour
{
    private float timer;
    private bool superFast = false;
    private float originalSpeed = 6f;
    private float boostDuration;
    private AudioSource audioPlayer = null;
    private string playerName = "";
    private PlayerBehaviorShooting script = null;

    // Start is called before the first frame update
    void Start()
    {
        timer = 0f;
        audioPlayer = this.GetComponent<AudioSource>();
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null) script = playerObject.GetComponentInChildren<PlayerBehaviorShooting>();
        if (script != null && playerObject != null) {
            playerName = playerObject.name;
            originalSpeed = script.MoveSpeed;
        }
    }

    // Update is called once per frame
    void Update()
    {
        timer = Time.deltaTime;

        if (script != null && superFast && (timer > boostDuration))
        {
            superFast = false;
            script.MoveSpeed = originalSpeed;
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        Collider col = collision.collider;
        if (col.gameObject.tag == "CollisionObject")
        {
            boostSpeed(col);
        }
    }

    public void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "CollisionObject")
        {
            boostSpeed(col);
        }
    }

    private void boostSpeed(Collider col)
    {
        if (!script) return;
        AudioClip playSound = null;
        ObjectSpeedBoost objSpeedBoost = col.gameObject.GetComponent<ObjectSpeedBoost>();
        if (objSpeedBoost != null) {
            Debug.Log("boosting speed ...");
            timer = 0f;
            float speedMultiplier = objSpeedBoost.speedMultiplier;
            boostDuration = objSpeedBoost.speedBoostDuration;
            superFast = true;
            script.MoveSpeed = script.MoveSpeed * speedMultiplier;

            if (objSpeedBoost.destroyOnCollision) {
                Destroy(col.gameObject);
            }

            playSound = objSpeedBoost.speedBoostSound;

        }

        if (playSound != null) {
            if (audioPlayer != null) audioPlayer.PlayOneShot(playSound);
        }
    }
}
