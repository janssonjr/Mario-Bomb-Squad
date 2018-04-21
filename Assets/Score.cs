using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour {

    Text scoreText;
    int score;
    int scoreToAdd;

    private void OnEnable()
    {
        score = 0;
        scoreToAdd = 0;
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
            ResetScoreText();
        }
    }

    private void ResetScoreText()
    {
        scoreText.text = score.ToString();
    }

    private void OnGameEvent(EventManager.GameEvent obj)
    {
        if(obj.myType == EventManager.GameEvent.EventType.Score)
        {
            scoreToAdd += obj.myAmount;
            UpdateScoreText();
            ScaleText();
        }
    }

    void UpdateScoreText()
    {
        iTween.StopByName()
        iTween.ValueTo(gameObject, 
            iTween.Hash(
                "from", score, 
                "to", (score + scoreToAdd), 
                "onupdate", "ScoreUpdate", 
                "easetype", iTween.EaseType.easeInCubic, 
                "time", 1f));
    }

    void ScoreUpdate(float newValue)
    {
        scoreText.text = ((int)newValue).ToString();
    }

    void ScaleText()
    {
        Hashtable h = new Hashtable();
        h.Add("time", 1f);
        h.Add("amount", new Vector3(1.5f, 1.5f, 0f));
        iTween.PunchScale(gameObject, h);
    }
}
