using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorGenerator : MonoBehaviour {
  public List<Color> colors;

	void Start () {
    colors.Add (Color.red);
    colors.Add (Color.blue);
    colors.Add (Color.green);
    colors.Add (Color.magenta);
    colors.Add (Color.yellow);
    colors.Add (Color.cyan);
	}
	
	// Update is called once per frame
	void Update () {
    int numColors = colors.Count;
    int numNodes = GameObject.FindGameObjectsWithTag ("Node").Length;

    if (numColors < numNodes) {
      for (int i = 0; i < (numNodes - numColors); i++) {
        colors.Add (Random.ColorHSV ());
      }
    }
	}
}
