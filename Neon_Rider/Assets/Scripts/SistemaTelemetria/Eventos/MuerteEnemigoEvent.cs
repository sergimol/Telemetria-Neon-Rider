using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;

public class MuerteEnemigoXML : DataXML
{
    public string x_pos;
    public string x_enemyId;
}

public class MuerteEnemigoEvent : TrackerEvent
{
    // Atributos del evento
    Vector2 pos;
    int enemyId;

    // Serializacion en JSON
    public override string toJSON()
    {
        string cadena = base.toJSON();
        cadena += ", \"EnemyID\": \"" + enemyId.ToString() + "\"";
        cadena += ", \"Pos\": \"" + pos.ToString() + "\"";
        return cadena;
    }

    // Serializacion en CSV
    public override string toCSV()
    {
        string cadena = base.toCSV();
        cadena += "EnemyID," + enemyId.ToString() + ",";
        cadena += "Pos," + "\"" + pos.x.ToString() + "\"" + "," + "\"" + pos.y.ToString() + "\"" + ",";
        return cadena;
    }

    // Serializacion en XML
    public override string toXML()
    {
        // Creacion del objeto DataXml
        MuerteEnemigoXML dataXml = new MuerteEnemigoXML();
        StringWriter stringWriter = new StringWriter();
        XmlSerializer serializer = new XmlSerializer(typeof(MuerteEnemigoXML));

        // Atributos comunes a todos los eventos
        dataXml.x_type = type;
        dataXml.x_timeStamp = timeStamp.ToString();
        dataXml.x_sessionID = Tracker.instance.getSessionId().ToString();

        // Atributos de evento
        dataXml.x_enemyId = enemyId.ToString();
        dataXml.x_pos = pos.ToString();

        // Serializamos con el formato
        serializer.Serialize(stringWriter, dataXml);


        return stringWriter.ToString();
    }
}
