using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public UIInGame ui;

    public float playTime;
    public int sec, min, hour, day, month, year;

    public bool isOnMenu = false;

    void Awake()
    {
        instance = this;
    }

    // Use this for initialization
    void Start()
    {
        ui = UIInGame.instance;
        playTime = sec;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateTime();
    }

    void UpdateTime()
    {
        playTime += Time.deltaTime;

        if (playTime >= 60)
        {
            ++min;
            playTime = 0;
        }

        sec = (int)playTime;

        if (min > 59)
        {
            ++hour;
            min = 0;
            if (hour > 23)
            {
                ++day;
                hour = 0;
                if (day > 30)
                {
                    ++month;
                    day = 1;
                    if (month > 12)
                    {
                        month = 1;
                        ++year;
                    }
                }
            }
        }


        ui.UpdateTime();

    }

}
