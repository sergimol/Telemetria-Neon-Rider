using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackerConfig : MonoBehaviour
{
    // Configuraci�n del tracker espec�fica para el juego
    // Struct y array para elegir desde el editor que eventos se trackean
    [Serializable]
    public struct EventConfig
    {
        public string eventName;
        public bool isTracked;
    }
    [SerializeField]
    EventConfig[] eventConfig;

    // Diccionario para comprobar r�pidamente si debe trackearse un evento durante ejecuci�n
    public Dictionary<string, bool> eventsTracked = new Dictionary<string, bool>();

    private void Awake()
    {
        foreach(var config in eventConfig)
        {
            eventsTracked.Add(config.eventName, config.isTracked);
        }
    }
}
