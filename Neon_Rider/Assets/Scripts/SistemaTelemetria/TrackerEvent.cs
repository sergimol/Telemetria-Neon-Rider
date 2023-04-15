using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
public struct possibleVar
{
    public Vector2? pos;
    public int? enemyId;
    public int? roomId;
}
public class TrackerEvent
{
    //Variables comunes
    String type; 
    long timeStamp;

    //Variables distintas según tipo
    possibleVar pVar;

    public TrackerEvent(String t, possibleVar pV)
    {
        type = t;
        timeStamp = DateTimeOffset.Now.ToUnixTimeSeconds();
        pVar = pV;
    }
    public TrackerEvent()
    {
    }

    public string GetEventType()
    {
        return type;
    }

    public long getTimeStamp()
    {
        return timeStamp;
    }

    public possibleVar getVar()
    {
        return pVar;
    }
}
