using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    Transform tr;

    public Transform target;
    public Vector3 margin;
    public float followSpeed = 10f;
    
    Vector3 mousePos;
    public float dragSpeed = -1f;

    void Awake()
    {
        tr = GetComponent<Transform>();
    }

    void Update()
    {
        if (!MapEditor.isMouseCamControl)
            return;

        if (Input.GetMouseButtonDown(0))
        {
            mousePos = Input.mousePosition;
            return;
        }

        if (!Input.GetMouseButton(0)) return;

        Vector3 dir = Camera.main.ScreenToViewportPoint(Input.mousePosition - mousePos);
        Vector3 move = new Vector3(dir.x * dragSpeed, dir.y * dragSpeed);

        transform.Translate(move, Space.World);
    }

    void FixedUpdate()
    {
        if (!MapEditor.isMouseCamControl && target != null)
        {
            tr.position = Vector3.Lerp(tr.position, target.position + margin, Time.deltaTime * followSpeed);
        }
    }
    
}
