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

    public void UpdateTime()
    {
        timeText.text = string.Format("{0:D2} : {1:D2}", GameManager.instance.hour, GameManager.instance.min);
        monthDayText.text = string.Format("{0:D2} / {1:D2}", GameManager.instance.month, GameManager.instance.day);

        float dayTime = GameManager.instance.hour * 3600f + GameManager.instance.min * 60f + GameManager.instance.sec;
        
        if (dayTime >= 43200)
        {
            daySlider.value = 1 - ((dayTime - 43200f) / 43200f);
        }
        else {
            daySlider.value = dayTime / 43200f;
        }


    }

}
