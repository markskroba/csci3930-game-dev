using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyTeleport : MonoBehaviour
{
    public GameObject teleportTarget = null;
    public AudioClip teleportAudio = null;
    private Vector3 targetPos;
    private Quaternion targetRot;

    // Start is called before the first frame update
    void Start()
    {
        targetPos = teleportTarget.transform.position;
        targetRot = teleportTarget.transform.rotation;
    }

    // Update is called once per frame
    void OnTriggerEnter(Collision col)
    {
        if ((teleportTarget != null) && (col.gameObject.tag == "Player")) {
            col.transform.position = targetPos;
            col.transform.rotation = targetRot;

            if (teleportAudio != null) {
                AudioSource audioPlayer = col.gameObject.GetComponent<AudioSource>();
                if (audioPlayer != null) {
                    audioPlayer.PlayOneShot(teleportAudio);
                }
            }
        }
    }
}
