using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Xml.Serialization;
using UnityEngine;
public struct possibleVar
{
    public Vector2? pos;
    public int? enemyId;
    public int? roomId;
}
public class TrackerEvent
{
    //Variables comunes
    protected String type; 
    protected long timeStamp;

    //Variables distintas según tipo
    possibleVar pVar;

    public TrackerEvent(String t, possibleVar pV)
    {
        type = t;
        timeStamp = DateTimeOffset.Now.ToUnixTimeSeconds();
        pVar = pV;
    }
    public TrackerEvent()
    {
    }

    public string GetEventType()
    {
        return type;
    }

    public long getTimeStamp()
    {
        return timeStamp;
    }

    public possibleVar getVar()
    {
        return pVar;
    }

    // SERIALIZADORES
    virtual public string toJSON()
    {
        // Atributos comunes a todos los eventos
        string aux = "{\"TimeStamp\": \"" + timeStamp.ToString() + "\", \"SessionId\": \"" +
            Tracker.instance.getSessionId().ToString() + "\", \"EventType\": \"" + type + "\"";

        return aux;
    }
    virtual public string toCSV()
    {
        // Atributos comunes a todos los eventos
        string aux = "TimeStamp," + timeStamp + "," + "EventType," + type + "," + "SessionId," + Tracker.instance.getSessionId().ToString() + ",";

        return aux;
    }
    virtual public string toXML()
    {
        // Creacion del objeto DataXml
        InicioSalaXML dataXml = new InicioSalaXML();
        StringWriter stringWriter = new StringWriter();
        XmlSerializer serializer = new XmlSerializer(typeof(InicioSalaXML));

        // Atributos comunes a todos los eventos
        dataXml.x_type = type;
        dataXml.x_timeStamp = timeStamp.ToString();
        dataXml.x_sessionID = Tracker.instance.getSessionId().ToString();

        // Serializamos con el formato
        serializer.Serialize(stringWriter, dataXml);


        return stringWriter.ToString();
    }

}
