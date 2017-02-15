using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LetterTile {

	static string BONUS_MULTIPLY2 = "bonus multiply 2";
	static string BONUS_MULTIPLY3 = "bonus multiply 3";
	static string BONUS_MULTIPLY5 = "bonus multiply 5";

	public const string STATE_ONBELT = "state on belt";
	public const string STATE_INPLAY = "state in play";
	public const string STATE_FALLING = "state falling";

	public string state;

	private char letterValue;
	private string bonus;
	private int activePosition;

	public LetterTileView tileView;



	public void setupTile (char _letter, string _bonus="")
	{
		letterValue = _letter;
		bonus = _bonus;
	}

	override public string ToString () // For debug to easily evaluate
	{
		return letterValue.ToString();
	}

	public void activateTile (LetterTileView _tileView)
	{
		state = STATE_ONBELT;
		tileView = _tileView;
		tileView.LetterTileModel = this;

	}

	public char getLetterValue ()
	{
		return letterValue;
	}
}
