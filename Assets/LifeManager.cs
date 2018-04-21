using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeManager : MonoBehaviour {

   public static List<Life> lives = new List<Life>();

    private void OnEnable()
    {
        for(int i = 0; i < transform.childCount; ++i)
        {
            var life = transform.GetChild(i).GetComponent<Life>();
            lives.Add(life);
            life.id = i;
            
        }
        EventManager.onStateEvent += OnStateEvent;
        EventManager.onGameEvent += OnGameEvent;
    }

    private void OnDisable()
    {
        EventManager.onStateEvent -= OnStateEvent;
        EventManager.onGameEvent -= OnGameEvent;
    }

    private void OnStateEvent(EventManager.StateEvent obj)
    {
        if (obj.newState == EventManager.StateEvent.StateType.Playing)
        {
            foreach(Life life in lives)
            {
                life.SetUp();

            }
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.L))
        {
            lives[0].gameObject.SetActive(!lives[0].gameObject.activeSelf);
        }
    }

    bool HasLivesLeft()
    {
        foreach(var life in lives)
        {
            if (life.gameObject.activeSelf == true)
                return true;
        }
        return false;
    }

    private void OnGameEvent(EventManager.GameEvent obj)
    {
        if (obj.myType == EventManager.GameEvent.EventType.LifeLost)
        {
            if (HasLivesLeft() == false)
            {
                EventManager.GameOver();
            }
        }
        else if(obj.myType == EventManager.GameEvent.EventType.LifeGained)
        {
            foreach(var life in lives)
            {
                if (life.gameObject.activeSelf == false)
                {
                    life.gameObject.SetActive(true);
                    life.Recreate();
                    return;
                }
            }

        }
    }
}
