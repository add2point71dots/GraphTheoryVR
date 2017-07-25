using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Colorer : MonoBehaviour {
	private SteamVR_TrackedController device;
	public Color selectedColor;
	public Color defaultColor;
	public SteamVR_LaserPointer colorLaser;
	public GameObject graph;
	public ValidColoringChecker validColoringChecker;
	private AudioSource colorSound;

	void Start () {
		device = gameObject.GetComponentInParent<SteamVR_TrackedController>();
		device.TriggerClicked += tryColor;
		colorLaser = gameObject.GetComponent<SteamVR_LaserPointer> ();
		selectedColor = defaultColor;
		validColoringChecker = graph.GetComponent<ValidColoringChecker>();
		colorSound = GetComponent<AudioSource>();
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
