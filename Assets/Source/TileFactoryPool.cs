using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileFactoryPool : MonoBehaviour {

	private static GameObject tileRef;

	private static int refillAmount = 5;

	private static List<GameObject> tilePool;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public static GameObject GetTile ()
	{
		if (tileRef == null) {
			tileRef = Resources.Load ("LetterTilePrefab") as GameObject;
		}
		if (tilePool == null) {
			tilePool = new List<GameObject> ();
		}

		if (tilePool.Count == 0) {

			for (int i = 0; i < refillAmount; i++) {

				GameObject newTile = GameObject.Instantiate (tileRef) as GameObject;
				newTile.SetActive (false);
				tilePool.Add (newTile);

			}

		}

		GameObject returnTile = tilePool [0];
		tilePool.Remove (returnTile);
		return returnTile;
			
	}

	public static void ReturnTile (GameObject _tile)
	{
		if (_tile.activeSelf) {
			_tile.SetActive (false);
		}
		tilePool.Add (_tile);
	}
		
}
