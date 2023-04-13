using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISerializer 
{
    string Serialize(TrackerEvent e);
}

public class JSONSerializer : ISerializer
{
    public string Serialize(TrackerEvent e)
    {
        EventType t = e.GetType();
        // Atributos comunes a todos los eventos
        string aux = "\"TimeStamp\": \"" + e.getTimeStamp().ToString() + "\", \"SessionId\": \"" +
            Tracker.instance.getSessionId().ToString() + "\", \"EventType\": \"" + t.ToString() + "\"";

        // Atributos espec�ficos a cada evento
        switch (t)
        {
            case EventType.MUERTEJUGADOR: case EventType.BLOQUEOBALA:
                aux += ", \"Pos\": \"" + e.getVar().pos.ToString() + "\"";
                break;
            case EventType.INICIOSALA: case EventType.FINSALA:
                aux += ", \"RoomId\": \"" + e.getVar().roomId.ToString() + "\"";
                break;
        }
        return aux;
    }
}
