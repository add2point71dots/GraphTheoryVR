using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour {
	public GameObject fireball;
	public GameObject graph;
	private SteamVR_TrackedController device;
	private ValidColoringChecker validColoringChecker;

	void Start () {
		validColoringChecker = graph.GetComponent<ValidColoringChecker>();

		device = gameObject.GetComponentInParent<SteamVR_TrackedController>();
		device.TriggerClicked += Destroy;
	}

	void Destroy(object sender, ClickedEventArgs e) {
		if (!gameObject.activeSelf)
			return;

		RaycastHit hit;
		Ray ray = new Ray(transform.position, transform.forward);

		if (Physics.Raycast (ray, out hit, 100f)) {
			GameObject hitObj = hit.transform.gameObject;

			if (hitObj.tag == "Node") {
				NodeConnections nodeConnections = hitObj.GetComponent<NodeConnections> ();

				for (int i = 0; i < nodeConnections.connectedEdges.Count; i++) {
					GameObject edge = nodeConnections.connectedEdges [i];

					GameObject startNode = edge.GetComponent<EdgeController> ().start;
					GameObject endNode = edge.GetComponent<EdgeController> ().end;

					GameObject otherNode = (startNode == hitObj) ? endNode : startNode;
					otherNode.GetComponent<NodeConnections> ().connectedEdges.Remove (edge);

					validColoringChecker.badEdges.Remove (edge);
					Destroy (edge);
				}

				for (int i = 0; i < nodeConnections.adjacentNodes.Count; i++) {
					NodeConnections adjNodeConnections = nodeConnections.adjacentNodes [i].GetComponent<NodeConnections> ();
					adjNodeConnections.adjacentNodes.Remove (hitObj);
				}

				Instantiate (fireball, hitObj.transform.position, hitObj.transform.rotation);
				Destroy (hitObj);

			} else if (hitObj.tag == "Edge") {
				GameObject startNode = hitObj.GetComponent<EdgeController> ().start;
				GameObject endNode = hitObj.GetComponent<EdgeController> ().end;

				NodeConnections startNodeConnections = startNode.GetComponent<NodeConnections> ();
				NodeConnections endNodeConnections = endNode.GetComponent<NodeConnections> ();

				startNodeConnections.connectedEdges.Remove (hitObj);
				endNodeConnections.connectedEdges.Remove (hitObj);
				startNodeConnections.adjacentNodes.Remove (endNode);
				endNodeConnections.adjacentNodes.Remove (startNode);
				validColoringChecker.badEdges.Remove (hitObj);

				Destroy (hitObj);
			}
		}
	}
}
