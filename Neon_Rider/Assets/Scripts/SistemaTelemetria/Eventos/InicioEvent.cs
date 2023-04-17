using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;


public class InicioEvent : TrackerEvent
{

    // Serializacion en JSON
    public override string toJSON()
    {
        string cadena = "[" + base.toJSON() + "},";
        return cadena;
    }

    // Serializacion en CSV
    public override string toCSV()
    {
        string cadena = base.toCSV();
        return cadena;
    }

    // Serializacion en XML
    public override string toXML()
    {
        string cadena = base.toXML();
        return cadena;
    }
}
