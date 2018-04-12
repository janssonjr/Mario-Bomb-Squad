using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static List<GameObject> lives = new List<GameObject>();

    private void OnEnable()
    {
        EventManager.onGameEvent += OnGameEvent;
        EventManager.onStateEvent += OnStateEvent;
    }

    private void OnDisable()
    {
        EventManager.onGameEvent -= OnGameEvent;
        EventManager.onStateEvent -= OnStateEvent;
    }

    private void OnStateEvent(EventManager.StateEvent obj)
    {
        if(obj.newState == EventManager.StateEvent.StateType.Playing && obj.oldState == EventManager.StateEvent.StateType.GameOver)
        {

        }
    }

    private void OnGameEvent(EventManager.GameEvent obj)
    {
        if(obj.myType == EventManager.GameEvent.EventType.LifeLost)
        {
            if(lives.Count <= 0)
            {
                EventManager.GameOver();
                GameOver();
            }
        }
    }

    private void GameOver()
    {

    }
}
