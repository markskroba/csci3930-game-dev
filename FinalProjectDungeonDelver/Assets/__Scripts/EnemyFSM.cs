using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFSM : MonoBehaviour
{
    public enum EnemyState { Attack, Patrol, Chase, Flee, AngeredPatrol };
    public EnemyState currentState;
    public Sight sightSensor;
    public float playerAttackDistance;

    public enum PatrolDirectionAxis  { X, Y };
    public PatrolDirectionAxis currentPatrolDirectionAxis;
    public Vector2 currentPatrolDirection;
    public float timeNextDirectionReversal = 0;
    public float patrolDuration;

    public float FleeTime = 3f;
    public float currentFleeTime = 0;
    public float originalSightDistance;

    private Enemy e;
    private Rigidbody2D rb;

    //private GameObject parent;
    // Start is called before the first frame update
    void Start()
    {
    }

    private void Awake()
    {
        e = this.GetComponentInParent<Enemy>();
        rb = this.GetComponentInParent<Rigidbody2D>();

        if (currentPatrolDirectionAxis == 0) currentPatrolDirection = Vector2.right;
        else currentPatrolDirection = Vector2.down;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentState == EnemyState.Patrol) { Patrol(); }
        else if (currentState == EnemyState.Chase) { Chase(); }
        else if (currentState == EnemyState.Flee) { Flee(); }
        else if (currentState == EnemyState.AngeredPatrol) { AngeredPatrol(); }
        else { Attack(); }
    }

    void Attack() {
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
        if (sightSensor.detectedObject == null)
        {
            print("State changing to patrol");
            currentState = EnemyState.Patrol;
            return;
        }
        else { 
            // actual movement: find a vector from enemy to player
            Vector3 dest = sightSensor.detectedObject.transform.position;
            GameObject parent = this.transform.parent.gameObject;
            parent.transform.position += (dest - parent.transform.position).normalized * parent.GetComponent<Skeletos>().speed * Time.deltaTime;
            rb.velocity = (dest - parent.transform.position) * parent.GetComponent<Skeletos>().speed * Time.deltaTime;
        }

        float distanceToPlayer = Vector2.Distance(transform.position, sightSensor.detectedObject.transform.position);
        if (distanceToPlayer <= playerAttackDistance) {
            currentState = EnemyState.Attack;
        }
    }
    void Patrol() {
        if (sightSensor.detectedObject != null)
        {
            print("State changing to chase");
            currentState = EnemyState.Chase;
        }
        else 
        {
            if (Time.time >= timeNextDirectionReversal) {
                reversePatrolDirection();
            }
            rb.velocity = currentPatrolDirection * patrolDuration;
        }
    }

    void reversePatrolDirection()
    {
        if (currentPatrolDirection == Vector2.down)  currentPatrolDirection = Vector2.up;
        else if (currentPatrolDirection == Vector2.up) currentPatrolDirection = Vector2.down;
        else if (currentPatrolDirection == Vector2.right) currentPatrolDirection = Vector2.left;
        else currentPatrolDirection = Vector2.right;

        timeNextDirectionReversal = Time.time + 2;
    }

    public void decideOnReceivingDamage() {
        float chance = Random.Range(0, 2);
        print(chance);
        if (chance <= .5f)
        {
            print("State changing to Flee");
            originalSightDistance = sightSensor.distance;
            print("Values saved as " + originalSightDistance);
            sightSensor.distance = 100;
            if (currentFleeTime == 0)
            {
                currentFleeTime = Time.time + FleeTime;
                print("new flee time: " + currentFleeTime);
            }
            currentState = EnemyState.Flee;
        }
        else
        {
            print("State changing to Angered Patrol");
            originalSightDistance = sightSensor.distance;
            sightSensor.distance = (float) (sightSensor.distance * 1.5);
            currentState = EnemyState.AngeredPatrol;
        }
    }

    void Flee() {
        Vector3 dest = sightSensor.detectedObject.transform.position;
        GameObject parent = this.transform.parent.gameObject;
        parent.transform.position += (dest - parent.transform.position).normalized * -1 * parent.GetComponent<Skeletos>().speed * Time.deltaTime;

        if (Time.time >= currentFleeTime)
        {
            currentFleeTime = 0;
            sightSensor.distance = originalSightDistance;
            print("Sight distance was saved as " + originalSightDistance + "");
            print("State changing to patrol");
            currentState = EnemyState.Patrol;
            return;
        }
    }

    void AngeredPatrol() {
        if (sightSensor.detectedObject == null)
        {
            print("distance was " + sightSensor.distance + ", saved was " + originalSightDistance);
            sightSensor.distance = originalSightDistance;
            print("distance now is " + sightSensor.distance);
            print("State changing to patrol");
            currentState = EnemyState.Patrol;
            return;
        }
        else { 
            Vector3 dest = sightSensor.detectedObject.transform.position;
            GameObject parent = this.transform.parent.gameObject;
            parent.transform.position += (dest - parent.transform.position).normalized * parent.GetComponent<Skeletos>().speed * Time.deltaTime;
            rb.velocity = (dest - parent.transform.position) * parent.GetComponent<Skeletos>().speed * Time.deltaTime;
        }

        float distanceToPlayer = Vector2.Distance(transform.position, sightSensor.detectedObject.transform.position);
        if (distanceToPlayer <= playerAttackDistance) {
            currentState = EnemyState.Attack;
        }
    }
}

