using System;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.IO;
using Newtonsoft.Json;
using System.Linq;
using System.Collections;
using UnityEngine.Networking;

public sealed class UsabilityTestsSingleton{

    //public List<EventData> gameEventsLocal = new List<EventData>();
    public List<EventData> gameEventsServer = new List<EventData>();

    public List<DateTime> GameEvents { get; set; }

    private static UsabilityTestsSingleton _instance = null;

    private string LogTestsURL = "http://web.tecnico.ulisboa.pt/~ist181633/FaradayMuseum/LogTests/LogTests_POST.php";
    public string LogFileName { get; set; } = "defaultName";

    private UsabilityTestsSingleton()
    {

    }

    public static UsabilityTestsSingleton Instance()
    {
        if (_instance == null){

            _instance = new UsabilityTestsSingleton();
        }
        return _instance;
    }

    public void AddGameEvent(LogEventType eventType)
    {
        gameEventsServer.Add(new EventData(eventType, DateTime.Now));
    }

    public void AddGameEvent(LogEventType eventType, string objectName)
    {
        gameEventsServer.Add(new EventData(eventType, objectName, DateTime.Now));
    }

    public string FileName()
    {
        string name = "";

        name += DateTime.Now.ToString("yyyy-MM-dd_HH-mm");
        name += "_";
        for (int i = 0; i < 7; i++)
        {
            name += UnityEngine.Random.Range(0, 9);
        }
        name += "_";
        for (int i = 0; i < 7; i++)
        {
            name += UnityEngine.Random.Range(0, 9);
        }
        name += ".txt";

        return name;
    }

    public IEnumerator PostRequest()
    {
        string file = "";
        string interaction = "";

        while (true)
        {
            file = "";
            List<IMultipartFormSection> wwwForm = new List<IMultipartFormSection>();

            List<EventData> auxEventData = gameEventsServer;

            foreach (var eventDataItem in auxEventData)
            {
                interaction = "";

                string eventType = "EventType: " + eventDataItem.EventType + " ";
                string eventDate = "EventDate: " + eventDataItem.EventDate + " ";
                string additionalInfo = "AdditionalInfo: " + eventDataItem.ObjectName + " ";

                //interaction = cleaner + eventType + eventDate + additionalInfo + "\n";
                interaction = eventType + eventDate + additionalInfo + "\n";
                file += interaction;
            }

            //Send info
            if (file != "")
            {
                wwwForm.Add(new MultipartFormDataSection("_fileName", LogFileName));
                wwwForm.Add(new MultipartFormDataSection("_content", file));
            }

            gameEventsServer.Clear();

            UnityWebRequest www = UnityWebRequest.Post(LogTestsURL, wwwForm);

            //w8 for answer
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log("Error While Sending: " + www.error);
            }

            yield return new WaitForSeconds(5.0f);
        }
    }
}