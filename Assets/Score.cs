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
        UpdateScoreText();
    }

    private void OnDisable()
    {
        EventManager.onGameEvent -= OnGameEvent;
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
