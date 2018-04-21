using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class InputManager : MonoBehaviour {

    public GraphicRaycaster raycaster;
    public LayerMask layerMask;
    public GameObject ballObject;
    private Ball pressedObject;
    private float sizeMultiplier;

    private void OnEnable()
    {
        pressedObject = null;
        sizeMultiplier = 1f;
        EventManager.onGameEvent += OnGameEvent;
    }

    private void OnDisable()
    {
        EventManager.onGameEvent += OnGameEvent;
    }

    private void OnGameEvent(EventManager.GameEvent obj)
    {
        if(obj.myType == EventManager.GameEvent.EventType.BiggerBall)
        {
            sizeMultiplier = obj.myAmountF;
            if(pressedObject != null)
            {
                var rt = pressedObject.GetComponent<RectTransform>();
                rt.sizeDelta = rt.sizeDelta * sizeMultiplier;
            }
            StartCoroutine(ResetSize(obj.myDurration));

        }
    }

    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        bool wasPressed = Input.GetMouseButtonDown(0);
        if (wasPressed == true)
        {
            PointerEventData data = new PointerEventData(null);
            data.position = Input.mousePosition;
            List<RaycastResult> results = new List<RaycastResult>();
            raycaster.Raycast(data, results);

            for (int i = 0; i < results.Count; ++i)
            {
                if(results[i].gameObject.layer == LayerMask.NameToLayer("Ball"))
                {
                    pressedObject = results[i].gameObject.GetComponent<Ball>();
                    pressedObject.OnClicked();
                }
            }
        }
        bool wasReleased = Input.GetMouseButtonUp(0);
        if(wasReleased == true)
        {
            if(pressedObject != null)
            {
                
                bool success = pressedObject.OnReleased();
                if(success == true)
                    SpawnNewBall();
                pressedObject = null;
            }
        }
    }

    void SpawnNewBall()
    {
        GameObject ob = Instantiate(ballObject, pressedObject.transform.parent);
        var rt = ob.GetComponent<RectTransform>();
        rt.sizeDelta = rt.sizeDelta * sizeMultiplier;
    }

    IEnumerator ResetSize(float aDuration)
    {
        yield return new WaitForSeconds(aDuration);
        sizeMultiplier = 1f;
        if(pressedObject != null)
        {
            var rt = pressedObject.GetComponent<RectTransform>();
            rt.sizeDelta = rt.sizeDelta * sizeMultiplier;
        }
    }
}
