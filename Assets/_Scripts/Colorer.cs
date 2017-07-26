using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Colorer : MonoBehaviour {	
	public GameObject graph;
	public Color defaultColor;
	private Color selectedColor;
	private SteamVR_LaserPointer colorLaser;
	private ValidColoringChecker validColoringChecker;
	private SteamVR_TrackedController device;
	private AudioSource colorSound;

	void Start () {
		colorLaser = gameObject.GetComponent<SteamVR_LaserPointer> ();
		selectedColor = defaultColor;
		validColoringChecker = graph.GetComponent<ValidColoringChecker>();
		colorSound = GetComponent<AudioSource>();

		device = gameObject.GetComponentInParent<SteamVR_TrackedController>();
		device.TriggerClicked += tryColor;
	}
	
	void tryColor (object sender, ClickedEventArgs e) {
		if (!gameObject.activeSelf)
			return;

		RaycastHit hit;
		Ray ray = new Ray(transform.position, transform.forward);

		if (Physics.Raycast (ray, out hit, 100f)) {
			GameObject hitObj = hit.transform.gameObject;

			if (hitObj.tag == "ColorButton")
				SelectColor (hitObj);

			if (hitObj.tag == "Node")
				ColorNode (hitObj);
		}
	}

	void SelectColor (GameObject button) {
		button.GetComponentInParent<AudioSource>().Play();
		selectedColor = button.GetComponent<Renderer> ().material.color;
		colorLaser.color = selectedColor;
	}

	void ColorNode(GameObject node) {
		NodeConnections nodeConnections = node.GetComponent<NodeConnections> ();

		for (int i = 0; i < nodeConnections.adjacentNodes.Count; i++) {
			GameObject adjNode = nodeConnections.adjacentNodes [i];
			Material adjNodeMaterial = adjNode.GetComponent<Renderer> ().material;

			if (adjNodeMaterial.GetColor ("_Color") == selectedColor && selectedColor != defaultColor) {
				GameObject badEdge = null;

				for (int j = 0; j < nodeConnections.connectedEdges.Count; j++) {
					GameObject edge = nodeConnections.connectedEdges [j];

					if (edge.GetComponent<EdgeController> ().start == adjNode || edge.GetComponent<EdgeController> ().end == adjNode) {
						badEdge = edge;
						break;
					}
				}

				if (!validColoringChecker.badEdges.Contains (badEdge))
					validColoringChecker.badEdges.Add (badEdge);
			} else {
				GameObject goodEdge = null;

				for (int j = 0; j < nodeConnections.connectedEdges.Count; j++) {
					GameObject edge = nodeConnections.connectedEdges [j];

					if (edge.GetComponent<EdgeController> ().start == adjNode || edge.GetComponent<EdgeController> ().end == adjNode) {
						goodEdge = edge;
						break;
					}
				}
				validColoringChecker.badEdges.Remove (goodEdge);
			}
		}
		colorSound.Play();
		Material nodeMaterial = node.GetComponent<Renderer> ().material;
		nodeMaterial.SetColor ("_Color", selectedColor);
	}
}
