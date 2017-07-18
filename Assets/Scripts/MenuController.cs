using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour {
	public GameObject rightController;

	private SteamVR_TrackedController device;
	private SteamVR_TrackedController rightDevice;
	private GameObject menu;
	private SteamVR_LaserPointer menuPointer;


	// Use this for initialization
	void Start () {
		device = GetComponent<SteamVR_TrackedController> ();
		rightDevice = rightController.GetComponent<SteamVR_TrackedController> ();
		menu = transform.Find ("Menu").gameObject;
		menu.SetActive (false);
		menuPointer = rightController.GetComponent<SteamVR_LaserPointer> ();
		device.MenuButtonClicked += ToggleMenu;
	}
	
	void ToggleMenu(object sender, ClickedEventArgs e) {
		menu.SetActive (!menu.activeSelf);

		menuPointer.enabled = menu.activeSelf;
	}
}
