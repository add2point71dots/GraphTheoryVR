using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour {
	private SteamVR_TrackedController device;

	void Start () {
		device = GetComponent<SteamVR_TrackedController>();
		device.TriggerClicked += Destroy;
	}

	void Destroy(object sender, ClickedEventArgs e) {
		Debug.Log ("DESTROYING");
		RaycastHit hit;
		Ray ray = new Ray(transform.position, transform.forward);

		if (Physics.Raycast (ray, out hit, 100f)) {
			GameObject hitObj = hit.transform.gameObject;

			Debug.Log ("hit is a:" + hit.transform.gameObject.GetType ());

			if (hitObj.tag == "Node" || hitObj.tag == "Edge") {
				Destroy (hitObj);
			}
		}
	}
}
