using UnityEngine;
using System.Collections;
using System;

[System.Serializable]
public struct Day {

	public DayType type;
	public DateTime date;

	public string name;
	public int total;
	public int level0;
	public int level1main;
	public int level1side;
	public int level2;
	public int uniLink;

	public void ComputeDailyTotal(){
		total = level0 + level1main + level1side + level2;
		name = date.ToShortDateString();
	}

	public static Day operator + (Day d1, Day d2){
		Day ret = new Day();
		ret.total = d1.total + d2.total;
		ret.level0 = d1.level0 + d2.level0;
		ret.level1main = d1.level1main + d2.level1main;
		ret.level1side = d1.level1side + d2.level1side;
		ret.level2 = d1.level2 + d2.level2;
		ret.uniLink = d1.uniLink + d2.uniLink;
		return ret;
	}
}
