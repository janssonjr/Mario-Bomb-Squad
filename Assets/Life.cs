using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Life : MonoBehaviour {

	// Use this for initialization
	void Start () {
        SetUp();
	}

    public void SetUp()
    {
        gameObject.SetActive(true);
        GameManager.lives.Add(gameObject);
    }

    private void OnEnable()
    {
    }

    private void OnDisable()
    {
    }

    // Update is called once per frame
    void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Bomb"))
        {
            gameObject.SetActive(false);
            GameManager.lives.Remove(gameObject);
            EventManager.LifeLost();
            Destroy(collision.gameObject);
        }
    }

    
}
