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
		
	}

	void endEdge(object sender, ClickedEventArgs e) {
		
	}


}
