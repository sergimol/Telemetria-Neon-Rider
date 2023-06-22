//using System.Collections;
//using System.Collections.Generic;
//using Firebase;
//using Firebase.Database;
//using UnityEngine;
//using System.IO;
//using System.Net.Security;
//using System.Runtime.ConstrainedExecution;
//using System;

//public class ServerPersistence : IPersistence
//{

//    List<TrackerEvent> eventsBuff;

//    [SerializeField]
//    string apiKey = "Api Key";
//    [SerializeField]
//    string url = "URL";
//    [SerializeField]
//    string appId = "App ID";
//    [SerializeField]
//    string storageBucket = "Storage Bucket";
//    [SerializeField]
//    string messageSenderId = "Message Sender ID";
//    [SerializeField]
//    string projectId = "Project ID";

//    JSONSerializer serializerJSON = null;   //JSON
//    DatabaseReference reference;

//    private int counterEvents = 0;

//    string idOrdenador = "NoId";

//    private void Awake()
//    {
//        idOrdenador = SystemInfo.deviceUniqueIdentifier;

//        AppOptions options = new AppOptions
//        {
//            ApiKey = apiKey,
//            DatabaseUrl = new System.Uri(url),
//            AppId = appId,
//            StorageBucket = storageBucket,
//            MessageSenderId = messageSenderId,
//            ProjectId = projectId
//        };
//        FirebaseApp app = FirebaseApp.Create(options);
        
//        reference = FirebaseDatabase.DefaultInstance.RootReference;

//        eventsBuff = new();

//        serializerJSON = GetComponent<JSONSerializer>();
//    }

//    public override void Send(TrackerEvent e) {
//        eventsBuff.Add(e);
//    }

//    public override void Flush() {
//        List<TrackerEvent> events = new List<TrackerEvent>(eventsBuff);
//        eventsBuff.Clear();
//        WriteAsync(events);
//        reference.Push();
//    }

//    private void OnDestroy()
//    {
//    }
//    private async void WriteAsync(List<TrackerEvent> events)
//    {
//        foreach (TrackerEvent e in events)
//        {
//            string ser = serializerJSON.Serialize(e);
//            await reference.Child("PcId: " + idOrdenador).Child("SessionId: " + Tracker.instance.getSessionId().ToString()).Child(counterEvents.ToString()).SetRawJsonValueAsync(ser.TrimStart('['));
//            counterEvents++;
//        }
//    }
//}