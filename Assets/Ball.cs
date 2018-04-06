using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using UnityEngine.UI.Extensions;

public class Ball : MonoBehaviour {

    public float shootforce;
    bool wasPressed;
    Rigidbody2D rb;
    Vector2 startPosition;
    UILineRenderer line;
    List<Vector2> points = new List<Vector2>();
    int objectHitCount;
    bool wasShot;
    Vector2 shotDirection;
    public Vector2 StartPosition
    {
        get { return startPosition; }
        set { startPosition = value; }
    }

    public void SetRigidbodyPosition(Vector2 aPosition)
    {
        //rb.position = aPosition;
        transform.localPosition = aPosition;
    }

    private void OnEnable()
    {
        wasShot = false;
        objectHitCount = 0;
        wasPressed = false;
        rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Kinematic;
        rb.gravityScale = 0f;
        startPosition = transform.localPosition;//rb.position;
        Debug.Log("StartPosition: " + StartPosition.ToString());
        Rect parentRect = transform.parent.GetComponent<RectTransform>().rect;
        //Rect mineRect = GetComponent<RectTransform>().rect;
        //float yDisty
        //line1.sizeDelta = new Vector2(20, 30);
        //Debug.Log("xMin: " + parentRect.xMin + " xMax: " + parentRect.xMax);
        //Debug.Log("yMin: " + parentRect.yMin + " yMax: " + parentRect.yMax);

        line = transform.parent.GetComponentInChildren<UILineRenderer>();

        points.Add(new Vector2(parentRect.xMin, parentRect.yMax));
        points.Add(transform.localPosition);
        points.Add(new Vector2(parentRect.xMax, parentRect.yMax));
        line.Points = points.ToArray();
        line.SetAllDirty();
        //line1.localPosition = new Vector3(parentRect.xMin, parentRect.yMax);
        //myLineRenderer.SetPosition(0, new Vector3(parentRect.xMax, parentRect.yMax, -1));
        //myLineRenderer.SetPosition(1, new Vector3(mineRect.center.x, mineRect.center.y, -1));
        //myLineRenderer.SetPosition(2, new Vector3(parentRect.xMin, parentRect.yMax));

    }

    public void Init()
    {
        wasPressed = false;
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(wasShot == true)
        {
            transform.localPosition = new Vector3(transform.localPosition.x * shotDirection.x, transform.localPosition.y *shotDirection.y, 0f) * 10 * Time.deltaTime;

        }
	}

    private void FixedUpdate()
    {
        if(wasPressed == true)
        {
            Vector2 previousPos = transform.localPosition;//rb.position;
            //rb.position = Input.mousePosition;
            transform.position = Input.mousePosition;

            //float distance = (rb.position - startPosition).magnitude;
            points[1] = transform.localPosition;
            line.Points = points.ToArray();
            line.SetAllDirty();
            //Debug.Log("Distance: " + distance.ToString());
            //if (distance > 126f)
              //  rb.position = previousPos;
        }
    }

    /*private void OnMouseDown()
    {
        wasPressed = true;
        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.gravityScale = 0f;
        Debug.Log("Was pressed");
    }*/

    public void OnClicked()
    {
        wasPressed = true;
        rb.bodyType = RigidbodyType2D.Kinematic;
    }

    public void OnReleased()
    {

        wasPressed = false;
        rb.bodyType = RigidbodyType2D.Dynamic;
        Vector2 direction = (startPosition - new Vector2(transform.localPosition.x, transform.localPosition.y));
        Vector2 velocity = direction.normalized * /*direction.magnitude * */shootforce;
        //rb.position = transform.position;
        shotDirection = direction.normalized;
        wasShot = true;
        //rb.velocity = velocity;
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
