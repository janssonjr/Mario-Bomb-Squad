using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour {

    public List<Panel> panels = new List<Panel>();

    static CanvasManager instance;


    GraphicRaycaster raycaster;

    private void Awake()
    {
        instance = this;
        raycaster = GetComponent<GraphicRaycaster>();
    }

    public CanvasManager Instance
    {
        get
        {
            if (instance == null)
                instance = this;
            return instance;
        }
    }

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
            panels[(int)PanelEnum.GameOverPanel].gameObject.SetActive(true);
        else if(obj.newState == EventManager.StateEvent.StateType.Playing && obj.oldState == EventManager.StateEvent.StateType.GameOver)
            panels[(int)PanelEnum.GameOverPanel].gameObject.SetActive(false);
    }

    // Use this for initialization
    void Start () {
		
	}

    public static GraphicRaycaster GetRaycaster()
    {
        return instance.raycaster;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
