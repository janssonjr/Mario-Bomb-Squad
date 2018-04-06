using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

    public GameObject bombPrefab;
    Rect rect;
	// Use this for initialization
	void Start () {
        rect = GetComponent<RectTransform>().rect;
        StartCoroutine(Spawn());
    }


    // Update is called once per frame
    void Update () {
		
	}

    IEnumerator Spawn()
    {
        while (true)
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
        float xpos = Random.Range(rect.xMin, rect.xMax);
        float ypos = Random.Range(rect.yMin, rect.yMax);

        return new Vector2(xpos, ypos);
    }
}
