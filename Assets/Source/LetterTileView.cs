using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LetterTileView : MonoBehaviour {

	public delegate void clickDelegate (LetterTile clickedTile);
	public event clickDelegate clickEvent;

	public LetterTile LetterTileModel;
			
	Vector2 currentPos = new Vector2 (0f, 0f);
	Vector2 moveToPos = new Vector2 (0f, 0f);

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
		currentPos.x -= (currentPos.x - moveToPos.x)/8;
		currentPos.y -= (currentPos.y - moveToPos.y)/8;

		//transform.eulerAngles = new Vector3(xrot, xrot*1.4f, xrot*.6f);
		transform.position = new Vector3 (currentPos.x, currentPos.y, 0f);
	}

	void OnMouseDown ()
	{
		clickEvent (LetterTileModel);
	}

	public void moveTo (Vector2 pos)
	{

		moveToPos = pos;

	}

}

