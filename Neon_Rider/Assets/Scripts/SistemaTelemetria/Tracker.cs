using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Analytics;

public class Tracker : MonoBehaviour
{
    public static Tracker instance = null;
    long sessionId;

    [SerializeField]    //Espaciado entre posts
    float timeBetweenPosts;
    float tSinceLastPost = 0;

    TrackerConfig config;
    FilePersistence filePersistence;
    [SerializeField] bool filePers = true;
    ServerPersistence serverPersistence;
    [SerializeField] bool serverPers = true;

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
        config = GetComponent<TrackerConfig>();
        filePersistence = GetComponent<FilePersistence>();
        serverPersistence = GetComponent<ServerPersistence>();

        sessionId = AnalyticsSessionInfo.sessionId;
    }
    private void Start()
    {
        AddEvent("Inicio", new possibleVar { });
    }

    private void Update()
    {
        if (tSinceLastPost > timeBetweenPosts)
        {
            if (filePers) filePersistence.Flush();
            if (serverPers) serverPersistence.Flush();
            tSinceLastPost = 0;
        }
        else
            tSinceLastPost += Time.deltaTime;
    }

    private void OnDestroy()
    {
        if (instance == this)
        {
            AddEvent("Fin", new possibleVar { });
            if (filePers) filePersistence.Flush();
            if (serverPers) serverPersistence.Flush();
        }
    }

    public void AddEvent(string t, possibleVar pV)
    {
        if (!config.eventsTracked.ContainsKey(t))
            Debug.Log("El evento " + t + " no se encuentra en la lista de eventos");
        else if (config.eventsTracked[t])
        {
            if (filePers) filePersistence.Send(new TrackerEvent(t, pV));
            if (serverPers) serverPersistence.Send(new TrackerEvent(t, pV));
        }
    }

    public long getSessionId()
    {
        return sessionId;
    }
}
