using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour {
	public GameObject rightController;

	private SteamVR_TrackedController device;
	private SteamVR_TrackedController rightDevice;
	private GameObject menu;
	private GameObject menuPointer;
  private GameObject prevMode;
	private GameObject graphController;
  private GameObject destroyController;


	// Use this for initialization
	void Start () {
		device = GetComponent<SteamVR_TrackedController> ();
		rightDevice = rightController.GetComponent<SteamVR_TrackedController> ();

		menu = transform.Find ("Menu").gameObject;
		menu.SetActive (false);
		menuPointer = rightController.transform.Find("MenuLaser").gameObject;
		graphController = rightController.transform.Find ("GraphController").gameObject;
    destroyController = rightController.transform.Find ("DestroyController").gameObject;
  //  prevMode = graphController;

		device.MenuButtonClicked += ToggleMenu;
	}

	void ToggleMenu(object sender, ClickedEventArgs e) {
		menu.SetActive (!menu.activeSelf);
		menuPointer.SetActive (menu.activeSelf);

    if (menu.activeSelf) {
      if (graphController.activeSelf) {
        prevMode = graphController;
        graphController.SetActive (false);
      } else if (destroyController.activeSelf) {
        prevMode = destroyController;
        destroyController.SetActive (false);
      }
    } else {
      prevMode.SetActive (true);
    }
		
	}
}
