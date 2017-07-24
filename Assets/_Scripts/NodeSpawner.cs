using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeSpawner : MonoBehaviour {
	private SteamVR_TrackedController device;
	public GameObject node;
	public Transform nodeSpawn;

	void Start() {
		device = gameObject.GetComponentInParent<SteamVR_TrackedController>();
		device.TriggerClicked += MakeNode;
	}

	void MakeNode(object sender, ClickedEventArgs e) {
		if (!gameObject.activeSelf)
			return;
		
		Vector3 newNodePosition = nodeSpawn.position;

		Collider[] nearbyNodes = Physics.OverlapSphere (newNodePosition, 0.1f);

		if (nearbyNodes.Length == 0) {
			GameObject newNode = Instantiate (node, newNodePosition, nodeSpawn.rotation);
			newNode.GetComponent<AudioSource>().Play();
		}
	}
}
