using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Analytics;

public class Tracker : MonoBehaviour
{
    public static Tracker instance = null;
    StreamWriter createStream;
    long sessionId;
    List<TrackerEvent> eventos;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            instance.Init();
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Init()
    {
        sessionId = AnalyticsSessionInfo.sessionId;
        eventos = new();
        createStream = new StreamWriter("GameTracked.json"); // !!! Cambiarlo por llamada a la persistencia
        AddEvent(EventType.INICIO);
    }

    private void OnDestroy()
    {
        if(instance = this)
        {
            AddEvent(EventType.FIN);
            Post();

            createStream.Close();
        }
    }

    public void AddEvent(EventType t)
    {
        eventos.Add(new TrackerEvent(t, sessionId));
    }

    public void TrackCompletable(string s, TrackerEvent e)
    {

    }

    public async void Post()
    {
        using (StreamWriter writer = createStream)
        {
            foreach(TrackerEvent e in eventos)
            {
                await writer.WriteLineAsync("{" + e.ToJson() + "},");   // !!! Cambiarlo por llamada a persistencia             
            }
            eventos.Clear();
        }
    }
}
