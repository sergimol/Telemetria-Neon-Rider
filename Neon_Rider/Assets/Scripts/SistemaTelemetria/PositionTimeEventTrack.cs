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
    Enemy_Death enemy_Death;

    // Start is called before the first frame update
    void Start()
    {
        actualTime = 0;
        enemy_Death = GetComponent<Enemy_Death>();
    }

    // Update is called once per frame
    void Update()
    {
        actualTime += Time.deltaTime;
        if (actualTime > time_)
        {
            actualTime = 0;
            if (!player)
                Tracker.instance.AddEvent(new PosicionNPCEvent(transform.position, enemy_Death.getId()));
            else
                Tracker.instance.AddEvent(new PosicionJugadorEvent(transform.position));
        }
    }
}
