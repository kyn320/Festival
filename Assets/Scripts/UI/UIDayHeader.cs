using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIDayHeader : MonoBehaviour
{

    public Text monthDayText;
    public Image weatherImage;
    public Slider daySlider;
    public Text timeText;

    public void UpdateTime(float _dayTime, float _sliderValue)
    {
        timeText.text = string.Format("{0:D2} : {1:D2}", GameManager.instance.hour, GameManager.instance.min);
        monthDayText.text = string.Format("{0:D2} / {1:D2}", GameManager.instance.month, GameManager.instance.day);

        daySlider.value = GameManager.GetTimeRatio(_dayTime);
    }

}
