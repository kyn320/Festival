using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class PixelPerfectCamera : MonoBehaviour
{

    Camera cam;

    public int targetWidth = 640;

    public int pixelsToUnit = 32;

    public float originSize = 0.1f;

    void Awake()
    {
        cam = GetComponent<Camera>();
        cam.orthographicSize = originSize = SetSize();
    }
    
    float SetSize()
    {
        int height = Mathf.RoundToInt(targetWidth / (float)Screen.width * Screen.height);
        return height / pixelsToUnit / 2;
    }
}
