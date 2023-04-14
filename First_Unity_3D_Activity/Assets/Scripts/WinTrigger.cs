using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinTrigger : MonoBehaviour
{
    public TMPro.TMP_Text winText = null;
    public string message = "You Win!";
    public AudioClip sound = null;
    public bool autoRestart = true;
    public float restartDelay = 5.0f;


    private double timer = 0f;
    private bool playerWon = false;
    private AudioSource audioPlayer = null;

    // Start is called before the first frame update
    void Start()
    {
        playerWon = false;
        timer = 0f;
        audioPlayer = this.GetComponent<AudioSource>();

        if (winText != null)
        {
            winText.SetText("");
        }
        else {
            Debug.Log("Need TMPro Text box to display message");
        }

    }

    // Update is called once per frame
    void Update()
    {
        timer = Time.deltaTime;

        if (autoRestart && playerWon && (timer > restartDelay)) {
            timer = 0;
            playerWon = false;

            // restart the scene
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (!playerWon && (col.gameObject.tag == "Player")) {
            Debug.Log(message);
            winText.SetText(message);

            playerWon = true;
            timer = 0f;

            if (audioPlayer != null && sound != null) {
                audioPlayer.PlayOneShot(sound);
            }
        }
    }
}
