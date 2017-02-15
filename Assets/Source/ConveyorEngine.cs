using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using UnityEngine;

public class ConveyorEngine : MonoBehaviour {

	const int minActiveLetters = 3;
	const int maxActiveLetters = 8;
	const float timeForMinLetterCheck = .5f;
	const float timeForPushLetter = 5f;




	private List<char> LetterDistribution;
	private List<char> TempLetters;
	private string letters = "aaaaaaaaabbccddddeeeeeeeeeeeeffggghhiiiiiiiiijkllllmmnnnnnnooooooooppqrrrrrrssssttttttuuuuvvwwxyyzaaaaaaaaabbccddddeeeeeeeeeeeeffggghhiiiiiiiiijkllllmmnnnnnnooooooooppqrrrrrrssssttttttuuuuvvwwxyyzaaaaaaaaabbccddddeeeeeeeeeeeeffggghhiiiiiiiiijkllllmmnnnnnnooooooooppqrrrrrrssssttttttuuuuvvwwxyyz";

	private ConveyorGameState gameState;

	[SerializeField]
	private ConveyorView gameView;

	private Timer TimerCheckForMinLetters;
	private Timer TimerPushNewLetter;

	private PlayHolderView playHolder;
	private BeltView belt;

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


		StartCoroutine (PushNewLetterCoroutine (timeForPushLetter));
		StartCoroutine (CheckForMinLettersCoroutine (timeForMinLetterCheck));

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
	}

	void OnDestroy ()
	{
		
	}

	void onMouseDown () {
		//Debug.Log ("mouse pressed");
	}




	void RefillBag ()
	{

		//TempLetters = new List<char> (LetterDistribution);
		TempLetters = letters.ToCharArray ().ToList();

		foreach (char s in TempLetters) {
			LetterTile tile = new LetterTile ();
			tile.setupTile (s);
			gameState.AddToTileBag (tile);
		}

		//Debug.Log ("Refilled bag "+gameState.GetNumActiveTiles().ToString());

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

		if (gameState.IsBagEmpty ()) {
			RefillBag ();
		}

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
		if (gameState.IsBagEmpty ()) {

			RefillBag ();
		}

		LetterTile newTile = gameState.PullTileFromBag ();
		GameObject tileGameObject = gameView.addNewTile ();

		newTile.activateTile (tileGameObject.GetComponent<LetterTileView> ());
		tileGameObject.GetComponentInChildren<TextMesh> ().text = newTile.getLetterValue ().ToString ().ToUpper();
		tileGameObject.GetComponent<LetterTileView> ().clickEvent += LetterTileClickHandler;

		belt.AddTile (tileGameObject.GetComponent<LetterTileView> ());

		int num = gameState.GetNumActiveTiles ();

		if (num > maxActiveLetters) {
			RemoveOldestLetter ();
		}



		//gameState.DebugGameState ();
	}

	void RemoveOldestLetter ()
	{

		gameState.RemoveOldestTile ();

	}

	void LetterTileClickHandler (LetterTile clickedTile)
	{
		switch (clickedTile.state) {
		case LetterTile.STATE_ONBELT:
		case LetterTile.STATE_FALLING:
			clickedTile.state = LetterTile.STATE_INPLAY;
			playHolder.addTile (clickedTile.tileView);
			break;
		case LetterTile.STATE_INPLAY:
			clickedTile.state = LetterTile.STATE_ONBELT;
			playHolder.removeTile (clickedTile.tileView);
			belt.repositionTiles ();
			break;
		}
	}

}
