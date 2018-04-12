using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

    public GameObject bombPrefab;
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

            for(int i = transform.childCount - 1; i >= 0; --i)
            {
                Destroy(transform.GetChild(i).gameObject);
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
            yield return new WaitForSeconds(2);

            Vector2 position = GetPointInSpawnArea();
            GameObject go = (GameObject)Instantiate(bombPrefab, transform);
            go.transform.localPosition = new Vector2(position.x, position.y);
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
