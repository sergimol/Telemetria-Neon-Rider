using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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