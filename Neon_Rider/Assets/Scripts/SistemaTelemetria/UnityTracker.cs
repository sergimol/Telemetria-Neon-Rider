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
