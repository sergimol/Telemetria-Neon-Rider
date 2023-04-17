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

    string ruta_json = @"Trazas\JSON\";
    string ruta_xml = @"Trazas\XML\";
    string ruta_csv = @"Trazas\CSV\";

    private void Start()
    {
        eventsBuff = new();
        string id = Tracker.instance.getSessionId().ToString();

        // Crear carpetas del tracker
        if (!Directory.Exists(@"Trazas\"))
        {
            Directory.CreateDirectory(ruta_json);
            Directory.CreateDirectory(ruta_csv);
            Directory.CreateDirectory(ruta_xml);
        }

        if (serializeInJSON)
        {
            serializerJSON = GetComponent<JSONSerializer>();
            jsonStream = new StreamWriter(ruta_json + id + ".json");
        }
        if (serializeInXML)
        {
            serializerXML = GetComponent<XMLSerializer>();
            xmlStream = new StreamWriter(ruta_xml + id + ".xml");
        }
        if (serializeInCSV)
        {
            serializerCSV = GetComponent<CSVSerializer>();
            csvStream = new StreamWriter(ruta_csv + id + ".csv");
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
        Write(events);
    }

    private void Write(List<TrackerEvent> events)
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
