using AYellowpaper.SerializedCollections;
using FMODUnity;
using System.Collections.Generic;
using UnityEngine;

public class EventBusManager : MonoBehaviour
{
    public EventBus EventBus { get; } = new();
}
