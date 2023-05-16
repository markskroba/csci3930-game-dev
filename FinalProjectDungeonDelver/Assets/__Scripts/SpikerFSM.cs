using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikerFSM : EnemyFSM
{
    new public enum EnemyState { Attack, Rest };
    public EnemyState spikerCurrentState;

    public GameObject projectile;
    public float projectileDelay = 2;
    private float currentProjectileDelay = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (spikerCurrentState == EnemyState.Rest) { Rest(); }
        else { Attack(); }
    }

    void Rest() {
        if (sightSensor.detectedObject != null) {
            print("State changing to attack");
            spikerCurrentState = EnemyState.Attack;
        }
        return;
    }

    void Attack() {
        if (sightSensor.detectedObject == null) {
            print("State changing to rest");
            spikerCurrentState = EnemyState.Rest;
            return;
        }
        if (currentProjectileDelay == 0)
        {
            Vector3 dest = sightSensor.detectedObject.transform.position;
            GameObject projectileGO = Instantiate<GameObject>(projectile);
            Rigidbody2D projectileRB = projectileGO.GetComponent<Rigidbody2D>();

            projectileGO.transform.position = transform.position;
            Vector3 dir = (dest - transform.position).normalized;

            projectileRB.velocity = dir * 4;
            print("spawning sword");
            currentProjectileDelay = Time.time + projectileDelay;
        }

        if (Time.time >= currentProjectileDelay) {
            currentProjectileDelay = 0;
        }
        return;
    }

}
