using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    Transform tr;

    public Transform target;
    public Vector3 margin;
    public float followSpeed = 10f;

    private float dist;
    private Vector3 v3OrgMouse;

    public float zoomFactor = 0.4f;
    public float minZoom = 0.4f;

    [SerializeField]
    private float orginZoom = 0;
    [SerializeField]
    private float maxZoom = 25.0f;
    [SerializeField]
    private float minX, maxX;
    [SerializeField]
    private float minY, maxY;

    void Awake()
    {
        tr = GetComponent<Transform>();
    }

    void Start()
    {
        orginZoom = Camera.main.orthographicSize;
        dist = transform.position.z;
        maxZoom = Mathf.RoundToInt(GameManager.instance.mapWidth / (float)Screen.width * Screen.height) / GameManager.pixelsToUnit * 0.5f;
        CalcMinMax();
    }

    void Update()
    {
        if (!MapEditor.isMouseCamControl)
            return;

        if (Input.GetMouseButtonDown(0))
        {
            v3OrgMouse = new Vector3(Input.mousePosition.x, Input.mousePosition.y, dist);
            v3OrgMouse = Camera.main.ScreenToWorldPoint(v3OrgMouse);
        }
        else if (Input.GetMouseButton(0))
        {
            var v3Pos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, dist);
            v3Pos = Camera.main.ScreenToWorldPoint(v3Pos);
            transform.position -= (v3Pos - v3OrgMouse);
        }

        float sw = Input.GetAxis("Mouse ScrollWheel");
        if (Mathf.Abs(sw) > 0.01f)
        {
            Camera.main.orthographicSize += -sw * zoomFactor;
            Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize, minZoom, maxZoom);
            CalcMinMax();
        }
        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(pos.x, minX, maxX);
        pos.y = Mathf.Clamp(pos.y, minY, maxY);
        transform.position = pos;
    }

    void FixedUpdate()
    {
        if (!MapEditor.isMouseCamControl && target != null)
        {
            tr.position = Vector3.Lerp(tr.position, target.position + margin, Time.deltaTime * followSpeed);
        }
    }

    void CalcMinMax()
    {
        float zoomRatio = (1 - (maxZoom / Camera.main.orthographicSize));
        float height = Camera.main.orthographicSize * zoomRatio * 2f;
        float width = height * Camera.main.aspect;

        minX = width * 0.5f;
        maxX = -minX;

        minY = height * 0.5f;
        maxY = -minY;
    }

}
