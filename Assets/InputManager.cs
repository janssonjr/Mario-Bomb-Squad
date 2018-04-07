using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InputManager : MonoBehaviour {

    public GraphicRaycaster raycaster;
    public LayerMask layerMask;
    public GameObject ballObject;
    private Ball pressedObject;

    private void OnEnable()
    {
        pressedObject = null;
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
                pressedObject.OnReleased();
                SpawnNewBall();
                pressedObject = null;
            }

        }
    }

    void SpawnNewBall()
    {
        GameObject newBall = Instantiate(ballObject, pressedObject.transform.parent) as GameObject;
    }
}
