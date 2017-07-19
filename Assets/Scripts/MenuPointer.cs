using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPointer : MonoBehaviour {
	private SteamVR_TrackedController device;
	private GameObject graphController;

	// Use this for initialization
	void Start () {
		graphController = transform.parent.Find ("GraphController").gameObject;
		device = gameObject.GetComponentInParent<SteamVR_TrackedController>();
		device.TriggerClicked += selectMode;
	}

	void selectMode(object sender, ClickedEventArgs e) {
		if (!gameObject.activeSelf)
			return;

		graphController.SetActive (false);

		RaycastHit hit;
		Ray ray = new Ray(transform.position, transform.forward);

		if (Physics.Raycast (ray, out hit, 100f)) {
			Debug.Log ("HIT NAMRE " + hit.transform.name);
			if (hit.transform.name == "GraphButton") {
				Debug.Log ("hit the graph button!");
				graphController.SetActive (true);
			}
		}
	}
}
