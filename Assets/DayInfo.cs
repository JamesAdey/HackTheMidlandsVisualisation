using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DayInfo : MonoBehaviour {

	public static DayInfo singleton;

	public Toggle holidayToggle;
	public Toggle openToggle;
	public Toggle eventToggle;
	public Toggle bcuTermToggle;

	void Awake () {
		singleton = this;
	}

	// Use this for initialization
	void Start () {
		
	}

	public void SetDayInfo(Day _d){
		if ((_d.type & DayType.Open) != 0) {
			openToggle.isOn = true;
		} else {
			openToggle.isOn = false;
		}
		if ((_d.type & DayType.BCUTerm) != 0) {
			bcuTermToggle.isOn = true;
		} else {
			bcuTermToggle.isOn = false;
		}
		if ((_d.type & DayType.SchoolHoliday) != 0) {
			holidayToggle.isOn = true;
		} else {
			holidayToggle.isOn = false;
		}
		if ((_d.type & DayType.Event) != 0) {
			eventToggle.isOn = true;
		} else {
			eventToggle.isOn = false;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
