using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Xml.Serialization;
using UnityEngine;

public class CSVSerializer : ISerializer
{
    public override string Serialize(TrackerEvent e)
    {
        string t = e.GetEventType();
        // Atributos comunes a todos los eventos
        string aux = "TimeStamp," + e.getTimeStamp() + "," + "EventType," + t + "," + "SessionId," + Tracker.instance.getSessionId().ToString() + ",";

        // Atributos específicos a cada evento
        switch (t)
        {
            case "Muerte Enemigo":
                aux += "Pos," + e.getVar().pos.Value.x.ToString() + "," + e.getVar().pos.Value.y.ToString() + ",";
                aux += "EnemyID," + e.getVar().enemyId.ToString() + ",";
                break;
            case "Muerte Jugador":
            case "Bloqueo":
            case "Posicion Jugador":
            case "Posicion NPC":
                aux += "Pos," + e.getVar().pos.Value.x.ToString() + "," + e.getVar().pos.Value.y.ToString() + ",";
                break;
            case "Inicio Sala":
                aux += "RoomID," + e.getVar().roomId.ToString() + ",";
                break;
            case "Fin Sala":
                aux += "RoomID," + e.getVar().roomId.ToString() + ",";
                break;
        }

        // La devolvemos como string
        return aux;
    }
}
