using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Tracker : MonoBehaviour
{
    public static Tracker instance = null;
    StreamWriter createStream;
    int sessionId;
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
        sessionId = 0;
        eventos = new();
        createStream = new StreamWriter("GameTracked.json"); // !!! Cambiarlo por llamada a la persistencia
        eventos.Add(new TrackerEvent(EventType.INICIO, sessionId));
    }

    private void OnDestroy()
    {
        if(instance = this)
        {
            eventos.Add(new TrackerEvent(EventType.FIN, sessionId));
            Post();

            createStream.Close();
        }
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
