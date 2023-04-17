using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;

public class InicioSalaXML : DataXML
{
    public string x_roomId;
}

public class InicioSalaEvent : TrackerEvent
{
    // Atributos del evento
    int roomId;

    // Serializacion en JSON
    public override string toJSON()
    {
        string cadena = base.toJSON();
        cadena += ", \"RoomId\": \"" + roomId.ToString() + "\"";
        return cadena;
    }

    // Serializacion en CSV
    public override string toCSV()
    {
        string cadena = base.toCSV();
        cadena += "RoomID," + roomId.ToString() + ",";
        return cadena;
    }
    // Serializacion en XML
    public override string toXML()
    {
        // Creacion del objeto DataXml
        InicioSalaXML dataXml = new InicioSalaXML();
        StringWriter stringWriter = new StringWriter();
        XmlSerializer serializer = new XmlSerializer(typeof(InicioSalaXML));

        // Atributos comunes a todos los eventos
        dataXml.x_type = type;
        dataXml.x_timeStamp = timeStamp.ToString();
        dataXml.x_sessionID = Tracker.instance.getSessionId().ToString();

        // Atributos de evento
        dataXml.x_roomId = roomId.ToString();

        // Serializamos con el formato
        serializer.Serialize(stringWriter, dataXml);


        return stringWriter.ToString();
    }
}
