using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class ConveyorGameState {

	private List<LetterTile> tileBag;				// tiles waiting to get pulled
	private List<LetterTile> activeTiles;			// the tiles currently in play on the conveyor

	private int randomBagTilePos;

	private System.Random rnd;

	public void Initialize () {
		tileBag = new List<LetterTile> ();
		activeTiles = new List<LetterTile> ();
		rnd = new System.Random ();
	}

	public void AddToTileBag (LetterTile _tile)
	{

		tileBag.Add(_tile);
		//Debug.Log ("add to tile bag"+ tileBag.Count + " "+_tile);

	}

	public void PullTileFromBag ()
	{

		//randomBagTilePos = rnd.Next(0, tileBag.Count);
		randomBagTilePos = (int)Random.Range(0, tileBag.Count);
		activeTiles.Add(tileBag[randomBagTilePos]);
		tileBag.RemoveAt(randomBagTilePos);

	}

	public void RemoveOldestTile ()
	{

		activeTiles.RemoveAt (0);

	}

	public int GetNumActiveTiles ()
	{
		return activeTiles.Count;
	}

	public bool IsBagEmpty ()
	{
		if (tileBag.Count == 0)
		{
			return true;
		}
		return false;
	}

	public List<LetterTile> GetActiveTiles ()
	{

		return activeTiles;

	}

	public void DebugGameState ()
	{

		string stateString = "";

		foreach (LetterTile tile in activeTiles) {
			stateString += tile.ToString ();
		}

		Debug.Log (stateString);

	}

}
