using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventSystem : Singleton<GameEventSystem>, ISystem
{
    [SerializeField]
    private int _Priority;
    public int Priority { get => _Priority; }

    private HashSet<GameEvent> _HotEvents;

    public void Setup() {
        _HotEvents = new HashSet<GameEvent>();
        SystemCoordinator.Instance.FinishSystemSetup(this);
    }

    public void addHotEvent(GameEvent gameEvent) {
        _HotEvents.Add(gameEvent);
    }
    public void removeHotEvent(GameEvent gameEvent) {
        _HotEvents.Remove(gameEvent);
    }

}
