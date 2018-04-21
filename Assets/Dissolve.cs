using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dissolve : MonoBehaviour {
    SpriteRenderer mySpriteRenderer;
    Image myImage;
    bool destroyOnComplete;
    private void OnEnable()
    {
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        myImage = GetComponent<Image>();
        if(mySpriteRenderer != null)
        {
            mySpriteRenderer.material = new Material(mySpriteRenderer.material);
        }
        else if(myImage != null)
        {
            myImage.material = new Material(myImage.material);
        }
        destroyOnComplete = true;
    }

    public void DestroyOnComplete(bool aShouldDestroyOnComplete)
    {
        destroyOnComplete = aShouldDestroyOnComplete;
    }

    public void SetThreshold(float aThreshold)
    {
        ChangeDissolve(aThreshold);
    }

    public void StartDissolve(float from = 0f, float to = 1f, iTween.EaseType easeType = iTween.EaseType.linear, float time = 0.5f)
    {
        iTween.ValueTo(gameObject, iTween.Hash("from", from, "to", to, "onupdate", "ChangeDissolve", "easetype", easeType, "oncomplete", "DissolveComplete", "time", time));
        Collider2D collider2D = GetComponent<BoxCollider2D>();
        if(collider2D != null)
        {
            collider2D.enabled = false;
        }
    }
	
    void ChangeDissolve(float newValue)
    {
        if (mySpriteRenderer != null)
            mySpriteRenderer.material.SetFloat("_Threshold", newValue);
        else if (myImage != null)
            myImage.material.SetFloat("_Threshold", newValue);
    }

    void DissolveComplete()
    {
        //Spawn powerup
        gameObject.SendMessage("DissolveDone", SendMessageOptions.DontRequireReceiver);

        //if(destroyOnComplete == true)
            //Destroy(gameObject);
    }


}
