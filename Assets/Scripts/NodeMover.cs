using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeMover : MonoBehaviour {
	private SteamVR_TrackedController device;

	void Start () {
		device = GetComponent<SteamVR_TrackedController>();
		device.PadClicked += grabNode;
		device.PadUnclicked += dropNode;
	}

	void grabNode(object sender, ClickedEventArgs e) {
		Collider[] nearbyNodes = Physics.OverlapSphere (transform.position, 0.1f);
		Debug.Log ("I found " + nearbyNodes.Length + " nearby nodes.");

		FindNearestNode (nearbyNodes);
	}

	void dropNode(object sender, ClickedEventArgs e) {
		Debug.Log ("Tryan DROP");
		
	}

	GameObject FindNearestNode(Collider[] nearbyNodes) {
		Debug.Log ("finding nearest!");
		GameObject nearestNode;
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
					Debug.Log ("smallest dist is " + smallestDist);
					Debug.Log ("nearest node is: " + nearestNode);

				}
				foundNode = true;
				Debug.Log ("smallest distance is " + smallestDist);
			}

		}

		if (foundNode) {
			Debug.Log ("I found a node!");
			return nearestNode;
		}
		return null;
	}
}
