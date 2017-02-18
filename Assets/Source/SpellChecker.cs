using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using UnityEngine;

public class SpellChecker : MonoBehaviour {

	private static TextAsset file;
	private static Dictionary<char, Dictionary<string, Dictionary <string, bool>>> words;

	// Use this for initialization
	public static void setup () {

		SpellChecker.words = new Dictionary<char, Dictionary<string, Dictionary <string, bool>>> ();

		file = Resources.Load ("words") as TextAsset;
		string fileString = file.text;

		List<string> allWords = new List<string> ();

		allWords.AddRange (file.text.Split (System.Environment.NewLine[0]));


		char firstOne;
		string firstTwo;
		string word;

		int count = 0;

		foreach (string baseWord in allWords) {

			if (baseWord.Length < 3)
				continue;

			word = baseWord.ToLower ();
			word = word.Trim ();

			firstOne = Convert.ToChar (word.Substring (0, 1));
			firstTwo = word.Substring (0, 2);

			if (!SpellChecker.words.ContainsKey(firstOne))
				SpellChecker.words.Add (firstOne, new Dictionary<string, Dictionary<string, bool>>());
			
			if (!SpellChecker.words[firstOne].ContainsKey (firstTwo))
				SpellChecker.words[firstOne].Add (firstTwo, new Dictionary<string, bool>());

			if (!SpellChecker.words [firstOne] [firstTwo].ContainsKey (word))
				SpellChecker.words [firstOne] [firstTwo].Add (word, true);

			/*print (firstOne.ToString () + " " + firstTwo + " " + word);

			count++;
			if (count == 100)
				break;*/
		}

	}

	public static bool CheckWord (string word) {

		if (word.Length < 3)
			return false;

		word = word.ToLower ();

		char firstOne = Convert.ToChar (word.Substring (0, 1));
		string firstTwo = word.Substring (0, 2);

		if (!SpellChecker.words [firstOne].ContainsKey (firstTwo))
			return false;

		if (SpellChecker.words [firstOne] [firstTwo] .ContainsKey(word))
			return true;

		return false;

	}

}
