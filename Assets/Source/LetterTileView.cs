using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LetterTileView : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		gameObject.transform.Translate (-.005f, 0f, 0f);
	}

	void OnMouseDown ()
	{
		
	}
}
