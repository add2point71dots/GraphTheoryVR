using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour {
	public GameObject rightController;

	private SteamVR_TrackedController device;
	private SteamVR_TrackedController rightDevice;
	private GameObject menu;
	private GameObject menuPointer;


	// Use this for initialization
	void Start () {
		device = GetComponent<SteamVR_TrackedController> ();
		rightDevice = rightController.GetComponent<SteamVR_TrackedController> ();
		menu = transform.Find ("Menu").gameObject;
		menu.SetActive (false);
		menuPointer = rightController.transform.Find("Laser").gameObject;
		device.MenuButtonClicked += ToggleMenu;
	}

	void ToggleMenu(object sender, ClickedEventArgs e) {
		Debug.Log ("CLICK");
		menu.SetActive (!menu.activeSelf);

		menuPointer.SetActive (menu.activeSelf);
	}
}
