using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EventType { INICIO, FIN, NUEVONIVEL, INICIOPAUSA, FINPAUSA, NIVELCOMPLETADO, GOLPE, MUERTEJUGADOR, MUERTEENEMIGO, BLOQUEOBALA }
public class TrackerEvent
{
    EventType tipo;
    long timeStamp; //  DateTimeOffset.Now.ToUnixTimeSeconds();
    long id_sesion;

    public TrackerEvent(EventType e, long id)
    {
        tipo = e;
        timeStamp = DateTimeOffset.Now.ToUnixTimeSeconds();
        id_sesion = id;
    }

    public string ToJson()
    {
        string aux = "\"TimeStamp\": \"" + timeStamp.ToString() + "\", \"SessionId\": \"" +
            id_sesion.ToString() + "\", \"EventType\": \"" + tipo.ToString() + "\""; // Añadir otros atributos
        return aux;
    }

}
