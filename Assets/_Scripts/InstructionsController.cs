using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstructionsController : MonoBehaviour {
	public GameObject instructions;
	private SteamVR_TrackedController device;

	// Use this for initialization
	void Start () {
		device = GetComponent<SteamVR_TrackedController> ();
		device.MenuButtonClicked += ToggleInstructions;
	}

	void ToggleInstructions(object sender, ClickedEventArgs e) {
		instructions.SetActive (!instructions.activeSelf);
	}
}
