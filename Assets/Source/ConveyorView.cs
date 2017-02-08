using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorView {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public GameObject addNewTile ()
	{

		GameObject newTile = GameObject.Instantiate (ConveyorEngine.tileRef) as GameObject;
		return newTile;

	}

	public void UpdateActiveTiles ()
	{

	}
}
