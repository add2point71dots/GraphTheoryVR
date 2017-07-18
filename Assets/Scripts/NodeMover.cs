using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeMover : MonoBehaviour {
	private SteamVR_TrackedController device;
	private GameObject grabbedObject;
	public float grabRadius;

	void Start () {
		device = gameObject.GetComponentInParent<SteamVR_TrackedController>();
		device.PadClicked += grabNode;
		device.PadUnclicked += dropNode;
	}

	void grabNode(object sender, ClickedEventArgs e) {
		if (!gameObject.activeSelf)
			return;
		
		Collider[] nearbyNodes = Physics.OverlapSphere (transform.position, grabRadius);
		grabbedObject = FindNearestNode (nearbyNodes);

		if (grabbedObject)
			grabbedObject.transform.parent = gameObject.transform.parent.transform;

	}

	void dropNode(object sender, ClickedEventArgs e) {
		if (!gameObject.activeSelf)
			return;
		
		if (grabbedObject)
			grabbedObject.transform.parent = null;
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
