using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Difficulty {
    public float speed = 1f;
    public float leftAndRightEdge = 10f;
    public float changeDirChance = .1f;
    public float appleDropDelay = 1f;

    public Difficulty(float speed, float leftAndRightEdge, float changeDirChance, float appleDropDelay)
    {
        this.speed = speed;
        this.leftAndRightEdge = leftAndRightEdge;
        this.changeDirChance = changeDirChance;
        this.appleDropDelay = appleDropDelay;
    }
}

public class AppleTree : MonoBehaviour
{
    [Header("Inscribed")]
    public GameObject applePrefab;
    public GameObject goldenApplePrefab;
    public GameObject poisonApplePrefab;

    public float speed = 1f;
    public float leftAndRightEdge = 10f;
    public float changeDirChance = .1f;
    public float appleDropDelay = 1f;

    public float goldenAppleChance = .2f;
    public float poisonAppleChance = .1f;
    public int level = 1;

    private List<Difficulty> difficulties;

    // Start is called before the first frame update
    void Start()
    {
        Difficulty easiest = new Difficulty(.5f, 10f, 0f, 1f);
        Difficulty easy = new Difficulty(1f, 10f, .1f, 1f);
        Difficulty medium = new Difficulty(5f, 10f, .1f, .5f);
        Difficulty hard = new Difficulty(10f, 10f, .05f, .5f);
        difficulties = new List<Difficulty> { easiest, easy, medium, hard };

        if (level > 3 || level < 0) level = 1;
        Difficulty selectedDifficulty = difficulties[level];

        speed = selectedDifficulty.speed;
        changeDirChance = selectedDifficulty.changeDirChance;
        appleDropDelay = selectedDifficulty.appleDropDelay;

        Invoke("DropApple", 2f);
    }

    void DropApple() {
        GameObject apple;
        float chance = Random.value;
        Debug.Log(chance);

        if (chance < goldenAppleChance)
        {
            apple = Instantiate<GameObject>(goldenApplePrefab);
        }
        else if (chance < poisonAppleChance + goldenAppleChance && chance > goldenAppleChance)
        {
            apple = Instantiate<GameObject>(poisonApplePrefab);
        }
        else {
            apple = Instantiate<GameObject>(applePrefab);
        }
        apple.transform.position = transform.position;
        Invoke("DropApple", appleDropDelay);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;
        pos.x += speed * Time.deltaTime;
        transform.position = pos;
        
        if (pos.x < -leftAndRightEdge) {
            speed = Mathf.Abs(speed);
        } else if (pos.x > leftAndRightEdge) {
            speed = - Mathf.Abs(speed);
        // } else if (Random.value < changeDirChance) {
        //     speed *= -1;
        }
    }

    void FixedUpdate() {
        if (Random.value < changeDirChance) {
            speed *= -1;
        }
    }
}
