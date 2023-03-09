using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickBehavior : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Bullet(Clone)")
        {
            Destroy(this.transform.gameObject);
            Destroy(collision.gameObject);
            Debug.Log("Brick destroyed!");
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
