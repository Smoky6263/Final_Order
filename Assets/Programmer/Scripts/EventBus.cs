using System;
using UnityEngine;
using System.Collections.Generic;

public class EventBus
{
    public readonly Dictionary<string, List<object>> _signalCallbacks = new Dictionary<string, List<object>>();
    
    public void Subscribe<T>(Action<T> callback)
    {
        string key = typeof(T).Name;
        if(_signalCallbacks.ContainsKey(key))
            _signalCallbacks[key].Add(callback);
        else
            _signalCallbacks.Add(key, new List<object>() { callback });
    }
    
    public void Unsubscribe<T>(Action<T> callback)
    {
        string key = typeof(T).Name;
        if (_signalCallbacks.ContainsKey(key))
            _signalCallbacks[key].Remove(callback);
        else
            Debug.LogError($"������� ��������� �� {typeof(T).Name}, ������� �� ���������������!");
    }

    public void Invoke<T>(T signal)
    {
        string key = typeof(T).Name;
        if (_signalCallbacks.ContainsKey(key))
        {
            foreach (var obj in _signalCallbacks[key])
            {
                var callback = obj as Action<T>;
                callback?.Invoke(signal);
            }
        }
    }
}
