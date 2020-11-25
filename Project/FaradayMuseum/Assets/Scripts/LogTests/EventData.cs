using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using UnityEngine;

[Serializable]
public class EventData{

    [JsonConverter(typeof(StringEnumConverter))]
    public LogEventType EventType { get; set; }

    public DateTime EventDate { get; set; }

    public string ObjectName { get; set; }


    public EventData(LogEventType eventType, DateTime dateTime)
    {
        EventType = eventType;
        EventDate = dateTime;
        ObjectName = "";
    }

    public EventData(LogEventType eventType, string objectName, DateTime dateTime)
    {
        EventType = eventType;
        EventDate = dateTime;
        ObjectName = objectName;
    }
}

public enum LogEventType
{
    Click,

    NoActionClick,

    Drag,

    AchivementUnlocked,

    TrackingTarget,

    //Book
    OnAppLoad,
    OpenBook,
    BookSwipeLeft,
    BookSwipeRight,
    ObjectClick,
    ChangeScene,
    Info1,
    Info2,
    Info3,
    InfoClose,

    //Telephone

    CallButton,
    BatteryPower,
    BatteryPlaced,
    BatteryOnTrigger,
    BatteryCharged,

    Motor,
    Magnet,
    Headphones,
    Handheld,

    TopDoor,
    MiddleDoor,
    BottomDoor,

    // Cathode Ray
    Rotating,
    Tension,
    Intensity,
    Rotation,
    Warning
}
