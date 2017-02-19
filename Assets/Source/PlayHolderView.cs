using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayHolderView : MonoBehaviour {

	[SerializeField]
	private float tileSpace;

	List<LetterTileView> tiles;

	// Use this for initialization
	void Start () {
		tiles = new List<LetterTileView> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void addTile (LetterTileView tile)
	{
		tile.setInPlay ();
		tiles.Add (tile);
		repositionTiles ();
	}

	public void removeTile (LetterTileView tile)
	{
		tiles.Remove(tile);
		repositionTiles ();
	}

	void repositionTiles ()
	{

		float count = 0f;

		foreach (LetterTileView tile in tiles) {

			tile.moveTo (new Vector2(((tiles.Count-1)*-.5f*tileSpace) + transform.position.x + (count * tileSpace), transform.position.y));
			count++;
		}

	}

}
