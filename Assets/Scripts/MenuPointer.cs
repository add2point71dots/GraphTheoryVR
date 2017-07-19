using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPointer : MonoBehaviour {
  public GameObject leftController;

	private SteamVR_TrackedController device;
  private SteamVR_TrackedController leftDevice;
	private GameObject graphController;
  private GameObject destroyController;
  private GameObject colorController;
  private GameObject menu;

	// Use this for initialization
	void Start () {
		graphController = transform.parent.Find ("GraphController").gameObject;
    destroyController = transform.parent.Find ("DestroyController").gameObject;
    colorController = transform.parent.Find ("ColorController").gameObject;

    menu = leftController.transform.Find ("Menu").gameObject;

		device = gameObject.GetComponentInParent<SteamVR_TrackedController>();
		device.TriggerClicked += selectMode;
	}

	void selectMode(object sender, ClickedEventArgs e) {
		if (!gameObject.activeSelf)
			return;

		RaycastHit hit;
		Ray ray = new Ray(transform.position, transform.forward);

		if (Physics.Raycast (ray, out hit, 100f)) {
			Debug.Log ("HIT NAMRE " + hit.transform.name);

      if (hit.transform.name == "GraphButton") {
        Debug.Log ("hit the graph button!");
        graphController.SetActive (true);
        menu.SetActive (false);
        gameObject.SetActive (false);
      } else if (hit.transform.name == "DestroyButton") {
        destroyController.SetActive (true);
        menu.SetActive (false);
        gameObject.SetActive (false);
      } else if (hit.transform.name == "ColorButton") {
        colorController.SetActive (true);
        menu.SetActive (false);
        gameObject.SetActive (false);
      }
		}
	}
}
