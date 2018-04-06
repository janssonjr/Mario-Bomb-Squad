using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour {

    public float fallSpeed;

	void Start () {
		
	}
	
	void Update () {
        transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y - fallSpeed * Time.deltaTime, transform.localPosition.z);
	}
}
