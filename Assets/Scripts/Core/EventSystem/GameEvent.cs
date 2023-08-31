using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameEvent", menuName = "Event/GameEvent", order = 1)]
public class GameEvent : ScriptableObject
{
    private event Action<GameEvent> _onEventTriggered;

    [ContextMenu("Invoke")]
    public void Invoke()
    {
        _onEventTriggered?.Invoke(this);
    }
    public void Subscribe(Action<GameEvent> cbk)
    {
        _onEventTriggered += cbk;
        GameEventSystem.Instance.addHotEvent(this);
    }
    public void Unsubscribe(Action<GameEvent> cbk)
    {
        _onEventTriggered -= cbk;
        if (_onEventTriggered == null)
        {
            GameEventSystem.Instance.removeHotEvent(this);
        }
    }

    public void Clear()
    {
        foreach (Delegate del in _onEventTriggered.GetInvocationList())
        {
            _onEventTriggered -= (Action<GameEvent>)del;
        }
    }
}
