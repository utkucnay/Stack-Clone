using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static class Observer
{
    public static void RegisterEventFromRoot<T>(Transform root, string methodName, UnityEvent uEvent) where T : IEvent
    {
        var events = root.GetComponentsInChildren<T>();
        foreach (var _event in events)
        {
            var action = (UnityAction)UnityAction.CreateDelegate(typeof(UnityAction), _event, methodName);
            uEvent.AddListener(action);
        }
    }

    public static void RegisterEventFromTransform<T>(Transform transform, string methodName, UnityEvent uEvent) where T : IEvent
    {
        var _event = transform.GetComponent<T>();
        if (_event == null) return;
        var action = (UnityAction)UnityAction.CreateDelegate(typeof(UnityAction), _event, methodName);
        uEvent.AddListener(action);
    }

    public static void RegisterEventFromRoot<T, TEventType>(Transform root, string methodName, UnityEvent<TEventType> uEvent) where T : IEventWithParam
    {
        var events = root.GetComponentsInChildren<T>();
        foreach (var _event in events)
        {
            var action = (UnityAction<TEventType>)UnityAction.CreateDelegate(typeof(UnityAction<TEventType>), _event, methodName);
            uEvent.AddListener(action);
        }
    }

    public static void RegisterEventFromAllGameObjects<T>(string methodName, UnityEvent uEvent) where T : IEvent
    {
        var objects = (MonoBehaviour[])GameObject.FindObjectsOfType(typeof(MonoBehaviour));
        List<T> events = new List<T>();
        foreach (var _object in objects)
        {
            var _T = _object.GetComponent<T>();
            if (_T == null) continue;
            events.Add(_T);
        }
        foreach (var _event in events)
        {
            var action = (UnityAction)UnityAction.CreateDelegate(typeof(UnityAction), _event, methodName);
            uEvent.AddListener(action);
        }
    }
}