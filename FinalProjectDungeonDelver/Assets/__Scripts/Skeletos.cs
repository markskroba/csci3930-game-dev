using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(InRoom))]
public class Skeletos : Enemy, IFacingMover
{
    [Header("Inscribed: Skeletos")]
    public int      speed = 2;
    public float    timeThinkMin = 1f;
    public float    timeThinkMax = 4f;

    [Header("Dynamic: Skeletos")]
    [Range(0,4)]
    public int      facing = 0;
    public float    timeNextDecision = 0;

    private InRoom  inRm;

    protected override void Awake() {
        base.Awake();
        inRm = GetComponent<InRoom>();
    }

    protected override void Update() {
        base .Update();
        if (knockback) return;
    }

    void DecideDirection() {
        facing = Random.Range(0,5);
        timeNextDecision = Time.time + Random.Range(timeThinkMin, timeThinkMax);
    }

    void BeginPatrol() {
        print("Patrol has begun");
        return;
    }

    public int GetFacing() { return facing % 4; }      // different from Dray

    public float GetSpeed() { return speed; }

    public bool moving { get { return (facing < 4); } }    // different from Dray

    public float gridMult { get { return inRm.gridMult; } }

    public bool isInRoom { get { return inRm.isInRoom; } }

    public Vector2 roomNum
    {
        get { return inRm.roomNum; }
        set { inRm.roomNum = value; }
    }

    public Vector2 posInRoom
    {
        get { return inRm.posInRoom; }
        set { inRm.posInRoom = value; }
    }

    public Vector2 GetGridPosInRoom(float mult = -1)
    {
        return inRm.GetGridPosInRoom(mult);
    }

}