using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EventManager : MonoBehaviour {

    public static Action<GameEvent> onGameEvent;
    public static Action<StateEvent> onStateEvent;
    static EventManager instance;

    public class StateEvent
    {
        public enum StateType
        {
            Playing,
            GameOver
        }

        public StateType newState;
        public StateType oldState;
    }

    public static void GameOver()
    {
        if (onStateEvent != null)
            onStateEvent.Invoke(new StateEvent { newState = StateEvent.StateType.GameOver, oldState = StateEvent.StateType.Playing});
    }

    public static void Retry()
    {
        if (onStateEvent != null)
            onStateEvent.Invoke(new StateEvent { newState = StateEvent.StateType.Playing, oldState = StateEvent.StateType.GameOver });
    }

    public class GameEvent
    {
        public enum EventType
        {
            Score,
            LifeLost
        }

        public EventType myType;
        public int myScore;
    }

    public static void LifeLost()
    {
        if (onGameEvent != null)
            onGameEvent.Invoke(new GameEvent { myType = GameEvent.EventType.LifeLost });
    }

    public static void Score(int aScore)
    {
        if (onGameEvent != null)
            onGameEvent.Invoke(new GameEvent { myType = GameEvent.EventType.Score, myScore = aScore});
    }
}
