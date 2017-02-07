using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LetterTile {

	static string BONUS_MULTIPLY2 = "bonus multiply 2";
	static string BONUS_MULTIPLY3 = "bonus multiply 3";
	static string BONUS_MULTIPLY5 = "bonus multiply 5";

	private char letterValue;
	private string bonus;

	// Use this for initialization
	void Start () {
		
	}

	public void setupTile (char _letter, string _bonus="")
	{
		letterValue = _letter;
		bonus = _bonus;
	}

	override public string ToString ()
	{

		return letterValue.ToString();

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
