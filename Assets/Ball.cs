using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using UnityEngine.UI.Extensions;

public class Ball : MonoBehaviour {

    public float shootforce;
    Transform aimPosTransform;
    bool wasPressed;
    Rigidbody2D rb;
    Vector2 startPosition;
    UILineRenderer line;
    List<Vector2> points = new List<Vector2>();
    bool wasShot;
    CircleCollider2D shotArea;
    CircleCollider2D myCollider;
	int myScore;
    RectTransform myRect;

    private void OnEnable()
    {
        Init();
    }

    public void Init()
    {
        wasShot = false;
        wasPressed = false;
        rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Kinematic;
        rb.gravityScale = 0f;
        startPosition = transform.TransformPoint(Vector3.zero);
        transform.position = startPosition;
        Rect parentRect = transform.parent.GetComponent<RectTransform>().rect;

        aimPosTransform = transform.parent.Find("AimPoint").transform;

        line = transform.parent.GetComponentInChildren<UILineRenderer>();

        points.Add(new Vector2(parentRect.xMin, parentRect.yMax));
        points.Add(transform.localPosition);
        points.Add(new Vector2(parentRect.xMax, parentRect.yMax));
        line.Points = points.ToArray();
        line.SetAllDirty();
        shotArea = transform.parent.Find("ShotArea").GetComponent<CircleCollider2D>();
        myCollider = GetComponent<CircleCollider2D>();
		myScore = 0;


        myRect = GetComponent<RectTransform>();
    }

    void ChangeSize(float sizeMultiplier)
    {
        var rectTransform = GetComponent<RectTransform>();
        rectTransform.sizeDelta = rectTransform.sizeDelta * sizeMultiplier;
    }

    void Update ()
    {
        if(wasShot == true)
        {
            //transform.position += new Vector3(shotDirection.x, shotDirection.y, 0f) * 300 * Time.deltaTime;
            bool isInside = Camera.main.pixelRect.Contains(myRect.position);
            Debug.Log(Camera.main.pixelRect.size);
            if(isInside == false && myScore > 0)
            {
                EventManager.Score(myScore * 100);
                Destroy(gameObject);
                //rb.velocity = Vector2.zero;
            }
            //if(render.isVisible == false)
            //{
            //EventManager.Score(myScore);
            //Destroy(gameObject);
            //}
        }
	}

    private void FixedUpdate()
    {
        if(wasPressed == true)
        {
            transform.position = Input.mousePosition;
            SetLinePos();
        }
    }

    void SetLinePos()
    {
        points[1] = transform.localPosition;
        line.Points = points.ToArray();
        line.SetAllDirty();
    }

    public void OnClicked()
    {
        wasPressed = true;
        rb.bodyType = RigidbodyType2D.Kinematic;
    }

    public bool OnReleased()
    {
        wasPressed = false;
        if(IsToSmallDrag())
        {
            Debug.Log("To small drag, returning to start pos");
            transform.position = startPosition;
            SetLinePos();
            return false;
        }
        rb.bodyType = RigidbodyType2D.Dynamic;
        Vector2 direction = (aimPosTransform.position - transform.position/*new Vector2(transform.position.x, transform.position.y)*/);

        rb.velocity = direction.normalized * shootforce;
        wasShot = true;
        //Destroy(gameObject, 6f);
        return true;
    }

    private bool IsToSmallDrag()
    {
        return Physics2D.IsTouching(shotArea, myCollider);
        /*float dragDistance = (startPosition - new Vector2(transform.position.x, transform.position.y)).magnitude;
        Debug.Log("DragDistance: "+ dragDistance.ToString());
        if (dragDistance < 50f)
            return true;
        return false;*/
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Bomb") && rb.bodyType == RigidbodyType2D.Dynamic)
        {
			myScore++;
			collision.SendMessage("OnHit");
        }
    }
}
