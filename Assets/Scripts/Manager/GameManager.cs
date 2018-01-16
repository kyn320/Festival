using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public static bool isNight = false;

    public UIInGame ui;

    public Sun sun;


    public float playTime;
    public float timeStamp = 1f;
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
        playTime += Time.deltaTime * timeStamp;

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
        float dayTime = hour * 3600f + min * 60f + sec;
        //태양 렌더
        sun.UpdateTime(dayTime);
        
        //밤낮 게이지 렌더
        int hourRender = hour + 6;

        if (hourRender > 23)
            hourRender -= 24;

        dayTime = hourRender * 3600f + min * 60f + sec;

        if (dayTime >= 43200)
        {
            //낮
            isNight = false;
        }
        else {
            //밤
            isNight = true;
        }
        
        ui.UpdateTime(dayTime);
    }

}
