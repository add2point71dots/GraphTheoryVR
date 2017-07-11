using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EdgeMaker : MonoBehaviour {
	private SteamVR_TrackedController device;
	public GameObject edge;
//	public Transform nodeSpawn;	

	void Start () {
		device = GetComponent<SteamVR_TrackedController>();
		device.Gripped += startEdge;
		device.Ungripped += endEdge;
	}
	
	// Update is called once per frame
	void startEdge(object sender, ClickedEventArgs e) {
		Debug.Log ("GRIPPING");
		RaycastHit hit;
		Ray ray = new Ray(transform.position, transform.forward);
		if (Physics.Raycast (ray, out hit, 0.1f)) {
			Debug.Log ("I've hit something!");
		} else {
			Debug.Log ("no hits!");
		}
	}

	void endEdge(object sender, ClickedEventArgs e) {
		Debug.Log ("UNGRIPPING");
	}


}
