using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine.UI;

public class DataManager : MonoBehaviour {

	public static DataManager singleton;
	[HideInInspector]
	public List<Week> weeks = new List<Week>();
	public DateTime startDate = new DateTime(2015,2,2);
	public DateTime endDate;

	public Week currentWeek;

	public Day[] dayAverage;
	int dayNumber;
	public Text totalText;

	public Text level0Text;
	public Text level1mainText;
	public Text level1sideText;
	public Text level2Text;
	public Text uniLinkText;

	public Text averageVisitorsLabel;
	public Text level0AvgText;
	public Text level1mainAvgText;
	public Text level1sideAvgText;
	public Text level2AvgText;
	public Text uniLinkAvgText;

	public Transform level0Circle;
	public Transform level1mainCircle;
	public Transform level1sideCircle;
	public Transform level2Circle;
	public Transform uniLinkCircle;


	private Day _displayDay;
	public Day displayDay{
		get{
			return _displayDay;
		}
		set{
			_displayDay = value;
			UpdateGUI ();
		}
	}

	void Awake () {
		singleton = this;
	}

	void UpdateGUI () {
		level0Text.text = displayDay.level0.ToString ();
		level1mainText.text = displayDay.level1main.ToString ();
		level1sideText.text = displayDay.level1side.ToString ();
		level2Text.text = displayDay.level2.ToString ();
		uniLinkText.text = displayDay.uniLink.ToString ();
		totalText.text = displayDay.total.ToString ();
		if (dayNumber == 7) {
			averageVisitorsLabel.text = "Average Visitors This week";
			level0AvgText.text = currentWeek.weeklyAverage.level0.ToString ();
			level1sideAvgText.text = currentWeek.weeklyAverage.level1main.ToString ();
			level1mainAvgText.text = currentWeek.weeklyAverage.level1side.ToString ();
			level2AvgText.text = currentWeek.weeklyAverage.level2.ToString ();
			uniLinkAvgText.text = currentWeek.weeklyAverage.uniLink.ToString ();
		} else {
			averageVisitorsLabel.text = "Daily Average Visitors";
			level0AvgText.text = dayAverage[dayNumber].level0.ToString ();
			level1sideAvgText.text = dayAverage[dayNumber].level1main.ToString ();
			level1mainAvgText.text = dayAverage[dayNumber].level1side.ToString ();
			level2AvgText.text = dayAverage[dayNumber].level2.ToString ();
			uniLinkAvgText.text = dayAverage[dayNumber].uniLink.ToString ();
		}

		// compute the ratios for scale
		if (displayDay.level0 > 0) {
			level0Circle.localScale = Vector3.one * 2 * Mathf.Log (displayDay.level0, 2);
		} else {
			level0Circle.localScale = Vector3.one;
		}

		if (displayDay.level1main > 0) {
			level1mainCircle.localScale = Vector3.one * 2 * Mathf.Log (displayDay.level1main, 2);
		} else {
			level1mainCircle.localScale = Vector3.one;
		}
		if (displayDay.level1side > 0) {
			level1sideCircle.localScale = Vector3.one * 2 * Mathf.Log (displayDay.level1side, 2);
		} else {
			level1sideCircle.localScale = Vector3.one;
		}
		if (displayDay.level2 > 0) {
			level2Circle.localScale = Vector3.one * 2 * Mathf.Log (displayDay.level2, 2);
		} else {
			level2Circle.localScale = Vector3.one;
		}
		if (displayDay.uniLink > 0) {
			uniLinkCircle.localScale = Vector3.one * 2 * Mathf.Log (displayDay.uniLink, 2);
		} else {
			uniLinkCircle.localScale = Vector3.one;
		}
		// set the day info
		DayInfo.singleton.SetDayInfo (displayDay);
		DaySlider.singleton.UpdateDate (displayDay.date.Day, displayDay.date.Month, displayDay.date.Year);

	}

	// Use this for initialization
	void Start () {
	}

	public void SetCurrentDay(int day, int month, int year){
		DateTime desiredDate = new DateTime (year, month, day);
		if (desiredDate > endDate) {
			desiredDate = endDate;
		} else if (desiredDate < startDate) {
			desiredDate = startDate;
		}

		// work out where the day is...

		// determine how many weeks ahead it is
		TimeSpan diff = desiredDate-startDate;
		int weekNumber = diff.Days / 7;
		currentWeek = weeks [weekNumber];
		// set the day to display
		dayNumber = diff.Days % 7;
		//SelectDisplayDay (dayNumber);
		DaySlider.singleton.slider.value = dayNumber;
	}

	public void SelectDisplayDay(int _val){
		dayNumber = _val;
		if (_val == 7) {
			displayDay = currentWeek.weeklyTotal;
		} else {
			displayDay = currentWeek.days [_val];
		}
	}

	public void CreateDataArrays(){
		int amount = DataLoader.singleton.level0.Length;
		int numWeeks = amount / 8;
		DateTime currentDate = startDate;
		for (int i = 0; i < numWeeks; i++) {
			Week newWeek = new Week ();
			newWeek.weeklyTotal.date = currentDate;

			newWeek.days = new Day[7];
			for (int j = 0; j < newWeek.days.Length; j++) {
				newWeek.days [j].date = currentDate;
				newWeek.days [j].level0 = DataLoader.singleton.level0 [i * 8 + j+1];
				newWeek.days [j].level1main = DataLoader.singleton.level1main [i * 8 + j+1];
				newWeek.days [j].level1side = DataLoader.singleton.level1side [i * 8 + j+1];
				newWeek.days [j].level2 = DataLoader.singleton.level2 [i * 8 + j+1];
				newWeek.days [j].uniLink = DataLoader.singleton.uniLink [i * 8 + j+1];
				newWeek.days [j].ComputeDailyTotal ();
				newWeek.days [j].type = SpecialDateInfo.singleton.GetDayType (currentDate);
				// move on 1 day
				currentDate = currentDate.AddDays (1);
			}
			newWeek.ComputeWeeklyTotal ();
			weeks.Add (newWeek);
		}


		dayAverage = new Day[7];
		// compute the average days...
		for (int i = 0; i < weeks.Count; i++) {
			for (int j = 0; j < weeks [i].days.Length; j++) {
				dayAverage [j] += weeks [i].days [j];
			}
		}
		for (int i = 0; i < dayAverage.Length; i++) {
			dayAverage [i].total /= weeks.Count;
			dayAverage [i].level0 /= weeks.Count;
			dayAverage [i].level1main /= weeks.Count;
			dayAverage [i].level1side /= weeks.Count;
			dayAverage [i].level2 /= weeks.Count;
			dayAverage [i].uniLink /= weeks.Count;
		}

		displayDay = weeks [0].weeklyTotal;
		// set the end date to how far we got
		endDate = currentDate.AddDays(-1);
		// set the current week to be the starting week
		currentWeek = weeks [0];

		// update the day display
		DaySlider.singleton.UpdateDate(startDate.Day,startDate.Month, startDate.Year);
		dayNumber = 7;
		UpdateGUI ();
	}

	// Update is called once per frame
	void Update () {
	
	}
}
