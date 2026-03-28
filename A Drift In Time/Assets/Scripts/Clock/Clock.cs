using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clock : MonoBehaviour
{
    float time0;
    int hour;
    int minute;

    // Start is called before the first frame update
    void Awake()
    {
        time0 = Time.time;
        hour = 0;
        minute = 0;  
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time-time0>=1)
        {
            time0 = Time.time;
            minute+=1;

            if(minute>=60)
            {
                minute=0;
                hour+=1;

                if (hour>=24)
                {
                    minute=0;
                    hour=0;
                }
            }

            EventManager.Instance.UpdateClock(hour,minute);

            //Debug.Log(hour+":"+minute);

        }
    }
}
