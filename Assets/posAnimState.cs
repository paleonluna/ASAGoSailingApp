﻿using UnityEngine;
using System.Collections;

public class posAnimState : MonoBehaviour {

	public Animator boatAnim;
	public GameManager gameManager;


	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

		//Cast ray at point of mouse click
		if (Input.GetButtonDown("Fire1")) 
		{
			Ray ray;
			RaycastHit hit;
			ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			if (Physics.Raycast(ray, out hit, 1000.0f)){

				//checks which point of sail object was clicked for animation states
				switch(hit.collider.gameObject.name){

				case "Irons":
					boatAnim.SetTrigger("Irons");
					gameManager.currAnimState = "In Irons";
					break;
				case "PTCloseHaul":
					boatAnim.SetTrigger("PTCloseHaul");
					gameManager.currAnimState = "Close Hauled Port Tack";
					break;
				case "PTCloseReach":
					boatAnim.SetTrigger("PTCloseReach");
					gameManager.currAnimState = "Close Reach Port Tack";
					break;

				case "PTBeamReach":
					boatAnim.SetTrigger ("PTBeamReach");
					gameManager.currAnimState = "Beam Reach Port Tack";
					break;

				case "PTBroadReach":
					boatAnim.SetTrigger("PTBroadReach");
					gameManager.currAnimState = "Broad Reach Port Tack";
					break;
				case "Run":
					boatAnim.SetTrigger("Run");
					gameManager.currAnimState = "Run";
					break;
				case "STBroadReach":
					boatAnim.SetTrigger("STBroadReach");
					gameManager.currAnimState = "Broad Reach Starboard Tack";
					break;
				case "STBeamReach":
					boatAnim.SetTrigger("STBeamReach");
					gameManager.currAnimState = "Beam Reach Starboard Tack";
					break;
				case "STCloseReach":
					boatAnim.SetTrigger("STCloseReach");
					gameManager.currAnimState = "Close Reach Starboard Tack";
					break;
				case "STCloseHaul":
					boatAnim.SetTrigger("STCloseHaul");
					gameManager.currAnimState = "Close Hauled Starboard Tack";
					break;


				}

			}


		
		}



	}
}