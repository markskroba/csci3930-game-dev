using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehavior : MonoBehaviour
{
    public Vector3 CamOffset = new Vector3 (0f, 1.2f, -2.6f);
    public Vector3 CamRotationOffset = new Vector3 (-15f, 0f, 0f);

    private Transform _target;

    // Start is called before the first frame update
    void Start()
    {
        _target = GameObject.Find ("Player").transform;
    }

    void LateUpdate()
    {
        this.transform.position = _target.TransformPoint (CamOffset);
        this.transform.LookAt (_target);
        this.transform.Rotate (Vector3.up + CamRotationOffset);
    }
}
