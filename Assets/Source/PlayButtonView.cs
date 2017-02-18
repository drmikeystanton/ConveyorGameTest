using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayButtonView : MonoBehaviour {

	public delegate void clickDelegate ();
	public event clickDelegate clickEvent;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnMouseDown ()
	{
		print ("play mouse down ");
		clickEvent ();

	}
}
