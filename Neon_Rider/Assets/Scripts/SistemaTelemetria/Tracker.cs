using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class Tracker
{
    private static Tracker instance = null;
    long sessionId;

    //Espaciado entre posts
    float timeBetweenPosts;
    float tSinceLastPost = 0;

    FilePersistence filePersistence;
    bool filePers = true;
    //ServerPersistence serverPersistence;
    //bool serverPers = true;
    // Diccionario para comprobar rápidamente si debe trackearse un evento durante ejecución
    Dictionary<string, bool> eventsTracked = new Dictionary<string, bool>();

    private long timeLastUpdate;
    private Tracker()
    {
    }

    public static Tracker Instance
    {
        get
        {
            if (instance == null) { 
                instance = new Tracker();
            }
            return instance;
        }
    }

    public void Init(long id)
    {
        sessionId = id;
        filePersistence = new FilePersistence(true, true, true);

        AddTrackableEvent<InicioEvent>(true);
        AddTrackableEvent<FinEvent>(true);
        AddEvent(new InicioEvent());
    }

    public void Update()
    {
        long time = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
        if (tSinceLastPost > timeBetweenPosts)
        {
            if (filePers) filePersistence.Flush();
            //if (serverPers) serverPersistence.Flush();
            tSinceLastPost = 0;
        }
        else
            tSinceLastPost += (time - timeLastUpdate);
        timeLastUpdate = time;
    }

    public void Release()
    {
        if (instance == this)
        {
            AddEvent(new FinEvent());
            if (filePers) filePersistence.Release();
            //if (serverPers) serverPersistence.Release();
        }
    }

    public void AddEvent(TrackerEvent e)
    {
        string eventType = e.GetType().Name;
        if (!eventsTracked.ContainsKey(eventType) || !eventsTracked[eventType])
            return;
 
        if (filePers) filePersistence.Send(e);
        //if (serverPers) serverPersistence.Send(e);
    }

    public long GetSessionId()
    {
        return sessionId;
    }

    public void AddTrackableEvent<T>(bool track)
    {
        if (!typeof(T).IsSubclassOf(typeof(TrackerEvent)))
            return;

        Type type = typeof(T);
        if (!eventsTracked.ContainsKey(type.Name))
            eventsTracked.Add(type.Name, track);
    }

    public void ChangeTrackableState<T>(bool track)
    {
        if (!typeof(T).IsSubclassOf(typeof(TrackerEvent)))
            return;

        Type type = typeof(T);
        if (eventsTracked.ContainsKey(type.Name))
            eventsTracked[type.Name] = track;
    }
}
