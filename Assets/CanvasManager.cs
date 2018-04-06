using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour {

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
