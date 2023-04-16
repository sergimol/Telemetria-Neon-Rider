using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
public class FilePersistence : IPersistence
{
    List<TrackerEvent> eventsBuff;

    [SerializeField]
    bool serializeInJSON = true;
    [SerializeField]
    bool serializeInXML = true;
    [SerializeField]
    bool serializeInCSV = true;

    JSONSerializer serializerJSON = null;   //JSON
    StreamWriter jsonStream = null;

    XMLSerializer serializerXML = null;     //XML
    StreamWriter xmlStream = null;

    CSVSerializer serializerCSV = null;     //CSV
    StreamWriter csvStream = null;

    private void Start()
    {
        eventsBuff = new();

        if (serializeInJSON)
        {
            serializerJSON = GetComponent<JSONSerializer>();
            jsonStream = new StreamWriter("GameTracked.json");
        }
        if (serializeInXML)
        {
            serializerXML = GetComponent<XMLSerializer>();
            xmlStream = new StreamWriter("GameTracked.xml");
        }
        if (serializeInCSV)
        {
            serializerCSV = GetComponent<CSVSerializer>();
            csvStream = new StreamWriter("GameTracked.csv");
        }
    }

    private void OnDestroy()
    {
        if (jsonStream != null) jsonStream.Close();
        if (xmlStream != null) xmlStream.Close();
        if (csvStream != null) csvStream.Close();
    }
    public override void Send(TrackerEvent e)
    {
        eventsBuff.Add(e);
    }

    public override void Flush()
    {
        List<TrackerEvent> events = new List<TrackerEvent>(eventsBuff);
        eventsBuff.Clear();
        WriteAsync(events);
    }

    private void WriteAsync(List<TrackerEvent> events)
    {
         foreach (TrackerEvent e in events)
        {
            if (serializeInJSON)
            {
                string ser = serializerJSON.Serialize(e);
                jsonStream.WriteLine(ser);
            }
            if (serializeInXML)
            {
                string ser = serializerXML.Serialize(e);
                xmlStream.WriteLine(ser);
            }
            if (serializeInCSV)
            {
                string ser = serializerCSV.Serialize(e);
                csvStream.WriteLine(ser);
            }
        }
    }
}
