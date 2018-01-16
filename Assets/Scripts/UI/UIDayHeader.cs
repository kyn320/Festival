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

    public void UpdateTime(float _dayTime)
    {
        timeText.text = string.Format("{0:D2} : {1:D2}", GameManager.instance.hour, GameManager.instance.min);
        monthDayText.text = string.Format("{0:D2} / {1:D2}", GameManager.instance.month, GameManager.instance.day);
        
        if (_dayTime >= 43200)
        {
            daySlider.value = 1 - ((_dayTime - 43200f) / 43200f);
        }
        else {
            daySlider.value = _dayTime / 43200f;
        }

    }

}
