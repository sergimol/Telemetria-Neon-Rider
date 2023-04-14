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

    [SerializeField]    //Espaciado entre posts
    float timeBetweenPosts;
    float tSinceLastPost = 0;

    [SerializeField]
    bool serializeInJSON = true;
    [SerializeField]
    bool serializeInXML = true;

    JSONSerializer serializerJSON;

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
        serializerJSON = GetComponent<JSONSerializer>();
        sessionId = AnalyticsSessionInfo.sessionId;
        eventsBuff = new();
        createStream = new StreamWriter("GameTracked.json"); // !!! Cambiarlo por llamada a la persistencia
        AddEvent(EventType.INICIO, new possibleVar { });
    }

    private void Update()
    {
        if (tSinceLastPost > timeBetweenPosts)
        {
            Post();
            tSinceLastPost = 0;
        }
        else
            tSinceLastPost += Time.deltaTime;
    }

    private void OnDestroy()
    {
        if (instance == this)
        {
            AddEvent(EventType.FIN, new possibleVar { });
            Post();

            createStream.Close();
        }
    }

    public void AddEvent(EventType t, possibleVar pV)
    {
        eventsBuff.Add(new TrackerEvent(t, pV));
    }

    public void TrackCompletable(string s, TrackerEvent e)
    {

    }

    public async void Post()
    {
        foreach (TrackerEvent e in eventsBuff)
        {
            if (serializeInJSON)
            {
                string ser = serializerJSON.Serialize(e);
                await createStream.WriteLineAsync("{" + ser + "},");   // !!! Cambiarlo por llamada a persistencia
            }
        }
        eventsBuff.Clear();
    }

    public long getSessionId()
    {
        return sessionId;
    }
}
