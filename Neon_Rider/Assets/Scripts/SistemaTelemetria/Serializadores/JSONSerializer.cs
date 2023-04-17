using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
public class JSONSerializer : ISerializer
{
    public override string Serialize(TrackerEvent e)
    {
        //string t = e.GetEventType();
        //// Atributos comunes a todos los eventos
        //string aux = "{\"TimeStamp\": \"" + e.getTimeStamp().ToString() + "\", \"SessionId\": \"" +
        //    Tracker.instance.getSessionId().ToString() + "\", \"EventType\": \"" + t + "\"";

        // Atributos específicos a cada evento
        //switch (t)
        //{
        //    case "Inicio":
        //        aux = "[" + aux;
        //        break;
        //    case "Muerte Enemigo":
        //        aux += ", \"Pos\": \"" + e.getVar().pos.ToString() + "\"";
        //        aux += ", \"EnemyID\": \"" + e.getVar().enemyId.ToString() + "\"";
        //        break;
        //    case "Muerte Jugador":
        //    case "Bloqueo":
        //    case "Posicion Jugador":
        //    case "Posicion NPC":
        //        aux += ", \"Pos\": \"" + e.getVar().pos.ToString() + "\"";
        //        break;
        //    case "Inicio Sala":
        //        aux += ", \"RoomId\": \"" + e.getVar().roomId.ToString() + "\"";
        //        break;
        //    case "Fin Sala":
        //        aux += ", \"RoomId\": \"" + e.getVar().roomId.ToString() + "\"";
        //        break;
        //}
        string cadena = e.toJSON();

        cadena += "}";

        if (e.GetEventType() != "Fin")
            cadena += ",";
        else cadena += "]";

        return cadena;
    }
}
