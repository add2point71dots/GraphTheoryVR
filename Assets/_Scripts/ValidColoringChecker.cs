using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ValidColoringChecker : MonoBehaviour {
	public List<GameObject> badEdges;
	
	// Update is called once per frame
	void Update () {
		if (badEdges.Count > 0) {
			Debug.Log("INVALID COLORING");
		}
	}
}
