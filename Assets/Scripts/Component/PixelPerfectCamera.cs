using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PixelPerfectCamera : MonoBehaviour
{

    Camera cam;

    public float originSize = 1f;

    public float mapWidth;
    public int pixelsToUnit = 32;


    void Awake()
    {
        cam = GetComponent<Camera>();
    }

    void Start()
    {
        cam.orthographicSize = originSize = SetSize();

        if (GameManager.instance != null)
        {
            mapWidth = GameManager.instance.mapWidth;
            pixelsToUnit = GameManager.pixelsToUnit;
        }
    }

    float SetSize()
    {
        int height = Mathf.RoundToInt(mapWidth / (float)Screen.width * Screen.height);

        return height / pixelsToUnit * 0.5f;
    }

}
