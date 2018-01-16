using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sun : MonoBehaviour
{
    Transform tr;

    void Awake()
    {
        tr = GetComponent<Transform>();
    }

    public void UpdateTime(float _dayTime)
    {
        tr.localRotation = Quaternion.Euler(((_dayTime - 21600) / 86400f * 360f) + 270f, 0, 0);
    }


}
