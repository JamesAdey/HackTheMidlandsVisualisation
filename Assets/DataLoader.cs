using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;

public class DataLoader : MonoBehaviour {

	public static DataLoader singleton;

	string csvFilePath = "visitors.csv";

	string[] data;

	public int[] totalPeopleIn;
	public int[] level0;
	public int[] level1main;
	public int[] level1side;
	public int[] level2;
	public int[] uniLink;

	public bool loading = false;

	void Awake(){
		singleton = this;
	}

	// Use this for initialization
	void Start () {
		StartCoroutine (DoLoading ());
	}
	IEnumerator DoLoading(){
		// load the data file
		LoadFile ();
		// load the date ranges
		//StartCoroutine(SpecialDateInfo.singleton.LoadDateRanges ());
		SpecialDateInfo.singleton.LoadDateRanges();
		yield return new WaitForEndOfFrame ();
		//loading = true;
		//while (loading == true) {
		//	yield return new WaitForEndOfFrame ();
		//}
		// now setup the arrays :)
		DataManager.singleton.CreateDataArrays ();
	}

	void LoadFile (){
		
		string path = Application.dataPath + "/"+csvFilePath;

		using (FileStream fileStream = File.Open (csvFilePath, FileMode.Open)) {
			StreamReader sr = new StreamReader (fileStream);
			List<string> newData = new List<string> ();
			while (sr.EndOfStream == false) {
				string line = sr.ReadLine ();
				line.Trim ();
				newData.Add (line);
			}
			data = newData.ToArray ();
		}



		totalPeopleIn = GetDataFromString (data[0]);
		level0 = GetDataFromString (data[1]);
		level1main = GetDataFromString (data[2]);
		level1side = GetDataFromString (data[3]);
		level2 = GetDataFromString (data[4]);
		uniLink = GetDataFromString (data[5]);


	}

	int[] GetDataFromString(string line){
		List<int> values = new List<int>();
		int charNum = 0;
		char nextChar = line[0];
		string text = "";
		string[] testArray = line.Split (new char[]{','});
		for (int i = 0; i < testArray.Length; i++) {
			if (testArray [i] == "") {
				values.Add (0);
			} else {
				values.Add (Convert.ToInt32 (testArray [i]));
			}
		}

		/*while(charNum < line.Length){
			nextChar = line[charNum];
			if (nextChar == ',') {
				// convert the text to an int and add it to the list
				values.Add (Convert.ToInt32 (text));
				text = "";
			} else {
				text += nextChar;
				print (text);
			}
			charNum++;
		}*/

		return values.ToArray ();
	}

	// Update is called once per frame
	void Update () {
	
	}


}
