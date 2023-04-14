using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public enum EventType { INICIO, FIN, INICIOSALA, FINSALA, INICIONIVEL, FINNIVEL, MUERTEJUGADOR, MUERTEENEMIGO, BLOQUEOBALA }
public struct possibleVar
{
    public Vector2? pos;
    public int? enemyId;
    public int? roomId;
}
public class TrackerEvent
{
    //Variables comunes
    EventType type; 
    long timeStamp;

    //Variables distintas según tipo
    possibleVar pVar;

    public TrackerEvent(EventType e, possibleVar pV)
    {
        type = e;
        timeStamp = DateTimeOffset.Now.ToUnixTimeSeconds();
        pVar = pV;
    }

    public async Task<string> ToJson()
    {
        JSONSerializer serializer = new JSONSerializer();
        return await serializer.Serialize(this);
    }

    public EventType GetType()
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
