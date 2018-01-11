using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldTargetTracker : TargetTracker
{
    [SerializeField]
    private bool isScreenTarget = false;

    protected override void Tracking()
    {
        if (isScreenTarget)
        {
            tr.position = Vector3.Lerp(tr.position, Camera.main.ScreenToWorldPoint(target.position) + margin, Time.deltaTime * trackingSpeed);
        }
        else {
            tr.position = Vector3.Lerp(tr.position, target.position + margin, Time.deltaTime * trackingSpeed);
        }
    }
}
