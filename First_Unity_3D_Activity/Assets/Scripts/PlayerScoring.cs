using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScoring : MonoBehaviour
{
    public TMPro.TMP_Text winText = null;

    private int score = 0;
    private AudioSource audioPlayer = null;

    void Start()
    {
        audioPlayer = this.GetComponent<AudioSource>();
        if (winText != null)
        {
            winText.SetText("Score: " + score.ToString());
        }
        else
        {
            Debug.Log("Need TMPro Text box to display message");
        }
    }

    void OnTriggerEnter(Collider col)
    {
        // this requires a tag of ObjectScoring on the collectable objects
        if (col.gameObject.tag == "ObjectScoring")
        {
            AudioClip collectSound = col.gameObject.GetComponent<ObjectScoring>().collectSound;
            bool destroyOnCollision = col.gameObject.GetComponent<ObjectScoring>().destroyOnCollision;

            score += col.gameObject.GetComponent<ObjectScoring>().value;
            winText.SetText("Score: " + score.ToString());

            if (audioPlayer != null && collectSound != null)
            {
                audioPlayer.PlayOneShot(collectSound);
            }

            if (destroyOnCollision) Destroy(col.gameObject);
        }
    }
}
