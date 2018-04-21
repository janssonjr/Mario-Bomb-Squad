using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour {

    public enum PowerUpType
    {
        DestroyAll,
        Freeze,
        Refill1Life,
        //BiggerBall,

        Length
    }

    public float fallSpeed;
    public float freezeTimer;
    PowerUpType myType;

    private void OnEnable()
    {
        myType = (PowerUpType)UnityEngine.Random.Range(0, (int)PowerUpType.Length);
    }

    // Update is called once per frame
    void Update () {
        transform.position -= new Vector3(0, fallSpeed, 0) * Time.deltaTime;
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Ball"))
        {
            ActivateEffect();
            Destroy(gameObject);
        }
    }

    private void ActivateEffect()
    {
        switch (myType)
        {
            case PowerUpType.DestroyAll:
                DestroyAll();
                break;
            case PowerUpType.Freeze:
                Freeze();
                break;
            case PowerUpType.Refill1Life:
                Refill1Life();
                break;
            //case PowerUpType.BiggerBall:
            //    BiggerBall();
            //    break;
            default:
                Debug.LogError("PowerUp has no effect");
                break;
        }
    }

    private void BiggerBall()
    {
        EventManager.BiggerBall();
    }

    private void Refill1Life()
    {
        EventManager.AddLife(1);
    }

    private void Freeze()
    {
        if(GameManager.Instance.DeltaMultiplier > 0)
        {
            GameManager.Instance.DeltaMultiplier = 0f;
            GameManager.Instance.StartResetDeltaMultiplier(freezeTimer);
        }
    }

    void DestroyAll()
    {
        for(int i = GameManager.Bombs.Count - 1; i >= 0; --i)
        {
            Destroy(GameManager.Bombs[i]);
            GameManager.Bombs.RemoveAt(i);
        }
    }
}
