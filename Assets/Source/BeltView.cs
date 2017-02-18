using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BeltView : MonoBehaviour {

	List<LetterTileView> tiles;

	[SerializeField]
	private float tileSpace;

	// Use this for initialization
	void Start () {
		tiles = new List<LetterTileView> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void MoveTileToStart (LetterTileView tile) {

		tile.moveTo (new Vector2 (transform.position.x + 3f, transform.position.y), true);

	}

	public void AddTile (LetterTileView tile)
	{
		MoveTileToStart (tile);
		tiles.Add (tile);
		repositionTiles ();

	}

	public void removeTile (LetterTileView tile)
	{
		tiles.Remove(tile);
	}

	public void repositionTiles ()
	{
		float count = 0f;

		foreach (LetterTileView tile in tiles) {

			if (tile.model.state == LetterTileModel.STATE_ONBELT) {
				tile.moveTo (new Vector2 (transform.position.x - (tiles.Count * tileSpace) + count * tileSpace, transform.position.y));
			}
			count++;
		}

	}

}
