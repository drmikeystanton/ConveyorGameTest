using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DwyerComponent : MonoBehaviour
{	
	// Use this for initialization
	void Start ()
	{
		DwyerNotComponent dnc1 = new DwyerNotComponent();
		DwyerNotComponent dnc2 = new DwyerNotComponent();

		Debug.Log("We made two random lists " + dnc1 + " and " + dnc2);

		dnc2.CopyList(dnc1.myList);

		Debug.Log("Copying 1 to 2 and now they're " + dnc1 + " and " + dnc2);

		dnc1.GenerateList();

		Debug.Log("Regenerating 1 to confirm it copied, and isn't a reference and now they're " + dnc1 + " and " + dnc2);
	}	
}
