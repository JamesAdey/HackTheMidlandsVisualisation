using UnityEngine;
using System.Collections;

public class VisitorText : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {
		transform.rotation = Quaternion.LookRotation (transform.position - Camera.main.transform.position);
	}
}
