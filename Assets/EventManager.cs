using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EventManager : MonoBehaviour {

    public static Action<GameEvent> onGameEvent;
    static EventManager instance;

    public class GameEvent
    {
        public enum EventType
        {
            Score
        }

        public EventType myType;
        public int myScore;
    }



    public static void Score(int aScore)
    {
        if (onGameEvent != null)
            onGameEvent.Invoke(new GameEvent { myType = GameEvent.EventType.Score, myScore = aScore});
    }
}
