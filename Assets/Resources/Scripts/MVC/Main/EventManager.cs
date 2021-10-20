using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : Singleton<EventManager>
{
    private Dictionary<string, Action<object>> eventList = new Dictionary<string, Action<object>>();

    //注册事件
    public void RegisterListener(string name, Action<object> action)
    {
        if (eventList.ContainsKey(name))
        {
            eventList[name] += action;
        }
        else
        {
            eventList.Add(name,action);
        }
    }
    
    //移除事件
    public void RemoveListener(string name, Action<object> action)
    {
        if (eventList.ContainsKey(name))
        {
            eventList[name] -= action;
            if (eventList[name] == null)
            {
                eventList.Remove(name);
            }
        }
    }

    //触发事件
    public void PostNotification(string name, object obj)
    {
        if (eventList.ContainsKey(name))
        {
            eventList[name](obj);
        }   
    }
    
}
