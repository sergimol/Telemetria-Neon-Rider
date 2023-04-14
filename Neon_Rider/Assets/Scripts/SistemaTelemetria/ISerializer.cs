using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ISerializer : MonoBehaviour
{
    public abstract string Serialize(TrackerEvent e);
}

