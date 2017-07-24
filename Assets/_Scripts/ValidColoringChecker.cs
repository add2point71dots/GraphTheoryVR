using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ValidColoringChecker : MonoBehaviour {
	public List<GameObject> badEdges;
  public GameObject invalidColoringText;
	
	// Update is called once per frame
	void Update () {
    invalidColoringText.SetActive (badEdges.Count > 0);
	}
}
