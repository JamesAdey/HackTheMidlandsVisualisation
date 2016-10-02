using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class DaySlider : MonoBehaviour {

	public static DaySlider singleton;

	public InputField dayText;
	public InputField monthText;
	public InputField yearText;

	public Slider slider;
	public Text displayText;
	string[] textStrings;

	public int day;
	public int month;
	public int year;

	void Awake () {
		singleton = this;
	}

	public void UpdateDate(int d, int m, int y){
		day = d;
		month = m;
		year = y;
		dayText.text = day.ToString ();
		monthText.text = month.ToString ();
		yearText.text = year.ToString ();

	}

	public void SetDay (string _d) {
		day = Convert.ToInt32(_d);
		// lookup max days per month
		if (month == DataManager.singleton.endDate.Month && year == DataManager.singleton.endDate.Year) {
			if (day > DataManager.singleton.endDate.Day) {
				day = DataManager.singleton.endDate.Day;
			} else if (day < 1) {
				day = 1;
			}
		}
		else if (day > DateTime.DaysInMonth(year,month)) {
			day = DateTime.DaysInMonth(year, month);
		}
		else if(day < 1){
			day = 1;
		}
		dayText.text = day.ToString ();
	}

	public void SetMonth (string _m){
		month = Convert.ToInt32(_m);
		if (year < DataManager.singleton.endDate.Year) {
			if (month < 1) {
				month = 1;
			} else if (month > 12) {
				month = 12;
			}
		} else if (month < DataManager.singleton.endDate.Month) {
			if (month < 1) {
				month = 1;
			}
		} else {
			month = DataManager.singleton.endDate.Month;
		}
		monthText.text = month.ToString ();
		SetDay (dayText.text);
	}

	public void SetYear (string _y){
		year = Convert.ToInt32(_y);
		if (year < DataManager.singleton.startDate.Year) {
			year = DataManager.singleton.startDate.Year;
		} else if (year > DataManager.singleton.endDate.Year) {
			year = DataManager.singleton.endDate.Year;
		}
		yearText.text = year.ToString ();
		SetMonth (monthText.text);
	}

	public void ViewDate () {
		DataManager.singleton.SetCurrentDay (day, month, year);
	}

	public void SliderValueChanged (float val){
		int intVal = (int)val;
		// lookup the display text
		displayText.text = textStrings[intVal];
		// change the value displayed
		DataManager.singleton.SelectDisplayDay(intVal);

	}

	// Use this for initialization
	void Start () {
		textStrings = new string[8];
		textStrings [0] = "Monday";
		textStrings [1] = "Tuesday";
		textStrings [2] = "Wednesday";
		textStrings [3] = "Thursday";
		textStrings [4] = "Friday";
		textStrings [5] = "Saturday";
		textStrings [6] = "Sunday";
		textStrings [7] = "WEEK";

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
