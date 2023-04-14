using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
public class JSONSerializer : ISerializer
{
    public override async Task<string> Serialize(TrackerEvent e)
    {
        string t = e.GetEventType();
        // Atributos comunes a todos los eventos
        string aux = "\"TimeStamp\": \"" + e.getTimeStamp().ToString() + "\", \"SessionId\": \"" +
            Tracker.instance.getSessionId().ToString() + "\", \"EventType\": \"" + t + "\"";

        // Atributos específicos a cada evento
        switch (t)
        {
            case "Muerte Enemigo":
                aux += ", \"Pos\": \"" + e.getVar().pos.ToString() + "\"";
                aux += ", \"EnemyID\": \"" + e.getVar().enemyId.ToString() + "\"";
                break;
            case "Muerte Jugador":
            case "Bloqueo":
            case "Posicion Jugador":
            case "Posicion NPC":
                aux += ", \"Pos\": \"" + e.getVar().pos.ToString() + "\"";
                break;
            case "Inicio Sala":
                aux += ", \"RoomId\": \"" + e.getVar().roomId.ToString() + "\"";
                break;
            case "Fin Sala":
                aux += ", \"RoomId\": \"" + e.getVar().roomId.ToString() + "\"";
                break;
        }
        return await Task.FromResult(aux);
    }
}
