using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using UnityEngine;

public class ConveyorEngine : MonoBehaviour {

	const int minActiveLetters = 3;
	const int maxActiveLetters = 8;
	const int timeForMinLetterCheck = 500;
	const int timeForPushLetter = 5000;




	private List<char> LetterDistribution;
	private List<char> TempLetters;
	private string letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

	private ConveyorGameState gameState;
	private ConveyorView view;

	private Timer TimerCheckForMinLetters;
	private Timer TimerPushNewLetter;

	// Use this for initialization
	void Start () {

		Debug.Log ("Conveyor engine demo");

		//string letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
		//LetterDistribution = letters.ToCharArray ().ToList();

		gameState = new ConveyorGameState ();
		gameState.Initialize ();

		view = new ConveyorView ();

		TimerCheckForMinLetters = new Timer (timeForMinLetterCheck);
		TimerCheckForMinLetters.AutoReset = true;
		TimerCheckForMinLetters.Elapsed += CheckForMinLetters;

		TimerPushNewLetter = new Timer (timeForPushLetter);
		TimerPushNewLetter.AutoReset = true;
		TimerPushNewLetter.Elapsed += PushNewLetterTimerHandler;

		Initialize ();

	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0)) {
		//	PushNewLetter ();
		}
	}

	void onMouseDown () {
		//Debug.Log ("mouse pressed");
	}

	public void Initialize ()
	{

		TimerCheckForMinLetters.Start ();
		TimerPushNewLetter.Start ();

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

	void CheckForMinLetters (object timer, ElapsedEventArgs evt)
	{

		if (gameState.IsBagEmpty ()) {
			RefillBag ();
		}

		int num = gameState.GetNumActiveTiles ();

		if (num < minActiveLetters) {
			PushNewLetter ();
		}

	}

	void PushNewLetterTimerHandler (object timer, ElapsedEventArgs evt)
	{

		Debug.Log ("timer push a new letter");
		PushNewLetter ();

	}

	void PushNewLetter ()
	{
		Debug.Log ("lets add one");

		if (gameState.IsBagEmpty ()) {

			RefillBag ();
		}

		gameState.PullTileFromBag ();


		int num = gameState.GetNumActiveTiles ();

		if (num > maxActiveLetters) {
			RemoveOldestLetter ();
		}

		gameState.DebugGameState ();

	}

	void RemoveOldestLetter ()
	{

		gameState.RemoveOldestTile ();

	}

}
