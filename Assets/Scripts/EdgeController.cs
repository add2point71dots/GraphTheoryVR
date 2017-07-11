using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EdgeController : MonoBehaviour {
	public Transform start;
	public Transform end;
	private LineRenderer line;

	// Use this for initialization
	void Start () {
		line = GetComponent<LineRenderer>();
		Debug.Log("IN DRAW EDGE");
		Debug.Log("start: " + start);
		Debug.Log("end: " + end);
	}
	
	// Update is called once per frame
	void Update () {
		line.SetPosition(0, start.position);
		line.SetPosition(1, end.position);
	}
}
