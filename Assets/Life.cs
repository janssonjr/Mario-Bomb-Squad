using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class Life : MonoBehaviour {

    public int id;
    private Dissolve dissolve;

	void Start () {
        SetUp();
	}

    public void SetUp()
    {
        gameObject.SetActive(true);
        dissolve.SetThreshold(0f);
        //GameManager.Lives.Add(gameObject);
    }

    private void OnEnable()
    {
        dissolve = GetComponent<Dissolve>();
    }

    private void OnDisable()
    {
        dissolve.SetThreshold(1f);
    }

    // Update is called once per frame
    void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Bomb"))
        {
            gameObject.SetActive(false);
            int index = LifeManager.lives.FindIndex((l)=> l.id == id);
            if (index == -1)
            {
                Debug.LogError("Life with Id: "+ id.ToString() + " doesn't exist in LifeManager.lives");
                return;
            }
            gameObject.SetActive(false);
            //GameManager.Lives.Remove(gameObject);
            EventManager.LifeLost();
            Destroy(collision.gameObject);
        }
    }

    public void Recreate()
    {
        dissolve.StartDissolve(1, 0, iTween.EaseType.easeInBack);
        dissolve.DestroyOnComplete(false);
    }
}
