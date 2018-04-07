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
    Vector2 aimPosition;
    bool wasPressed;
    Rigidbody2D rb;
    Vector2 startPosition;
    UILineRenderer line;
    List<Vector2> points = new List<Vector2>();
    int objectHitCount;
    bool wasShot;
    Vector2 shotDirection;

    private void OnEnable()
    {
        Init();
    }

    public void Init()
    {
        wasShot = false;
        objectHitCount = 0;
        wasPressed = false;
        rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Kinematic;
        rb.gravityScale = 0f;
        startPosition = transform.TransformPoint(Vector3.zero);
        transform.position = startPosition;
        Rect parentRect = transform.parent.GetComponent<RectTransform>().rect;

        aimPosTransform = GameObject.FindGameObjectWithTag("AimPoint").transform;

        line = transform.parent.GetComponentInChildren<UILineRenderer>();

        points.Add(new Vector2(parentRect.xMin, parentRect.yMax));
        points.Add(transform.localPosition);
        points.Add(new Vector2(parentRect.xMax, parentRect.yMax));
        line.Points = points.ToArray();
        line.SetAllDirty();
    }

    void Update ()
    {
        if(wasShot == true)
        {
            transform.position += new Vector3(shotDirection.x, shotDirection.y, 0f) * 300 * Time.deltaTime;


        }
	}

    private void FixedUpdate()
    {
        if(wasPressed == true)
        {
            transform.position = Input.mousePosition;

            points[1] = transform.localPosition;
            line.Points = points.ToArray();
            line.SetAllDirty();
        }
    }

    public void OnClicked()
    {
        wasPressed = true;
        rb.bodyType = RigidbodyType2D.Kinematic;
    }

    public void OnReleased()
    {

        wasPressed = false;
        rb.bodyType = RigidbodyType2D.Dynamic;
        Vector2 direction = (aimPosTransform.position - transform.position/*new Vector2(transform.position.x, transform.position.y)*/);

        shotDirection = direction.normalized;
        Debug.Log("Direction: " + shotDirection);
        //wasShot = true;
        rb.velocity = direction.normalized * shootforce;
        Destroy(gameObject, 6f);
        //line.enabled = false;
    }

    private void OnDestroy()
    {
        EventManager.Score(objectHitCount);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Bomb") && rb.bodyType == RigidbodyType2D.Dynamic)
        {
            objectHitCount++;
            Destroy(collision.gameObject);
        }
    }

}
