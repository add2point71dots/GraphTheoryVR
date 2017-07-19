using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EdgeMaker : MonoBehaviour {
	public GameObject edge;
	public float selectRadius;
	private SteamVR_TrackedController device;
	private GameObject controller;
	private GameObject edgeDrawing;
	private EdgeController edgeController;
	private bool drawingEdge;
	private GameObject startNode;
	private GameObject endNode;
	private NodeConnections startNodeConnections;
	private NodeConnections endNodeConnections;
	private GameObject selectedNode;


	void Start () {
		drawingEdge = false;
		device = gameObject.GetComponentInParent<SteamVR_TrackedController>();
		device.Gripped += startEdge;
		device.Ungripped += endEdge;
		controller = transform.gameObject;
	}
	
	// Update is called once per frame
	void startEdge(object sender, ClickedEventArgs e) {
		if (!gameObject.activeSelf)
			return;
		
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
		if (!gameObject.activeSelf)
			return;
		
		bool drawingNewEdge = true;
		if (drawingEdge) {
			Collider[] nearbyNodes = Physics.OverlapSphere (transform.position, selectRadius);
			selectedNode = FindNearestNode (nearbyNodes);
			GameObject[] allEdges = GameObject.FindGameObjectsWithTag ("Edge");

			foreach (GameObject edge in allEdges) {
				EdgeController otherEdgeController = edge.GetComponent<EdgeController> ();

				bool duplicate1 = otherEdgeController.start == edgeController.start && otherEdgeController.end == selectedNode;
				bool duplicate2 = otherEdgeController.end == edgeController.start && otherEdgeController.start == selectedNode;

				if (duplicate1 || duplicate2) {
					drawingNewEdge = false;
					break;
				}
			}
			if (selectedNode && drawingNewEdge) {
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
