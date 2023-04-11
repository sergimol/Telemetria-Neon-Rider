using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public enum EventType { INICIO, FIN, NUEVONIVEL, INICIOPAUSA, FINPAUSA, NIVELCOMPLETADO, GOLPE, MUERTEJUGADOR, MUERTEENEMIGO, BLOQUEOBALA }
public class TrackerEvent
{
    EventType tipo;
    long timeStamp; //  DateTimeOffset.Now.ToUnixTimeSeconds();
    int id_sesion;

    public TrackerEvent(EventType e, int id)
    {
        tipo = e;
        timeStamp = DateTimeOffset.Now.ToUnixTimeSeconds();
        id_sesion = id;
    }

    string toJson()
    {
        string aux = "\"" + timeStamp.ToString() + "\" : \"" + id_sesion.ToString() + "\",\"" + tipo.ToString() + "\""; // Añadir otros atributos
        return aux;
    }

}

public interface IPersistence
{
    void Send(TrackerEvent e) { }

    void Flush() { }
}

public class FilePersistence : IPersistence
{
    public void Send(TrackerEvent e) { }

    public void Flush() { }
}

public class ServerPersistence : IPersistence
{
    public void Send(TrackerEvent e) { }

    public void Flush() { }
}

public class Tracker : MonoBehaviour
{
    public static Tracker instance = null;
    StreamWriter createStream;

    List<TrackerEvent> eventos;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            instance.Init();
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Init()
    {
        createStream = new StreamWriter("GameTracked.json"); // !!! Cambiarlo por llamada a la persistencia
        eventos.Add(new TrackerEvent(EventType.INICIO, 1));
    }

    private void End()
    {
        eventos.Add(new TrackerEvent(EventType.FIN, 1));
        post();

        createStream.Close();
    }

    public void trackCompletable(string s, TrackerEvent e)
    {

    }

    public async void post()
    {
        using (StreamWriter writer = createStream)
        {
            foreach(TrackerEvent e in eventos)
            {
                await createStream.WriteLineAsync(eventos[0].ToString());
            }
            
        }
    }
}
