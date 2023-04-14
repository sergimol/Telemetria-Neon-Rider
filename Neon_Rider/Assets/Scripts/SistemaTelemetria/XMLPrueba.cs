using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

//Clase auxiliar para serializar Vector2
public class xmlVector2
{
    string x;
    string y;

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

    // Variables diferentes de cada evento
    public string? x_enemyId { get; set; }
    public string? x_roomId { get; set; }
    public xmlVector2? x_pos { get; set; }

    // Por cada variable de evento hay que espeficiar si se debe omitir cuando es null
    [XmlIgnore]
    public bool ShouldSerializeEnemyId
    {
        get { return !string.IsNullOrEmpty(x_enemyId); }

    }

    [XmlIgnore]
    public bool ShouldSerializeRoomId
    {
        get { return !string.IsNullOrEmpty(x_roomId); }

    }

    [XmlIgnore]
    public bool ShouldSerializePos
    {
        get { return x_pos.IsEmpty(); }

    }

}

public class XMLPrueba : MonoBehaviour
{
    StreamWriter xmlStream;

    // Start is called before the first frame update
    void Start()
    {

        string a = "a";
        string.IsNullOrEmpty(a);

        TrackerEvent e = new TrackerEvent("Muerte Jugador",  new possibleVar {pos = new Vector2(1,1)});

        DataXmlObject dataXml = new DataXmlObject
        {
            x_type = e.GetType().ToString(),
            x_time = e.getTimeStamp().ToString(),
            x_enemyId = e.getVar().enemyId.ToString(),
            x_pos = new xmlVector2(e.getVar().pos.Value.x, e.getVar().pos.Value.y),
            x_roomId =e.getVar().roomId.ToString() 
        };

        XmlSerializer serializer = new XmlSerializer(typeof(DataXmlObject));
        xmlStream = new StreamWriter("Prueba.xml");
        serializer.Serialize(xmlStream, dataXml);


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
