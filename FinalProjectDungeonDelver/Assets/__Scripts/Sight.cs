using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sight : MonoBehaviour
{
    public float distance;
    public float angle;
    public LayerMask objectsLayer;
    public LayerMask obstaclesLayer;
    public Collider2D detectedObject;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, distance, objectsLayer);
        detectedObject = null;
        for (int i = 0; i < colliders.Length; i++)
        {
            Collider2D collider = colliders[i];
            Vector2 directionToController = collider.bounds.center - transform.position;
            directionToController.Normalize();
            float angleToCollider = Vector2.Angle(transform.forward, directionToController);
            if (angleToCollider < angle) {
                if (!Physics2D.Linecast(transform.position, collider.bounds.center, obstaclesLayer)) {
                    detectedObject = collider;
                    break;
                }
            }
        }
    }
}
