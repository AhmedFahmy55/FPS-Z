using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Int event",menuName = "GameEvents/IntEvent")]
public class IntEvent : ScriptableObject
{
    private event Action<int> OnEvent;



    public void Subscribe(Action<int> action)
    {
        OnEvent += action;
    }

    public void Unsubscribe(Action<int> action)
    {
        OnEvent -= action;

    }

    public void Invoke(int value)
    {
        OnEvent?.Invoke(value);
    }
}
