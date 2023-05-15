using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// public class Enemy : MonoBehaviour, ISwappable
public class Enemy : MonoBehaviour
{
    protected static Vector2[]      directions = new Vector2[] {
        Vector2.right, Vector2.up, Vector2.left, Vector2.down, Vector2.zero };

    [Header("Inscribed: Enemy")]
    public float            maxHealth = 1;
    public float            knockbackSpeed = 5;
    public float            knockbackDuration = 0.1f;
    public float            invincibleDuration = 0.5f;

    [SerializeField]
    private GameObject      guaranteedDrop = null;
    public List<GameObject> randomItems;

    [Header("Dynamic: Enemy")]
    public float            health;
    public bool             invincible = false;
    public bool             knockback = false;

    private float           knockbackDone = 0;
    private float           invincibleDone = 0;
    private Vector2         knockbackVel;

    protected Animator              anim;
    protected Rigidbody2D           rigid;
    protected SpriteRenderer        sRend;

    protected virtual void Awake()
    {
        health = maxHealth;
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        sRend = GetComponent<SpriteRenderer>();
    }

    protected virtual void Update()
    {
        if (invincible && Time.time > invincibleDone) invincible = false;
        sRend.color = invincible ? Color.red : Color.white;
        if (knockback) {
            rigid.velocity = knockbackVel;
            if (Time.time < knockbackDone) return ;
        }
        anim.speed = 1;
        knockback = false;
    }

    void OnTriggerEnter2D (Collider2D colld)
    {
        if (invincible) return;      // Return if this can't be damaged
        DamageEffect dEf = colld.gameObject.GetComponent<DamageEffect>();
        if (dEf == null) return;    // If no DamageEffect, exit

        health -= dEf.damage;       // Subtract the damage amount from health
        if (health <= 0)
            Die();
        else {
            // flee/angered patrol changes
            EnemyFSM fsm = this.GetComponentInChildren<EnemyFSM>();
            if (fsm != null) {
                fsm.decideOnReceivingDamage();
            }  
        }

        invincible = true;
        invincibleDone = Time.time + invincibleDuration;

        if (dEf.knockback)
        {
            Vector2 delta;
            // Is an IFacingMover attached to the Collider that triggered this?
            IFacingMover iFM = colld.GetComponent<IFacingMover>();
            if (iFM != null)
            {
                // Determine the direction of knockback from the iFM's facing
                delta = directions[iFM.GetFacing()];
            }
            else
            {
                // Determine the direction of knockback from relative position
                delta = transform.position - colld.transform.position;
                if (Mathf.Abs (delta.x) >= Mathf.Abs (delta.y))
                {
                    // Knockback should be horizontal
                    delta.x = (delta.x > 0) ? 1 : -1;
                    delta.y = 0;
                }
                else
                {
                    // Knockback should be vertical
                    delta.y = (delta.y > 0) ? 1 : -1;
                    delta.x = 0;                
                }
            }

            // Apply knockback speed to the Rigidbody
            knockbackVel = delta * knockbackSpeed;
            rigid.velocity = knockbackVel;

            // Set mode to knockback and set time to stop knockback
            knockback = true;;
            knockbackDone = Time.time + knockbackDuration;
            anim.speed = 0;
        }
    }

    void Die() {
        GameObject go;
        if (guaranteedDrop != null) {
            go = Instantiate<GameObject>(guaranteedDrop);
            go.transform.position = transform.position;
        } else if (randomItems.Count > 0) {
            int n = Random.Range(0, randomItems.Count);
            GameObject prefab = randomItems[n];
            if (prefab != null) {
                go = Instantiate<GameObject>(prefab);
                go.transform.position = transform.position;
            }
        }
        Destroy(gameObject);
    }

}
