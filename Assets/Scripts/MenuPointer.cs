using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPointer : MonoBehaviour {
	private SteamVR_TrackedController device;

	// Use this for initialization
	void Start () {
		device = gameObject.GetComponentInParent<SteamVR_TrackedController>();
		device.TriggerClicked += selectMode;
	}
	
	// Update is called once per frame
	void selectMode(object sender, ClickedEventArgs e) {
		if (!gameObject.activeSelf)
			return;

		RaycastHit hit;
		Ray ray = new Ray(transform.position, transform.forward);

		if (Physics.Raycast (ray, out hit, 100f)) {
			Debug.Log ("HIT NAMRE " + hit.transform.name);
			if (hit.transform.name == "GraphButton") {
				Debug.Log ("hit the graph button!");
			}
		}
	}
}
