﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EdgeMaker : MonoBehaviour {
	private SteamVR_TrackedController device;
	public GameObject edge;
	GameObject edgeDrawing;
	private EdgeController edgeController;
	private bool drawingEdge;
	private Transform lineStart;
	private Transform lineEnd;
//	public Transform nodeSpawn;	

	void Start () {
		drawingEdge = false;
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
			lineStart = hit.transform;
			lineEnd = transform;

			edgeDrawing = Instantiate (edge);
			edgeController = edgeDrawing.GetComponent<EdgeController> ();
			edgeController.start = lineStart;
			edgeController.end = lineEnd;

			drawingEdge = true;
			Debug.Log ("I've hit something!");
		} else {
			Debug.Log ("no hits!");
		}
	}

	void endEdge(object sender, ClickedEventArgs e) {
		Debug.Log ("drawingedge is: " + drawingEdge);
		if (drawingEdge) {
			Debug.Log ("DRAWING AN EDGE");
			RaycastHit hit;
			Ray ray = new Ray(transform.position, transform.forward);
			if (Physics.Raycast (ray, out hit, 0.1f) && edgeController.start != hit.transform) {
				edgeController.end = hit.transform;
			} else {
				Destroy (edgeDrawing);
			}
		}
		drawingEdge = false;
		Debug.Log ("UNGRIPPING");
	}


}
