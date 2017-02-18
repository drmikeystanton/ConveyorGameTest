using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using UnityEngine;

public class ConveyorEngine : MonoBehaviour {

	const int minActiveLetters = 3;
	const int maxActiveLetters = 8;
	const float timeForMinLetterCheck = .6f;
	const float timeForPushLetter = 3.5f;




	private List<char> LetterDistribution;


	private ConveyorGameState gameState;

	[SerializeField]
	private ConveyorView gameView;

	private Timer TimerCheckForMinLetters;
	private Timer TimerPushNewLetter;

	private PlayHolderView playHolder;
	private BeltView belt;

	[SerializeField]
	private PlayButtonView playButton;

	// Use this for initialization
	void Start () {

		Debug.Log ("Conveyor engine demo");

		//string letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
		//LetterDistribution = letters.ToCharArray ().ToList();

		gameState = new ConveyorGameState ();
		gameState.Initialize ();

		playHolder = GameObject.Find ("PlayHolderGameObject").GetComponent<PlayHolderView> ();
		belt = GameObject.Find ("ConveyorBeltGameObject").GetComponent<BeltView> ();

		//tileRef = Resources.Load ("LetterTilePrefab") as GameObject;

		playButton.clickEvent += handlePlayButtonClick;

		StartCoroutine (PushNewLetterCoroutine (timeForPushLetter));
		StartCoroutine (CheckForMinLettersCoroutine (timeForMinLetterCheck));

		SpellChecker.setup ();

	

	}

	/*private IEnumerator SampleCouroutine(float waitDelay)
	{
		int numLoops = 15;
		int loopCounter = 0;

		while (true) //loop forever until we break the coroutine
		{
			yield return new WaitForSeconds(waitDelay); //wait one second in execution

			Debug.Log("Spawning after a delay");

			Instantiate(tileRef);

			yield return new WaitForEndOfFrame(); // wait one more frame for fun

			//Another way to wait
			//yield return new WaitUntil(< FUNCTION >);

			loopCounter++;

			if (loopCounter >= numLoops)
			{
				yield break; //exit the couroutine
			}

			//Other ways to stop coroutines
			//StopAllCoroutines();
			//StopCoroutine(<ReferenceEquals HERE>);
		}
	}*/
	
	// Update is called once per frame
	void Update () {
		/*if (Input.GetMouseButtonDown (0)) {
			PushNewLetter ();
		}*/
		if (gameView.fallingTile != null && gameView.fallingTile.transform.position.y < -5f) {
			KillTile (gameView.fallingTile.model);
			gameView.fallingTile = null;
		}
	}

	void OnDestroy ()
	{
		
	}

	void onMouseDown () {
		//Debug.Log ("mouse pressed");
	}






	private IEnumerator CheckForMinLettersCoroutine (float delay)
	{

		while (true) {

			yield return new WaitForSeconds (delay);

			CheckForMinLetters ();

		}

	}

	void CheckForMinLetters ()
	{

		int num = gameState.GetNumActiveTiles ();

		if (num < minActiveLetters) {
			PushNewLetter ();
		}

	}


	private IEnumerator PushNewLetterCoroutine (float delay)
	{
		while (true) {

			yield return new WaitForSeconds (delay);
			
			PushNewLetter ();

		}
	}

	void PushNewLetter ()
	{
		
		GameObject tileGameObject = TileFactoryPool.GetTile ();
		LetterTileModel newTileModel = gameState.PullTileFromBag ();
		LetterTileView tileView = tileGameObject.GetComponent<LetterTileView> ();

		newTileModel.activateTile (tileView);
		tileView.activate (newTileModel, LetterTileClickHandler);

		belt.AddTile (tileView);

		int num = gameState.GetNumActiveTiles ();

		if (num >= maxActiveLetters) {
			RemoveOldestLetter ();
		}

	}

	void RemoveOldestLetter ()
	{

		LetterTileModel oldTile = gameState.RemoveOldestTile ();
		belt.removeTile (oldTile.tileView);
		switch (oldTile.state) {
		case LetterTileModel.STATE_ONBELT:
			gameView.setFallingTile (oldTile.tileView);
			oldTile.state = LetterTileModel.STATE_FALLING;
			break;
		}
	}

	void LetterTileClickHandler (LetterTileModel clickedTile)
	{
		switch (clickedTile.state) {
		case LetterTileModel.STATE_ONBELT:
		case LetterTileModel.STATE_FALLING:
			clickedTile.state = LetterTileModel.STATE_INPLAY;
			gameState.AddTileToPlay (clickedTile);
			playHolder.addTile (clickedTile.tileView);
			break;
		case LetterTileModel.STATE_INPLAY:
			removeTileFromPlay (clickedTile);
			break;
		case LetterTileModel.STATE_DEAD:
			break;
		}
	}

	void removeTileFromPlay (LetterTileModel tile) {

		playHolder.removeTile (tile.tileView);
		if (gameState.isTileOnBelt(tile)) {
			tile.state = LetterTileModel.STATE_ONBELT;
			belt.repositionTiles ();
		} else {
			KillTile (tile);
		}

	}

	void KillTile (LetterTileModel _tile) {

		_tile.state = LetterTileModel.STATE_DEAD;
		TileFactoryPool.ReturnTile (_tile.tileView.gameObject);
		belt.removeTile (_tile.tileView);
		belt.MoveTileToStart(_tile.tileView);
		_tile.deactivateTile ();

	}


	void handlePlayButtonClick () {

		string wordPlayed = gameState.GetWordToPlay ();

		if (wordPlayed.Length < 3)
			return;



		List<LetterTileModel> inPlayTiles = gameState.GetInPlayTiles ();

		bool valid = SpellChecker.CheckWord (wordPlayed.ToLower ());

		print (wordPlayed + " is a " + valid + " play");

		if (valid) {

			foreach (LetterTileModel tile in inPlayTiles) {
				KillTile (tile);
			}

		} else {

			foreach (LetterTileModel tile in inPlayTiles) {
				removeTileFromPlay (tile);
			}

		}

		gameState.ClearTilesInPlay ();

	}

}
