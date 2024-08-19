using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Timer 
{
    public float time;
    private float targetTime;

    public bool TimeOut
    {
        get { return targetTime < Time.time ; }
    }

    public Timer(float time)
    {
        this.time = time;
    }

    public void StartTimer()
    {
        targetTime = Time.time + time;
    }

    
}
