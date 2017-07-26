using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MenuController : MonoBehaviour {
	public GameObject rightController;
  public GameObject colorPalette;
	private SteamVR_TrackedController device;
	private SteamVR_TrackedController rightDevice;
	private GameObject menu;
	private GameObject menuPointer;
	private TextMeshPro controllerLabelText;
	private string prevLabelText;
	private GameObject prevMode;
	private GameObject graphController;
	private GameObject destroyController;
	private GameObject colorController;

	void Start () {
		rightDevice = rightController.GetComponent<SteamVR_TrackedController> ();
		menuPointer = rightController.transform.Find("MenuLaser").gameObject;
		controllerLabelText = rightController.transform.Find ("RightControllerLabel").gameObject.GetComponent<TextMeshPro>();
		graphController = rightController.transform.Find ("GraphController").gameObject;
		destroyController = rightController.transform.Find ("DestroyController").gameObject;
		colorController = rightController.transform.Find ("ColorController").gameObject;
		menu = transform.Find ("Menu").gameObject;
		menu.SetActive (false);

		device = GetComponent<SteamVR_TrackedController> ();
		device.MenuButtonClicked += ToggleMenu;
	}

	void ToggleMenu(object sender, ClickedEventArgs e) {
		menu.SetActive (!menu.activeSelf);
		menuPointer.SetActive (menu.activeSelf);

		if (menu.activeSelf) {
			prevLabelText = controllerLabelText.text;
			controllerLabelText.text = "selector";

			if (graphController.activeSelf) {
				prevMode = graphController;
				graphController.SetActive (false);
			} else if (destroyController.activeSelf) {
				prevMode = destroyController;
				destroyController.SetActive (false);
			} else if (colorController.activeSelf) {
				prevMode = colorController;
				colorController.SetActive (false);
				colorPalette.SetActive (false);
			}
		} else {
			if (prevMode == colorController)
				colorPalette.SetActive(true);

			prevMode.SetActive (true);
			controllerLabelText.text = prevLabelText;
		}	
	}
}
