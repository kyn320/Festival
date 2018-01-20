using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightController : MonoBehaviour
{
    public Light lightPoint;
    public float maxIntensity = 7;

    public float switchSpeed = 10f;

    void OnEnable()
    {
        GameManager.nightAction += SetLight;
    }

    public void SetLight(bool _isNight)
    {
        if (lightSwitch != null)
        {
            StopCoroutine(lightSwitch);
        }

        lightSwitch = StartCoroutine(LightSwitch(_isNight));
    }

    Coroutine lightSwitch = null;

    IEnumerator LightSwitch(bool _isNight)
    {
        if (_isNight)
        {
            lightPoint.enabled = _isNight;
            while (lightPoint.intensity < maxIntensity)
            {
                lightPoint.intensity = Mathf.Lerp(lightPoint.intensity, maxIntensity, Time.deltaTime * switchSpeed);
                yield return null;
            }
            lightPoint.intensity = maxIntensity;
        }
        else
        {
            while (lightPoint.intensity > 0.1f)
            {
                lightPoint.intensity = Mathf.Lerp(lightPoint.intensity, 0f, Time.deltaTime * switchSpeed);
                yield return null;
            }
            lightPoint.intensity = 0f;
            lightPoint.enabled = _isNight;

        }

        lightSwitch = null;
    }

    void OnDisable()
    {
        GameManager.nightAction -= SetLight;
    }

}
