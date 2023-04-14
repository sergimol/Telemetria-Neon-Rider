using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Analytics;

public class Tracker : MonoBehaviour
{
    public static Tracker instance = null;
    StreamWriter createStream_json;
    StreamWriter createStream_xml;
    StreamWriter createStream_csv;
    long sessionId;
    List<TrackerEvent> eventsBuff;

    [SerializeField]    //Espaciado entre posts
    float timeBetweenPosts;
    float tSinceLastPost = 0;

    [SerializeField]
    bool serializeInJSON = true;
    [SerializeField]
    bool serializeInXML = true;
    [SerializeField]
    bool serializeInCSV = true;

    TrackerConfig config;
    JSONSerializer serializerJSON;
    XMLSerializer serializerXML;
    CSVSerializer serializerCSV;

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
        serializerJSON = GetComponent<JSONSerializer>();
        serializerXML = GetComponent<XMLSerializer>();
        serializerCSV = GetComponent<CSVSerializer>();

        sessionId = AnalyticsSessionInfo.sessionId;
        eventsBuff = new();
        createStream_json = new StreamWriter("GameTracked.json"); // !!! Cambiarlo por llamada a la persistencia
        createStream_xml = new StreamWriter("GameTracked.xml"); // !!! Cambiarlo por llamada a la persistencia
        createStream_csv = new StreamWriter("GameTracked.csv"); // !!! Cambiarlo por llamada a la persistencia
        AddEvent("Inicio", new possibleVar { });
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
            AddEvent("Fin", new possibleVar { });
            Post();

            createStream_json.Close();
            createStream_xml.Close();
            createStream_csv.Close();
        }
    }

    public void AddEvent(string t, possibleVar pV)
    {
        if (!config.eventsTracked.ContainsKey(t))
            Debug.Log("El evento " + t + " no se encuentra en la lista de eventos");
        else if(config.eventsTracked[t])
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
                string ser = await serializerJSON.Serialize(e);
                await createStream_json.WriteLineAsync("{" + ser + "},");   // !!! Cambiarlo por llamada a persistencia
            }
            if (serializeInXML)
            {
                string ser = await serializerXML.Serialize(e);
                await createStream_xml.WriteLineAsync(ser);   // !!! Cambiarlo por llamada a persistencia
            }
            if (serializeInCSV)
            {
                string ser = await serializerCSV.Serialize(e);
                Debug.Log(ser);
                await createStream_csv.WriteLineAsync(ser);   // !!! Cambiarlo por llamada a persistencia
            }
        }
        eventsBuff.Clear();
    }

    public long getSessionId()
    {
        return sessionId;
    }
}
