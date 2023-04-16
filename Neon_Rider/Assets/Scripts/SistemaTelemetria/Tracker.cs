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
    FilePersistence persistence;

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
        persistence = GetComponent<FilePersistence>();

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
            persistence.Flush();
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
            persistence.Flush();
        }
    }

    public void AddEvent(string t, possibleVar pV)
    {
        if (!config.eventsTracked.ContainsKey(t))
            Debug.Log("El evento " + t + " no se encuentra en la lista de eventos");
        else if (config.eventsTracked[t])
            persistence.Send(new TrackerEvent(t, pV));
    }

    public void TrackCompletable(string s, TrackerEvent e)
    {

    }

    public long getSessionId()
    {
        return sessionId;
    }
}
