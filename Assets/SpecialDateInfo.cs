using UnityEngine;
using System.Collections;
using System;
using System.IO;
using System.Collections.Generic;

[System.Serializable]
public struct DateRange {
	public DateTime start;
	public DateTime end;
}

public class SpecialDateInfo : MonoBehaviour {
	public static SpecialDateInfo singleton;
	public DateRange[] holidays;
	public DateRange[] open;
	public DateRange[] bcuTerm;
	public DateRange[] events;
	DateRange[] corData;
	bool loading;

	public string holidayFileName = "holidays.txt";
	public string openFileName = "opendays.txt";
	public string bcuTermFileName = "bcuterm.txt";
	public string eventsFileName = "events.txt";

	void Awake () {
		singleton = this;
	}

	// Use this for initialization
	void Start () {
	}

	public void LoadDateRanges (){
		holidays = GetDateRangeFromFile (holidayFileName);
		open = GetDateRangeFromFile (openFileName);
		//StartCoroutine(GetDateRangeFromFile(bcuTermFileName));
		//loading = true;
		//while (loading == true) {
		//	yield return new WaitForEndOfFrame ();
		//}
		bcuTerm = GetDateRangeFromFile(bcuTermFileName);
		events = GetDateRangeFromFile (eventsFileName);
		//DataLoader.singleton.loading = false;
	}

	DateRange[] GetDateRangeFromFile(string fileName){
		string filePath = Application.dataPath + "/" + fileName;
		List<DateRange> newData = new List<DateRange> ();
		using (FileStream fileStream = File.Open (filePath, FileMode.Open)) {
			StreamReader sr = new StreamReader (fileStream);
			
			while (sr.EndOfStream == false) {
				
				//yield return new WaitForEndOfFrame ();
				DateRange date = new DateRange ();
				string line = sr.ReadLine ();
				//print ("line is"+line);
				// remove the endline character
				line.Trim ();
				// remove spaces?
				line.Replace(" ","");
				// split the string
				string[] data = line.Split(new char[]{',','/'});
				//print ("split string");
				//yield return new WaitForSeconds(2);
				//print ("waited");
				//yield return new WaitForEndOfFrame ();
				if (data.Length != 6) {
					throw new Exception ("The file could not be read properly. Must contain data in the format [XX/XX/XXXX , XX/XX/XXXX] per line");
				}
				//yield return new WaitForEndOfFrame ();
				//print ("got 6 parameters");
				//yield return new WaitForEndOfFrame ();
				date.start = new DateTime(Convert.ToInt32 (data [2]),Convert.ToInt32 (data [1]),Convert.ToInt32 (data [0]));
				//print ("set start date");
				//yield return new WaitForEndOfFrame ();
				date.end = new DateTime(Convert.ToInt32 (data [5]),Convert.ToInt32 (data [4]),Convert.ToInt32 (data [3]));
				//print ("set end date");
				newData.Add (date);
				//print ("added data");
			}

		}

		return newData.ToArray();
		//corData = newData.ToArray();
		//print ("setting core data");
		//loading = false;
		//print ("not loading");
	}

	public DayType GetDayType (DateTime checkDate){
		DayType returnType = (DayType)0;

		for (int i = 0; i < holidays.Length; i++) {
			if (checkDate >= holidays [i].start && checkDate <= holidays [i].end) {
				returnType = returnType | DayType.SchoolHoliday;
				i = holidays.Length;
			}
		}
		for (int i = 0; i < open.Length; i++) {
			if (checkDate >= open [i].start && checkDate <= open [i].end) {
				returnType = returnType | DayType.Open;
				i = open.Length;
			}
		}
		for (int i = 0; i < bcuTerm.Length; i++) {
			if (checkDate >= bcuTerm [i].start && checkDate <= bcuTerm [i].end) {
				returnType = returnType | DayType.BCUTerm;
				i = bcuTerm.Length;
			}
		}
		for (int i = 0; i < events.Length; i++) {
			if (checkDate >= events [i].start && checkDate <= events [i].end) {
				returnType = returnType | DayType.Event;
				i = events.Length;
			}
		}
		return returnType;
	}

	// Update is called once per frame
	void Update () {
	
	}
}
