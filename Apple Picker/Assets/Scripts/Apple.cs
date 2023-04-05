using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apple : MonoBehaviour
{
    public int points;
    public static float bottomY = -20f;

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < bottomY && points > 0) {
            Debug.Log(points);
            Destroy(this.gameObject);

            ApplePicker apScript = Camera.main.GetComponent<ApplePicker>();
            apScript.AppleMissed();
        }
        
    }
}
