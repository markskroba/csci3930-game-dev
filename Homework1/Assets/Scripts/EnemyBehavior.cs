using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    private Transform _target;
    public float MoveSpeed = 2f;
    private Rigidbody _rb;

    void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player")
        {
            Debug.Log("Player detected");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.name == "Player")
        {
            Debug.Log("Player out of range");
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _target = GameObject.Find("Player").transform;
        
    }

    // Update is called once per frame
    void Update()
    {
        _rb.MovePosition(this.transform.position + this.transform.forward * MoveSpeed * Time.fixedDeltaTime);
    }

    void LateUpdate()
    {
        this.transform.LookAt(_target);
    }
}
