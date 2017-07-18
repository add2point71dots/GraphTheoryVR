using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EdgeMaker : MonoBehaviour {
	private SteamVR_TrackedController device;
	private GameObject controller;
	public GameObject edge;
	GameObject edgeDrawing;
	private EdgeController edgeController;
	private bool drawingEdge;
	private GameObject startNode;
	private GameObject endNode;
	private NodeConnections startNodeConnections;
	private NodeConnections endNodeConnections;

	void Start () {
		drawingEdge = false;
		device = GetComponent<SteamVR_TrackedController>();
		device.Gripped += startEdge;
		device.Ungripped += endEdge;
		controller = transform.gameObject;
		Debug.Log ("CONTROLLER IS " + controller);
	}
	
	// Update is called once per frame
	void startEdge(object sender, ClickedEventArgs e) {
		Debug.Log ("GRIPPING");
		RaycastHit hit;
		Ray ray = new Ray(transform.position, transform.forward);
		if (Physics.Raycast (ray, out hit, 0.1f)) {
			GameObject hitObject = hit.transform.gameObject;
			if (hitObject.tag == "Node") {
				startNode = hitObject;
				startNodeConnections = hitObject.GetComponent<NodeConnections> ();

				edgeDrawing = Instantiate (edge);
				edgeController = edgeDrawing.GetComponent<EdgeController> ();
				edgeController.start = startNode;
				edgeController.end = controller;

				drawingEdge = true;
				Debug.Log ("I've hit something!");
			}
		} else {
			Debug.Log ("no hits!");
		}
	}

	void endEdge(object sender, ClickedEventArgs e) {
		if (drawingEdge) {
			RaycastHit hit;
			Ray ray = new Ray(transform.position, transform.forward);
			if (Physics.Raycast (ray, out hit, 0.1f) && edgeController.start != hit.transform && hit.transform.gameObject.tag == "Node") {
				endNode = hit.transform.gameObject;
				edgeController.end = endNode;
					
				endNodeConnections = endNode.GetComponent<NodeConnections> ();

				startNodeConnections.connectedEdges.Add (edgeDrawing);
				endNodeConnections.connectedEdges.Add (edgeDrawing);

				startNodeConnections.adjacentNodes.Add (endNode);
				endNodeConnections.adjacentNodes.Add (startNode);


			} else {
				Destroy (edgeDrawing);
			}
		}
		drawingEdge = false;
	}


}
