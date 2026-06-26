using System;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "DSP_SceneEvent", menuName = "DSP/Scene Event")]
public class DSP_SceneEvent : ScriptableObject
{
    private event Func<bool> _event;

    public void Subscribe(Func<bool> callback)
    {
        _event += callback;
    }

    public void Unsubscribe(Func<bool> callback)
    {
        _event -= callback;
    }

    public void Raise()
    {
        if (_event != null)
        {
            bool result = _event.GetInvocationList().Cast<Func<bool>>().Any(callback => callback());

            if (!result)
            {
                Debug.LogWarning($"Event {name} was raised but no subscribers returned successfully");
            }
            else
            {
                Debug.Log($"invoked to targets: {_event.GetInvocationList().Length}");
                foreach (var callback in _event.GetInvocationList())
                {
                    Debug.Log("Invoking on: " + callback.Target);
                }
            }
        }
        else
        {
            Debug.LogWarning($"Event {name} was raised but has no subscribers");
        }
    }
}
