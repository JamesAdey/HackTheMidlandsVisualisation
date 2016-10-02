using UnityEngine;
using System.Collections;

[System.Flags, System.Serializable]
public enum DayType : byte {
	Open = 1,
	SchoolHoliday = 2,
	BCUTerm = 4,
	Event = 8
}
