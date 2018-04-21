using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour {

    public float fallSpeed;
    public Transform targetLife = null;
    public GameObject PowerUpPrefab;
    float fallTime;
    Vector2 startPos;
    Dissolve dissolve;
    bool wasHit;

    private void OnEnable()
    {
        EventManager.onGameEvent += OnGameEvent;
        fallTime = 0f;
        dissolve = GetComponent<Dissolve>();
        wasHit = false;
    }

    private void OnDisable()
    {
        EventManager.onGameEvent -= OnGameEvent;
    }

    private void DissolveDone()
    {
        int rand = UnityEngine.Random.Range(0, 100);
        Debug.Log("Chance to spawn power up: " + rand.ToString());
        if (rand == 50)
        {
            SpawnPowerUp();
        }
        GameManager.Bombs.Remove(gameObject);
    }

    private void SpawnPowerUp()
    {
        GameObject go = Instantiate(PowerUpPrefab, transform.parent) as GameObject;
        go.transform.position = transform.position;
    }

    private void OnGameEvent(EventManager.GameEvent obj)
    {
        if(obj.myType == EventManager.GameEvent.EventType.LifeLost)
        {
            if (targetLife == null)
                return;
            var life = targetLife.gameObject.GetComponent<Life>();
            if (life == null)
            {
                targetLife = null;
                return;
            }
            if (LifeManager.lives.Contains(life) == false)
            {
                if (LifeManager.lives.Count > 0)
                    UpdateTarget();
                else
                    targetLife = null;
            }
        }
    }

    void Start () {
        if(LifeManager.lives.Count > 0)
            UpdateTarget();
        startPos = transform.position;

    }
    void UpdateTarget()
    {
        float shortestDistance = float.MaxValue;
        foreach(var life in LifeManager.lives)
        {
            float distance = (life.transform.position - transform.position).magnitude;
            if(distance < shortestDistance)
            {
                shortestDistance = distance;
                targetLife = life.transform;
            }

        }
    }

    float totalTime = 0f;

    void Update () {
        if (wasHit == true)
            return;
        if(targetLife != null)
        {
            //Vector2 dir = transform.position - targetLife.position;
            fallTime += GameManager.Instance.DeltaTime / fallSpeed;
            totalTime += GameManager.Instance.DeltaTime;
            //float time = fallTime / fallSpeed;
            //Debug.Log("TotalTime: " + totalTime.ToString() + "fallTime: " + fallTime.ToString());
            //transform.position = new Vector2(transform.position.x, transform.position.y) - dir.normalized * fallSpeed * GameManager.Instance.DeltaTime;
            transform.position = Vector2.Lerp(startPos, targetLife.position, fallTime);
        }
        else
        {
            //transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y - fallSpeed * GameManager.Instance.DeltaTime, transform.localPosition.z);
        }
	}

    void OnHit()
    {
        if (wasHit == true)
            return;
        dissolve.StartDissolve();
        wasHit = true;
    }
}
