using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

//Clase auxiliar para serializar Vector2
public class xmlVector2
{
    public string x;
    public string y;

    public bool IsEmpty()
    {
        return string.IsNullOrEmpty(x) && string.IsNullOrEmpty(y);
    }

    public xmlVector2(float x_, float y_)
    {
        x = x_.ToString();
        y = y_.ToString();
    }
    public xmlVector2() { }
}

// Objeto preset para serializar xml
// Tiene todos los par�metros de todos los eventos
public class DataXmlObject
{
    // Variables comunes a todos los eventos
    public string x_type;
    public string x_time;
    public string x_sessionId;

    // Variables diferentes de cada evento
    public string? x_enemyId { get; set; }
    public string? x_roomId { get; set; }
    public xmlVector2? x_pos { get; set; }

    // Por cada variable de evento hay que espeficiar si se debe omitir en la serializacion cuando es null
    [XmlIgnore]
    public bool ShouldSerializePos { get { return x_pos.IsEmpty(); } }
    [XmlIgnore]
    public bool ShouldSerializeEnemyId { get { return !string.IsNullOrEmpty(x_enemyId); } }

    [XmlIgnore]
    public bool ShouldSerializeRoomId { get { return !string.IsNullOrEmpty(x_roomId); } }



}

public class XMLSerializer : ISerializer
{
    public override string Serialize(TrackerEvent e)
    {
        string cadena = e.toXML();

        // La devolvemos como string
        return cadena;
    }
}