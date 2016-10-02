using UnityEngine;
using System.Collections;

public class CameraControls : MonoBehaviour {

	public Vector3[] floors;
	public int currentFloor = 0;


	float pitch = 45;
	float yaw;

	Vector3 targetPos;
	Transform thisTransform;

	Quaternion desiredRot = Quaternion.Euler (45, 0, 0);

	// Use this for initialization
	void Start () {
		thisTransform = this.transform;
	}

	public void GoUp(){
		if (currentFloor < floors.Length - 1) {
			currentFloor += 1;
		}
	}

	public void GoDown  () {
		if (currentFloor > 0) {
			currentFloor -= 1;
		}
	}

	// Update is called once per frame
	void Update () {
		targetPos = floors [currentFloor];
		thisTransform.position = Vector3.Lerp (thisTransform.position, targetPos, 2*Time.deltaTime);
		if (Input.GetKeyUp (KeyCode.UpArrow)) {
			GoUp ();
		}
		if(Input.GetKeyUp(KeyCode.DownArrow)){
			GoDown ();
		}
		if (Input.GetMouseButton (1)) {
			yaw -= Input.GetAxis ("Mouse X");
			pitch += Input.GetAxis ("Mouse Y");
			pitch = Mathf.Clamp (pitch, 45, 90);
			desiredRot = Quaternion.Euler (pitch, yaw, 0);
		}
		if (Input.GetKey (KeyCode.Escape)) {
			Application.Quit ();
		}
		thisTransform.rotation = desiredRot;

	}

}
