using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boid : MonoBehaviour
{
    private Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        vel = Random.onUnitSphere * Spawner.SETTINGS.velocity;


    }

    void LookAhead() {
        transform.LookAt(pos + rb.velocity);
    }

    void Colorize() {
        Color randColor = Random.ColorHSV(0, 1, .5f, 1, .5f, 1);
        Renderer[] rends = gameObject.GetComponentsInChildren<Renderer>();
        foreach (Renderer r in rends) {
            r.material.color = randColor;        
        }

        TrailRenderer trend = GetComponent<TrailRenderer>();
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
