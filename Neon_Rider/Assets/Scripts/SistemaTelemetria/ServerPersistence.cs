using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ServerPersistence : IPersistence
{

    public override void Send(TrackerEvent e) { }

    public override void Flush() { }

    private void OnDestroy()
    {
        
    }
}