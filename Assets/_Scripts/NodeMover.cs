using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeMover : MonoBehaviour {
	public float grabRadius;

	private SteamVR_TrackedController device;
	private GameObject grabbedObject;
	private AudioSource edgeSound;

	void Start () {
		edgeSound = GetComponent<AudioSource>();
		edgeSound.Play();
		device = gameObject.GetComponentInParent<SteamVR_TrackedController>();
		device.Gripped += grabNode;
		device.Ungripped += dropNode;
	}

	void grabNode (object sender, ClickedEventArgs e)
	{
		if (!gameObject.activeSelf)
			return;
		
		Collider[] nearbyNodes = Physics.OverlapSphere (transform.position, grabRadius);
		grabbedObject = FindNearestNode (nearbyNodes);

		if (grabbedObject) {
			if (grabbedObject.GetComponent<NodeConnections>().connectedEdges.Count > 0)
				edgeSound.Play();

			grabbedObject.transform.parent = gameObject.transform.parent.transform;
		}

	}

	void dropNode (object sender, ClickedEventArgs e)
	{
		if (!gameObject.activeSelf)
			return;
		
		if (grabbedObject) {
			if (edgeSound.isPlaying)
				edgeSound.Stop ();

			grabbedObject.transform.parent = null;
		}
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
