using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeManager : MonoBehaviour {

    List<Life> lives = new List<Life>();

    private void OnEnable()
    {
        for(int i = 0; i < transform.childCount; ++i)
        {
            lives.Add(transform.GetChild(i).GetComponent<Life>());
        }
        EventManager.onStateEvent += OnStateEvent;
    }

    private void OnDisable()
    {
        EventManager.onStateEvent -= OnStateEvent;
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
}
