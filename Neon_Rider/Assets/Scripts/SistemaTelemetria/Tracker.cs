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
        //config = GetComponent<TrackerConfig>();
        filePersistence = new FilePersistence(true, true, true);
        //serverPersistence = new ServerPersistence();

        Type type = typeof(InicioEvent);
        eventsTracked.Add(type.Name, true);
        type = typeof(FinEvent);
        eventsTracked.Add(type.Name, true);
        ///////////////////////////////
        type = typeof(BloqueoEvent);
        eventsTracked.Add(type.Name, true);
        type = typeof(FinNivelEvent);
        eventsTracked.Add(type.Name, true);
        type = typeof(FinSalaEvent);
        eventsTracked.Add(type.Name, true);
        type = typeof(InicioNivelEvent);
        eventsTracked.Add(type.Name, true);
        type = typeof(InicioSalaEvent);
        eventsTracked.Add(type.Name, true);
        type = typeof(MuerteEnemigoEvent);
        eventsTracked.Add(type.Name, true);
        type = typeof(MuerteJugadorEvent);
        eventsTracked.Add(type.Name, true);
        type = typeof(PosicionJugadorEvent);
        eventsTracked.Add(type.Name, true);
        type = typeof(PosicionNPCEvent);
        eventsTracked.Add(type.Name, true);
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
        if (eventsTracked[e.GetType().Name])
        {
            if (filePers) filePersistence.Send(e);
            //if (serverPers) serverPersistence.Send(e);
        }
    }

    public long getSessionId()
    {
        return sessionId;
    }
}
