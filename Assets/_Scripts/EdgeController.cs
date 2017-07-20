using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EdgeController : MonoBehaviour {
	public GameObject start;
	public GameObject end;
	private LineRenderer line;
	private CapsuleCollider collider;

	// Use this for initialization
	void Start () {
		line = GetComponent<LineRenderer>();
		collider = gameObject.AddComponent <CapsuleCollider> ();
		collider.radius = 0.03f;
		collider.isTrigger = true;
		collider.direction = 2;
	}

	// Update is called once per frame
	void Update () {
		line.SetPosition(0, start.transform.position);
		line.SetPosition(1, end.transform.position);

		collider.transform.position = (start.transform.position + end.transform.position) / 2;
		collider.center = Vector3.zero;
		collider.height = (end.transform.position - start.transform.position).magnitude;
		collider.transform.LookAt (start.transform.position);
	}
}
