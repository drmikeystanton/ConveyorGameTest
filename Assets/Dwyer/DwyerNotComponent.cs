using System.Collections.Generic;
using UnityEngine;

public class DwyerNotComponent
{
	public List<int> myList = new List<int>();

	public DwyerNotComponent()
	{
		GenerateList();
	}

	public void CopyList(List<int> newList)
	{
		myList = new List<int>(newList);
	}

	public void GenerateList()
	{
		myList = new List<int>();

		int listSize = (int)(Random.Range(3, 6));
		for (int i = 0; i < listSize; ++i)
		{
			myList.Add((int)Random.Range(10, 500));
		}
	}

	public override string ToString()
	{
		string printedString = "{";
		for (int i = 0; i < myList.Count; ++i)
		{
			printedString += myList[i] + ", ";
		}

		//Remove the trailing comma if the list has any items in it
		if (myList.Count > 0)
		{
			printedString = printedString.Substring(0, printedString.Length - 2);
		}

		printedString += "}";

		return printedString;
	}
}
