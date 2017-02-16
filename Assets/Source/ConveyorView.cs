using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorView : MonoBehaviour {



	[SerializeField]
	private PlayHolderView playHolder;

	[SerializeField]
	private BeltView belt;

	public LetterTileView fallingTile;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void setFallingTile (LetterTileView tile)
	{
		fallingTile = tile;
	}

}
