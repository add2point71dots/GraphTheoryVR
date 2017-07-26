using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeMover : MonoBehaviour {
	public float grabRadius;
	private SteamVR_TrackedController device;
	private GameObject grabbedObject;

	void Start () {
		device = gameObject.GetComponentInParent<SteamVR_TrackedController>();
		device.Gripped += grabNode;
		device.Ungripped += dropNode;
	}

	void grabNode (object sender, ClickedEventArgs e) {
		if (!gameObject.activeSelf)
			return;
		
		Collider[] nearbyNodes = Physics.OverlapSphere (transform.position, grabRadius);
		grabbedObject = FindNearestNode (nearbyNodes);

		if (grabbedObject) {
			NodeConnections nodeConnections = grabbedObject.GetComponent<NodeConnections> ();
			grabbedObject.transform.parent = gameObject.transform.parent.transform;

			if (nodeConnections.connectedEdges.Count > 0)
				nodeConnections.connectedEdges[0].GetComponent<AudioSource>().Play();
		}
	}

	void dropNode (object sender, ClickedEventArgs e) {
		if (!gameObject.activeSelf)
			return;
		
		if (grabbedObject) {
			NodeConnections nodeConnections = grabbedObject.GetComponent<NodeConnections> ();
			grabbedObject.transform.parent = null;

			if (nodeConnections.connectedEdges.Count > 0)
				nodeConnections.connectedEdges[0].GetComponent<AudioSource>().Stop();
		}
	}

	GameObject FindNearestNode(Collider[] nearbyNodes) {
		if (nearbyNodes.Length == 0)
			return null;

		GameObject nearestNode = nearbyNodes[0].gameObject;
		GameObject tempNode;
		float smallestDist = 500f;
		float tempDist;
		bool foundNode = false;

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

		if (foundNode)
			return nearestNode;

		return null;
	}
}
