using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeMover : MonoBehaviour {
	private SteamVR_TrackedController device;
	private GameObject grabbedObject;
	public float grabRadius;

	void Start () {
		device = GetComponent<SteamVR_TrackedController>();
		device.PadClicked += grabNode;
		device.PadUnclicked += dropNode;
	}

	void grabNode(object sender, ClickedEventArgs e) {
		Collider[] nearbyNodes = Physics.OverlapSphere (transform.position, grabRadius);
		Debug.Log ("I found " + nearbyNodes.Length + " nearby nodes.");

		grabbedObject = FindNearestNode (nearbyNodes);
		grabbedObject.transform.parent = transform;
	}

	void dropNode(object sender, ClickedEventArgs e) {
		grabbedObject.transform.parent = null;
	}

	GameObject FindNearestNode(Collider[] nearbyNodes) {
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
