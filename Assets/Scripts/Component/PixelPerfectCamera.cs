using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PixelPerfectCamera : MonoBehaviour
{

    Camera cam;

    public float originSize = 1f;

    void Awake()
    {
        cam = GetComponent<Camera>();
    }

    void Start()
    {
        cam.orthographicSize = originSize = SetSize();
    }

    float SetSize()
    {
        int height = Mathf.RoundToInt(GameManager.instance.mapWidth / (float)Screen.width * Screen.height);
        return height / GameManager.pixelsToUnit * 0.5f;
    }

}
