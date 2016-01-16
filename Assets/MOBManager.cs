﻿using UnityEngine;
using System.Collections;

public class MOBManager : MonoBehaviour {
	enum MOBState {intro, gameplay, reset,win};
	MOBState curState;
	public static MOBManager s_instance;
	[SerializeField]
	GameObject playerBoat;
	[SerializeField]
	GameObject setup1, setup2;
	bool switchToGamePlay, switchToReset;
	[SerializeField]
	Fader win,lose;
	[SerializeField]
	Camera overhead, main;

	[SerializeField]
	Transform setup1transform, setup2transform;

	void Awake() {
		if (s_instance == null) {
			s_instance = this;
		} else {
			Destroy(gameObject);
		}
	}

	void Update () {
		switch (curState) {
		case MOBState.intro:
			{
				if (switchToGamePlay) {
					playerBoat.GetComponent<Rigidbody> ().isKinematic = false;
					switchToGamePlay = false;
					curState = MOBState.gameplay;
				}
				break;
			}
		case MOBState.gameplay:
			{
				if (switchToReset) {
					switchToReset = false;
					curState = MOBState.reset;
				}
				break;
			}
		case MOBState.reset:
			{
				StartCoroutine ("PauseBoats");
				curState = MOBState.gameplay;
				break;
			}
		}
	}

	public void StartGame() {
		switchToGamePlay = true;
		CameraMain ();
	}

	public void WinScenario() {
		win.StartFadeOut ();
		StartCoroutine ("WinReset");
	}

	public void Fail(){
		lose.StartFadeOut ();
		StopAllCoroutines ();
		StartCoroutine ("FailReset");
	}

	public IEnumerator Land() {
		yield return new WaitForSeconds (3f);
		WinScenario ();
	}

	void CameraMain() {
		
		overhead.GetComponent<Camera> ().enabled = false;
		main.GetComponent<Camera> ().enabled = true;
	}

	void CameraOverhead() {
		overhead.GetComponent<Camera> ().enabled = true;
		main.GetComponent<Camera> ().enabled = false;
	}

	IEnumerator FailReset () {
		CameraOverhead ();
		yield return new WaitForSeconds (3f);
		CameraMain ();
		if (setup2.gameObject.activeSelf == false) {
			playerBoat.transform.position = setup1transform.position;
			playerBoat.transform.rotation = setup1transform.rotation;
		} else {
			playerBoat.transform.position = setup2transform.position;
			playerBoat.transform.rotation = setup2transform.rotation;
		}

	}

	IEnumerator WinReset () {
		CameraOverhead ();
		yield return new WaitForSeconds (3f);
		CameraMain ();
		if (setup2.activeSelf == false) {
			setup2.SetActive (true);
			setup1.SetActive (false);
			playerBoat.transform.position = setup2transform.position;
			playerBoat.transform.rotation = setup2transform.rotation;
		} else {
			curState = MOBState.win;
		}
	}
}
