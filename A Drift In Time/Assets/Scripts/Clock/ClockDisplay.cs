using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ClockDisplay : MonoBehaviour
{

    public TextMeshProUGUI hourDisplay;
    public TextMeshProUGUI minuteDisplay;

    void OnEnable()
    {
        EventManager.Instance.OnUpdateClock+=OnUpdateClock;
    }

    void OnDisable()
    {
        EventManager.Instance.OnUpdateClock-=OnUpdateClock;
    }
    // Start is called before the first frame update
    void Awake()
    {
        if(!hourDisplay) {Debug.LogError("Reference to hourDIsplay missing from ClockDisplay");}
        if(!minuteDisplay) {Debug.LogError("Reference to minuteDisplay missing from ClockDisplay");}
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnUpdateClock(int hour,int minute)
    {
        if (hour.ToString().Length == 1)
        {
            hourDisplay.text = "0" + hour.ToString();
        }
        else
        {
            hourDisplay.text = hour.ToString();
        }
        
        if (minute.ToString().Length == 1)
        {
            minuteDisplay.text = "0" + minute.ToString();
        }
        else
        {
            minuteDisplay.text = minute.ToString();
        }
        
    }
}
