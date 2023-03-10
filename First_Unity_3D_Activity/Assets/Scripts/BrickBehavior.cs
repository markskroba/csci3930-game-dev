using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickBehavior : MonoBehaviour
{
    public Rigidbody Bullet;

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.name);
        if (collision.gameObject.name == Bullet.gameObject.name + "(Clone)")
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
