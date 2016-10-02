using UnityEngine;
using System.Collections;

[System.Serializable]
public class Week {
	public Day weeklyTotal;
	public Day weeklyAverage;

	public Day[] days;

	public void ComputeWeeklyTotal(){
		weeklyTotal = new Day ();
		weeklyTotal.date = days [0].date;
		weeklyTotal.name = days [0].name;
		//Debug.Log(days.Length);
		for (int i = 0; i < days.Length; i++) {
			weeklyTotal.total += days [i].total;
			weeklyTotal.level0 += days [i].level0;
			weeklyTotal.level1main += days [i].level1main;
			weeklyTotal.level1side += days [i].level1side;
			weeklyTotal.level2 += days [i].level2;
			weeklyTotal.uniLink += days [i].uniLink;
			weeklyTotal.type = weeklyTotal.type | days [i].type;
		}

		// set the averages
		weeklyAverage.name = weeklyTotal.name;
		weeklyAverage.total = weeklyTotal.total / 7;
		weeklyAverage.level0 = weeklyTotal.level0 / 7;
		weeklyAverage.level1main = weeklyTotal.level1main / 7;
		weeklyAverage.level1side = weeklyTotal.level1side / 7;
		weeklyAverage.level2 = weeklyTotal.level2 / 7;
		weeklyAverage.uniLink = weeklyTotal.uniLink / 7;
	}

}
