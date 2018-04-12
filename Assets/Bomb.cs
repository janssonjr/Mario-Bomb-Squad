using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour {

    public float fallSpeed;
    public Transform targetLife = null;

    private void OnEnable()
    {
        EventManager.onGameEvent += OnGameEvent;
    }

    private void OnDisable()
    {
        EventManager.onGameEvent -= OnGameEvent;
    }

    private void OnGameEvent(EventManager.GameEvent obj)
    {
        if(obj.myType == EventManager.GameEvent.EventType.LifeLost)
        {
            if(GameManager.lives.Contains(targetLife.gameObject) == false)
            {
                if (GameManager.lives.Count > 0)
                    UpdateRandomTarget();
                else
                    targetLife = null;
            }
        }
    }

    void Start () {
        if(GameManager.lives.Count > 0)
            UpdateRandomTarget();
	}
    void UpdateRandomTarget()
    {
        targetLife = GameManager.lives[UnityEngine.Random.Range(0, GameManager.lives.Count - 1)].transform;
    }

    void Update () {
        if(targetLife != null)
        {
            Vector2 dir = transform.position - targetLife.position;
            transform.position = new Vector2(transform.position.x, transform.position.y) - dir.normalized * fallSpeed * Time.deltaTime;
        }
        else
        {
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y - fallSpeed * Time.deltaTime, transform.localPosition.z);
        }
	}
}
