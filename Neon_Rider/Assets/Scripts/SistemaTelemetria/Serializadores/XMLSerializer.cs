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
// Tiene todos los parámetros de todos los eventos
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
        //string t = e.GetEventType();

        //// Creacion del objeto DataXml
        //DataXmlObject dataXml = new DataXmlObject();
        //StringWriter stringWriter = new StringWriter();
        //XmlSerializer serializer = new XmlSerializer(typeof(DataXmlObject));

        //// Atributos comunes a todos los eventos
        //dataXml.x_type = t;
        //dataXml.x_time = e.getTimeStamp().ToString();
        //dataXml.x_sessionId = Tracker.instance.getSessionId().ToString();

        //// Atributos específicos a cada evento
        //switch (t)
        //{
        //    case "Muerte Enemigo":
        //        dataXml.x_pos = new xmlVector2(e.getVar().pos.Value.x, e.getVar().pos.Value.y);
        //        dataXml.x_enemyId = e.getVar().enemyId.ToString();
        //        break;
        //    case "Muerte Jugador":
        //    case "Bloqueo":
        //    case "Posicion Jugador":
        //    case "Posicion NPC":
        //        dataXml.x_pos = new xmlVector2(e.getVar().pos.Value.x, e.getVar().pos.Value.y);
        //        break;
        //    case "Inicio Sala":
        //        dataXml.x_roomId = e.getVar().roomId.ToString();
        //        break;
        //    case "Fin Sala":
        //        dataXml.x_roomId = e.getVar().roomId.ToString();
        //        break;
        //}

        //// Lo serializamos con el formato en una variable
        //serializer.Serialize(stringWriter, dataXml);

        string cadena = e.toXML();

        // La devolvemos como string
        return cadena;
    }
}
