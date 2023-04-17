using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;

public class NivelXML : DataXML
{
    public string x_levelId;
}

public class InicioNivelEvent : TrackerEvent
{
    // Atributos del evento
    int levelId;

    public InicioNivelEvent(int rId) : base("InicioNivel")
    {
        levelId = rId;
    }

    // Serializacion en JSON
    public override string toJSON()
    {
        string cadena = base.toJSON();
        cadena += ", \"LevelID\": \"" + levelId.ToString() + "\"},";
        return cadena;
    }

    // Serializacion en CSV
    public override string toCSV()
    {
        string cadena = base.toCSV();
        cadena += "LevelID," + levelId.ToString() + ",";
        return cadena;
    }
    // Serializacion en XML
    public override string toXML()
    {
        // Creacion del objeto DataXml
        NivelXML dataXml = new NivelXML();
        StringWriter stringWriter = new StringWriter();
        XmlSerializer serializer = new XmlSerializer(typeof(NivelXML));

        // Atributos comunes a todos los eventos
        dataXml.x_type = type;
        dataXml.x_timeStamp = timeStamp.ToString();
        dataXml.x_sessionID = Tracker.instance.getSessionId().ToString();

        // Atributos de evento
        dataXml.x_levelId = levelId.ToString();

        // Serializamos con el formato
        serializer.Serialize(stringWriter, dataXml);


        return stringWriter.ToString();
    }
}
