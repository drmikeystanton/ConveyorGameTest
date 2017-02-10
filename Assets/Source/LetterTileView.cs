using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LetterTileView : MonoBehaviour {

	float flySpeed = 0f;
	float rotate;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
		if (flySpeed != 0f) {
			flySpeed += .01f;

			transform.eulerAngles = transform.eulerAngles + new Vector3(rotate, 0, rotate);
			transform.position = transform.position + new Vector3 (0f, flySpeed, 0f);
		} else {
			transform.Translate (-.005f, 0f, 0f);
		}
	}

	void OnMouseDown ()
	{
		rotate = Random.Range (-5f, 5f);
		flySpeed = Random.Range (.001f, .05f);
	}
}
