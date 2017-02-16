using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

public class ConveyorGameState {

	private List<LetterTileModel> tileBag;				// tiles waiting to get pulled
	private List<LetterTileModel> activeTiles;			// the tiles currently in play on the belt

	private List<char> TempLetters;
	private string letters = "aaaaaaaaabbccddddeeeeeeeeeeeeffggghhiiiiiiiiijkllllmmnnnnnnooooooooppqrrrrrrssssttttttuuuuvvwwxyyzaaaaaaaaabbccddddeeeeeeeeeeeeffggghhiiiiiiiiijkllllmmnnnnnnooooooooppqrrrrrrssssttttttuuuuvvwwxyyzaaaaaaaaabbccddddeeeeeeeeeeeeffggghhiiiiiiiiijkllllmmnnnnnnooooooooppqrrrrrrssssttttttuuuuvvwwxyyz";

	private int randomBagTilePos;

	private System.Random rnd;

	public void Initialize () {
		tileBag = new List<LetterTileModel> ();
		activeTiles = new List<LetterTileModel> ();
		rnd = new System.Random ();
	}

	public void AddToTileBag (LetterTileModel _tile)
	{

		tileBag.Add(_tile);

	}

	public LetterTileModel PullTileFromBag ()
	{

		if (IsBagEmpty ()) {

			RefillBag ();
		}

		randomBagTilePos = rnd.Next(0, tileBag.Count);
		//randomBagTilePos = (int)Random.Range(0, tileBag.Count);
		LetterTileModel newTile = tileBag[randomBagTilePos];
		activeTiles.Add(newTile);
		tileBag.Remove (newTile);

		return newTile;

	}

	void RefillBag ()
	{

		//TempLetters = new List<char> (LetterDistribution);
		TempLetters = letters.ToCharArray ().ToList();

		foreach (char s in TempLetters) {
			LetterTileModel tile = new LetterTileModel ();
			tile.setupTile (s);
			AddToTileBag (tile);
		}

		//Debug.Log ("Refilled bag "+gameState.GetNumActiveTiles().ToString());

	}

	public LetterTileModel RemoveOldestTile ()
	{
		LetterTileModel oldTile = activeTiles [0];
		activeTiles.RemoveAt (0);
		return oldTile;
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

	public List<LetterTileModel> GetActiveTiles ()
	{

		return activeTiles;

	}

	public bool isTileOnBelt (LetterTileModel _tile){
		if (activeTiles.IndexOf (_tile) == -1) {
			return false;
		}
		return true;
	}

}
