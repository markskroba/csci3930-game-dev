using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFSM : MonoBehaviour
{
    public enum EnemyState { Attack, Patrol, Chase, Flee, AngeredPatrol };
    public EnemyState currentState;
    public Sight sightSensor;
    public float playerAttackDistance;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (currentState == EnemyState.Patrol) { Patrol(); }
        else if (currentState == EnemyState.Chase) { Chase(); }
        else { Attack(); }
    }

    void Attack() {
        print(sightSensor.detectedObject);
        if (sightSensor.detectedObject == null) {
            print("State changing to patrol");
            currentState = EnemyState.Patrol;
            return;
        }

        float distanceToPlayer = Vector2.Distance(transform.position, sightSensor.detectedObject.transform.position);
        if (distanceToPlayer >= playerAttackDistance * 1.1f) {
            currentState = EnemyState.Chase;
        }
    }
    void Chase() {
        if (sightSensor.detectedObject == null) {
            print("State changing to patrol");
            currentState = EnemyState.Patrol;
            return;
        }

        float distanceToPlayer = Vector2.Distance(transform.position, sightSensor.detectedObject.transform.position);
        if (distanceToPlayer <= playerAttackDistance) {
            currentState = EnemyState.Attack;
        }
    }
    void Patrol() {
        if (sightSensor.detectedObject != null) {
            print("State changing to chase");
            currentState = EnemyState.Chase;
        }
    }
}
