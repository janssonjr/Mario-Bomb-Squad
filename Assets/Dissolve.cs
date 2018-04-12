using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dissolve : MonoBehaviour {

    SpriteRenderer mySpriteRenderer;
    Image myImage;

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
    }

    public void StartDissolve()
    {
        iTween.ValueTo(gameObject, iTween.Hash("from", 0f, "to", 1f, "onupdate", "ChangeDissolve", "easetype", iTween.EaseType.linear, "oncomplete", "DissolveComplete", "time", 0.5f));
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
        Destroy(gameObject);
    }
}
