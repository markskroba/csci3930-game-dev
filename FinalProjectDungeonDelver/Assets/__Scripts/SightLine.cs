using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class SightLine : MonoBehaviour
{
    public Transform EyePoint;
    public string TargetTag;
    public float FieldOfView = 45f;
    public Vector2 LastKnowSighting { get; set; } = Vector2.zero;
    public bool IsTargetInSightLine { get; set; } = false;
    private CircleCollider2D ThisCollider;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag(TargetTag))
        {
            UpdateSight(collision.transform);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(TargetTag))
        {
            IsTargetInSightLine = false;
        }
    }

    private void UpdateSight(Transform target)
    {
        IsTargetInSightLine = HasClearLineofSightToTarget(target) && TargetInFOV(target);

        if (IsTargetInSightLine)
        {
            LastKnowSighting = target.position;
        }
    }

    private bool HasClearLineofSightToTarget(Transform Target)
    {
        RaycastHit Info;

        Vector2 DirToTarget = (Target.position - EyePoint.position).normalized;
        if (Physics.Raycast(EyePoint.position, DirToTarget, out Info, ThisCollider.radius))
        {
            if (Info.transform.CompareTag(TargetTag))
            {
                return true;
            }
        }

        return false;
    }

    private bool TargetInFOV(Transform target)
    {
        Vector2 DirToTarget = target.position - EyePoint.position;
        float angle = Vector2.Angle(EyePoint.forward, DirToTarget);
        if (angle <= FieldOfView)
        {
            return true;
        }
        return false;
    }

    // Start is called before the first frame update
    void Start()
    {
        ThisCollider = GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
