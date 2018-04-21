using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    //public static List<GameObject> Lives = new List<GameObject>();
    public static List<GameObject> Bombs = new List<GameObject>();
    private float deltaTime;
    private float deltaMultiplier;

    private static GameManager instance = null;


    public static GameManager Instance
    {
        get
        { 
            return instance;
        }
    }

    public float DeltaTime
    {
        get { return deltaTime * deltaMultiplier; }
    }

    public float DeltaMultiplier
    {
        set { deltaMultiplier = value; }
        get { return deltaMultiplier; }
    }

    private void OnEnable()
    {
        instance = this;
        deltaMultiplier = 1f;
        EventManager.onGameEvent += OnGameEvent;
        EventManager.onStateEvent += OnStateEvent;
    }

    private void OnDisable()
    {
        EventManager.onGameEvent -= OnGameEvent;
        EventManager.onStateEvent -= OnStateEvent;
    }

    private void Update()
    {
        deltaTime = Time.deltaTime;
    }

    private void OnStateEvent(EventManager.StateEvent obj)
    {
        if(obj.newState == EventManager.StateEvent.StateType.Playing && obj.oldState == EventManager.StateEvent.StateType.GameOver)
        {

        }
    }

    private void OnGameEvent(EventManager.GameEvent obj)
    {
        //if(obj.myType == EventManager.GameEvent.EventType.LifeLost)
        //{
        //    if(Lives.Count <= 0)
        //    {
        //        EventManager.GameOver();
        //    }
        //}
    }

    public IEnumerator ResetDeltaMultiplier(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        deltaMultiplier = 1f;
    }

    public void StartResetDeltaMultiplier(float freezeTimer)
    {
        StartCoroutine(ResetDeltaMultiplier(freezeTimer));
    }
}
