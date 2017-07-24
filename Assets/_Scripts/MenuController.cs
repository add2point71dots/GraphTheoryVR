using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour {
	public GameObject rightController;

	private SteamVR_TrackedController device;
	private SteamVR_TrackedController rightDevice;
	private GameObject menu;
  private GameObject colorPalette;
	private GameObject menuPointer;
  private TextMesh controllerLabelText;
  private string prevLabelText;
  private GameObject prevMode;
	private GameObject graphController;
  private GameObject destroyController;
  private GameObject colorController;


	// Use this for initialization
	void Start () {
		device = GetComponent<SteamVR_TrackedController> ();
		rightDevice = rightController.GetComponent<SteamVR_TrackedController> ();

		menu = transform.Find ("Menu").gameObject;
		menu.SetActive (false);
		menuPointer = rightController.transform.Find("MenuLaser").gameObject;
    controllerLabelText = rightController.transform.Find ("RightControllerLabel").gameObject.GetComponent<TextMesh>();
		graphController = rightController.transform.Find ("GraphController").gameObject;
    destroyController = rightController.transform.Find ("DestroyController").gameObject;
    colorController = rightController.transform.Find ("ColorController").gameObject;
    colorPalette = transform.Find ("ColorPalette").gameObject;

		device.MenuButtonClicked += ToggleMenu;
	}

	void ToggleMenu(object sender, ClickedEventArgs e) {
		menu.SetActive (!menu.activeSelf);
		menuPointer.SetActive (menu.activeSelf);

    if (menu.activeSelf) {
      prevLabelText = controllerLabelText.text;
      controllerLabelText.text = "Menu Selector";

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
