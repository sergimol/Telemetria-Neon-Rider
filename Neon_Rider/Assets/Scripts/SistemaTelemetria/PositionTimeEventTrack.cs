using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionTimeEventTrack : MonoBehaviour
{
    [Header("Tiempo de Muestreo")]
    [SerializeField]
    float time_;

    [Header("Jugador o NPC")]
    [SerializeField]
    bool player;

    private float actualTime;


    // Start is called before the first frame update
    void Start()
    {
        actualTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        actualTime += Time.deltaTime;
        if (actualTime > time_)
        {
            actualTime = 0;
            if (!player)
                Tracker.instance.AddEvent("Posicion NPC", new possibleVar { pos = transform.position });
            else
                Tracker.instance.AddEvent("Posicion Jugador", new possibleVar { pos = transform.position });
            //Debug.Log("PosicionXTiempo");
        }
    }
}
