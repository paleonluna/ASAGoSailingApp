﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
/*
	This class handles game flow in the Trim Module
*/
public class TrimManager : MonoBehaviour {
	public static TrimManager s_instance;

	enum TrimManagerState {Intro, Playing, Complete};
	TrimManagerState thisTrimManagerState;
	private float[] listOfPositions = {50f, 70f, 90f, 135f, 178f, 225f, 270f, 290f, 310f};
	bool answerSubmitted;
	int posIndex = 0;
	[SerializeField]
	Button submitButton, gotoNextModule;
	[SerializeField]
	Slider sailEfficiencySlider, trimSlider;
	[SerializeField]
	GameObject introText,goodJob,complete,panel;

	//switches
	bool switchToPlaying, switchToComplete;

	void Awake() {
		if (s_instance == null) {
			s_instance = this;
		}
		else {
			Destroy(gameObject);
			Debug.LogWarning( "Deleting "+ gameObject.name +" because it is a duplicate TrimManager." );
		}
	}
		
	void Start () {
		submitButton.gameObject.SetActive(false);
	}

	void Update () {
		switch (thisTrimManagerState) {
		case TrimManagerState.Intro: 
			if (switchToPlaying) {
				thisTrimManagerState = TrimManagerState.Playing;
				submitButton.gameObject.SetActive(true);
				switchToPlaying = false;
			}
			break;

		case TrimManagerState.Playing:
			SubmitButtonLogic ();
			if (switchToComplete) {
				switchToComplete = false;
				submitButton.gameObject.SetActive( false );
				gotoNextModule.gameObject.SetActive (true);
				complete.SetActive (true);
				thisTrimManagerState = TrimManagerState.Complete;
				panel.SetActive (false);
				CongratulationsPopUp.s_instance.InitializeCongratulationsPanel( "Trimming" );
			}
			break;

		case TrimManagerState.Complete:

			break;
		}
	}

	public void BeginTutorial () {
		introText.GetComponent<Fader> ().StartFade ();
		posIndex = 0;
		GameObject.FindGameObjectWithTag("Player").transform.rotation = Quaternion.Euler(new Vector3(0,listOfPositions[posIndex],0));
		switchToPlaying = true;
	}

	public void NextPOSButton () {
		trimSlider.value = 80;
		posIndex++;
		if (posIndex > 2 && posIndex < 6) {
			trimSlider.value = 5;

		}
		goodJob.GetComponent<Fader> ().StartFade ();
		if (posIndex >= listOfPositions.Length) {
			switchToComplete = true;
			return;
		} else {
			GameObject.FindGameObjectWithTag ("Player").transform.rotation = Quaternion.Euler (new Vector3 (0, listOfPositions [posIndex], 0));
		}
	}

	void SubmitButtonLogic () {
		if (sailEfficiencySlider.value > .8f) {
			submitButton.interactable = true;
		} else {
			submitButton.interactable = false;
		}
	}
}
