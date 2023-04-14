using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
public class FilePersistence : IPersistence
{
    StreamWriter persStream;

    private void Start()
    {
        persStream = new StreamWriter("GameTracked.json");
    }
    public override void Send(TrackerEvent e) { }

    public override void Flush() {
        
    }
}
