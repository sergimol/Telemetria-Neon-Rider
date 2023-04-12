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
    List<TrackerEvent> eventsBuff;

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
        eventsBuff = new();
        createStream = new StreamWriter("GameTracked.json"); // !!! Cambiarlo por llamada a la persistencia
        AddEvent(EventType.INICIO, new possibleVar{});
    }

    private void OnDestroy()
    {
        if(instance = this)
        {
            AddEvent(EventType.FIN, new possibleVar{});
            Post();

            createStream.Close();
        }
    }

    public void AddEvent(EventType t, possibleVar pV)
    {
        eventsBuff.Add(new TrackerEvent(t, sessionId, pV));
    }

    public void TrackCompletable(string s, TrackerEvent e)
    {

    }

    public async void Post()
    {
        using (StreamWriter writer = createStream)
        {
            foreach(TrackerEvent e in eventsBuff)
            {
                await writer.WriteLineAsync("{" + e.ToJson() + "},");   // !!! Cambiarlo por llamada a persistencia             
            }
            eventsBuff.Clear();
        }
    }
}
