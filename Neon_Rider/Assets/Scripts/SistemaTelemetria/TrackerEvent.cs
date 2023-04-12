using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EventType { INICIO, FIN, NUEVONIVEL, INICIOPAUSA, FINPAUSA, NIVELCOMPLETADO, GOLPE, MUERTEJUGADOR, MUERTEENEMIGO, BLOQUEOBALA }
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
    long sessionId;     //Esto habra que quitarlo de aqui cuando pongamos la persistencia bien

    //Variables distintas según tipo
    possibleVar pVar;

    public TrackerEvent(EventType e, long id, possibleVar pV)
    {
        type = e;
        timeStamp = DateTimeOffset.Now.ToUnixTimeSeconds();
        sessionId = id;
        pVar = pV;
    }

    public string ToJson()
    {
        string aux = "\"TimeStamp\": \"" + timeStamp.ToString() + "\", \"SessionId\": \"" +
            sessionId.ToString() + "\", \"EventType\": \"" + type.ToString() + "\"";    // Añadir otros atributos
        return aux;
    }
}
