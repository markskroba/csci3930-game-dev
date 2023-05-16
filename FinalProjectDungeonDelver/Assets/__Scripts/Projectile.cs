using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public GameObject shooterPrefab;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (shooterPrefab != null && !collision.gameObject.name.Contains(shooterPrefab.name) && collision.gameObject.name != "Sword")
        {
            Destroy(this.gameObject);
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
