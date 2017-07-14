using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EdgeController : MonoBehaviour {
	public Transform start;
	public Transform end;
	private LineRenderer line;
	private CapsuleCollider collider;

	// Use this for initialization
	void Start () {
		line = GetComponent<LineRenderer>();
		collider = gameObject.AddComponent <CapsuleCollider> ();
		collider.radius = 0.005f;
		collider.isTrigger = true;
		collider.direction = 2;
	}
	
	// Update is called once per frame
	void Update () {
		line.SetPosition(0, start.position);
		line.SetPosition(1, end.position);

		collider.transform.position = (start.position + end.position) / 2;
		collider.center = Vector3.zero;
		collider.height = (end.position - start.position).magnitude;
		collider.transform.LookAt (start.position);
	}
}
