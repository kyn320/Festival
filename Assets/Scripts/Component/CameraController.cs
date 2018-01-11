using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    Transform tr;

    public Transform target;
    public Vector3 margin;
    public float followSpeed = 10f;


    void Awake()
    {
        tr = GetComponent<Transform>();
    }

    void FixedUpdate()
    {
        if (target != null)
        {
            tr.position = Vector3.Lerp(tr.position, target.position + margin, Time.deltaTime * followSpeed);
        }
    }
}
