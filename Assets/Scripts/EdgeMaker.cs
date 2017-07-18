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
	private GameObject selectedNode;
	public float selectRadius;

	void Start () {
		drawingEdge = false;
		device = gameObject.GetComponentInParent<SteamVR_TrackedController>();
		device.Gripped += startEdge;
		device.Ungripped += endEdge;
		controller = transform.gameObject;
	}
	
	// Update is called once per frame
	void startEdge(object sender, ClickedEventArgs e) {
		Collider[] nearbyNodes = Physics.OverlapSphere (transform.position, selectRadius);
		selectedNode = FindNearestNode (nearbyNodes);

		if (selectedNode) {
			startNode = selectedNode;
			startNodeConnections = startNode.GetComponent<NodeConnections> ();

			edgeDrawing = Instantiate (edge);
			edgeController = edgeDrawing.GetComponent<EdgeController> ();
			edgeController.start = startNode;
			edgeController.end = controller;

			drawingEdge = true;
		}
	}

	void endEdge(object sender, ClickedEventArgs e) {
		if (drawingEdge) {
			Collider[] nearbyNodes = Physics.OverlapSphere (transform.position, selectRadius);
			selectedNode = FindNearestNode (nearbyNodes);
			
			if (selectedNode) {
				endNode = selectedNode;
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

	GameObject FindNearestNode(Collider[] nearbyNodes) {
		if (nearbyNodes.Length == 0)
			return null;
		GameObject nearestNode = nearbyNodes[0].gameObject;
		float smallestDist = 500f;
		bool foundNode = false;
		GameObject tempNode;
		float tempDist;

		for (int i = 0; i < nearbyNodes.Length; i++) {
			if (nearbyNodes [i].tag == "Node") {
				tempNode = nearbyNodes [i].gameObject; 
				tempDist = (tempNode.transform.position - transform.position).sqrMagnitude;
				if (tempDist < smallestDist) {
					smallestDist = tempDist;
					nearestNode = tempNode;
				}
				foundNode = true;
			}
		}

		if (foundNode) {
			return nearestNode;
		}
		return null;
	}

}
