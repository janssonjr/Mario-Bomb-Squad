using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour {

    Text scoreText;
    int score;

    private void OnEnable()
    {
        score = 0;
        scoreText = GetComponent<Text>();
        EventManager.onGameEvent += OnGameEvent;
        EventManager.onStateEvent += OnStateEvent;
        UpdateScoreText();
    }

    private void OnDisable()
    {
        EventManager.onGameEvent -= OnGameEvent;
        EventManager.onStateEvent -= OnStateEvent;
    }

    private void OnStateEvent(EventManager.StateEvent obj)
    {
        if(obj.newState == EventManager.StateEvent.StateType.Playing)
        {
            score = 0;
            UpdateScoreText();
        }
    }

    private void OnGameEvent(EventManager.GameEvent obj)
    {
        if(obj.myType == EventManager.GameEvent.EventType.Score)
        {
            score += obj.myScore;
            UpdateScoreText();
        }
    }

    void UpdateScoreText()
    {
        scoreText.text = "Score: " + score.ToString();
    }

    void Start () {
		
	}
	
	void Update () {
		
	}
}
