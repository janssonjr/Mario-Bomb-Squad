using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

    public GameObject bombPrefab;
    public float spawnDeley;
    bool shouldSpawn;
    Rect rect;

    private void OnEnable()
    {
        EventManager.onStateEvent += OnStateEvent;
    }

    private void OnDisable()
    {
        EventManager.onStateEvent -= OnStateEvent;
    }

    private void OnStateEvent(EventManager.StateEvent obj)
    {
        if (obj.newState == EventManager.StateEvent.StateType.GameOver)
        {
            shouldSpawn = false;
        }
        else if(obj.newState == EventManager.StateEvent.StateType.Playing)
        {

            for(int i = GameManager.Bombs.Count - 1; i >= 0; --i)
            {
                Destroy(GameManager.Bombs[i]);
                GameManager.Bombs.RemoveAt(i);
            }
            shouldSpawn = true;
            StartCoroutine(Spawn());
        }
    }

    void Start () {
        rect = GetComponent<RectTransform>().rect;
        shouldSpawn = true;
        StartCoroutine(Spawn());
    }

    void Update () {
		
	}

    IEnumerator Spawn()
    {
        while (shouldSpawn)
        {
            yield return new WaitForSeconds(spawnDeley);
            if (GameManager.Instance.DeltaMultiplier > 0)
            {
                spawnDeley -= Time.deltaTime * 0.9f;
                spawnDeley = Mathf.Max(spawnDeley, 0.3f);
                Vector2 position = GetPointInSpawnArea();
                GameObject go = (GameObject)Instantiate(bombPrefab, transform);
                go.transform.localPosition = new Vector2(position.x, position.y);
                GameManager.Bombs.Add(go);
                //shouldSpawn = false;
            }
                //go.transform.SetParent(transform);
        }
    }

    Vector2 GetPointInSpawnArea()
    {
        float xpos = UnityEngine.Random.Range(rect.xMin, rect.xMax);
        float ypos = UnityEngine.Random.Range(rect.yMin, rect.yMax);

        return new Vector2(xpos, ypos);
    }
}
