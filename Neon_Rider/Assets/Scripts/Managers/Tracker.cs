using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event
{
    enum EventType { INICIO, FIN, NUEVONIVEL, INICIOPAUSA, FINPAUSA, NIVELCOMPLETADO, GOLPE, MUERTEJUGADOR, MUERTEENEMIGO, BLOQUEOBALA }
    long timeStamp; //  DateTimeOffset.Now.ToUnixTimeSeconds();
    int id_sesion;

}

public class Tracker : MonoBehaviour
{

    private static Tracker instance = null;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }


}
