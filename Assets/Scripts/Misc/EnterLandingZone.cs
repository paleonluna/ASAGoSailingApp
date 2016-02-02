﻿using UnityEngine;
using System.Collections;

public class EnterLandingZone : MonoBehaviour {

	void OnTriggerExit(Collider other) {
		if (other.tag == "Player") {
			if (MOBManager.s_instance != null) {
				StartCoroutine (MOBManager.s_instance.Land());
			}
			if (DockingManager.s_instance != null) {
				StartCoroutine (DockingManager.s_instance.Land());
			}
		}
	}


}