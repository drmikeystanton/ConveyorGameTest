using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LetterTileView : MonoBehaviour {

	public delegate void clickDelegate (LetterTileModel clickedTile);
	public event clickDelegate clickEvent;

	public LetterTileModel model;

	public TextMesh letterValueText;

	Vector2 fallVels = new Vector2 (-.15f, 0);
	float resist = .95f;
	float rotate = 5f;
	float gravity = -.008f;

	float rotation = 0f;
	Vector2 currentPos;
	Vector2 moveToPos;

	// Use this for initialization
	void Start () {
	}

	void Destroy ()
	{
	}

	public void activate (LetterTileModel _model, clickDelegate _callBack)
	{
		model = _model;
		letterValueText.text = model.getLetterValue ().ToString ().ToUpper();
		gameObject.SetActive (true);
		setRotation (0);
		if (clickEvent == null)
		{
			clickEvent+= _callBack;
		}

	}

	void OnDisable () {

		model = null;

	}
		

	// Update is called once per frame
	void Update () {

		if (model.state == LetterTileModel.STATE_FALLING) {
			fallVels.x *= resist;
			fallVels.y *= resist;
			fallVels.y += gravity;
			rotate *= resist;
			currentPos.x += fallVels.x;
			currentPos.y += fallVels.y;
			moveToPos = currentPos;
			setRotation (rotation + rotate);

		}
		else 
		{
			currentPos.x -= (currentPos.x - moveToPos.x) / 8;
			currentPos.y -= (currentPos.y - moveToPos.y) / 8;
			setRotation (rotation * .97f);

		}
		transform.position = new Vector3 (currentPos.x, currentPos.y, 0f);
	}

	void OnMouseDown ()
	{
		clickEvent (model);
	}

	public void setInPlay ()
	{
		setRotation (Random.Range (-30f, 30f));

	}

	void setRotation (float _r){
		rotation = _r;
		transform.eulerAngles = new Vector3 (0, 0, rotation);
	}

	public void moveTo (Vector2 pos, bool jump=false)
	{
		moveToPos = pos;
		if (jump) {
			currentPos = pos;
			transform.position = new Vector3 (currentPos.x, currentPos.y, 0f);
		}
	}

}

