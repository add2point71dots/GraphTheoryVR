using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour {
	public GameObject rightController;

	private SteamVR_TrackedController device;
	private SteamVR_TrackedController rightDevice;
	private GameObject menu;
	private GameObject menuPointer;
//	private GameObject graphController;


	// Use this for initialization
	void Start () {
		device = GetComponent<SteamVR_TrackedController> ();
		rightDevice = rightController.GetComponent<SteamVR_TrackedController> ();
		menu = transform.Find ("Menu").gameObject;
		menu.SetActive (false);
		menuPointer = rightController.transform.Find("MenuLaser").gameObject;
//		graphController = rightController.transform.Find ("GraphController").gameObject;
		device.MenuButtonClicked += ToggleMenu;
	}

	void ToggleMenu(object sender, ClickedEventArgs e) {
		menu.SetActive (!menu.activeSelf);
		menuPointer.SetActive (menu.activeSelf);
//		graphController.SetActive (!menu.activeSelf);
	}
}
