using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class UnityTracker : MonoBehaviour
{
    public static UnityTracker instance;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Tracker.Instance.Init(AnalyticsSessionInfo.sessionId);
        // Eventos correspondientes al juego
        Tracker.Instance.AddTrackableEvent<BloqueoEvent>(true);
        Tracker.Instance.AddTrackableEvent<FinNivelEvent>(true);
        Tracker.Instance.AddTrackableEvent<FinSalaEvent>(true);
        Tracker.Instance.AddTrackableEvent<InicioNivelEvent>(true);
        Tracker.Instance.AddTrackableEvent<InicioSalaEvent>(true);
        Tracker.Instance.AddTrackableEvent<MuerteEnemigoEvent>(true);
        Tracker.Instance.AddTrackableEvent<MuerteJugadorEvent>(true);
        Tracker.Instance.AddTrackableEvent<PosicionJugadorEvent>(true);
        Tracker.Instance.AddTrackableEvent<PosicionNPCEvent>(true);
    }

    // Update is called once per frame
    void Update()
    {
        Tracker.Instance.Update();
    }

    private void OnDestroy()
    {
        if(instance == this)
            Tracker.Instance.Release();
    }
}
