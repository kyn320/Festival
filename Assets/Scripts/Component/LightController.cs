using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightController : MonoBehaviour
{

    public Light lightPoint;
    
    public void SetLight(bool _isNight)
    {
        lightPoint.enabled = _isNight;
    }


}
