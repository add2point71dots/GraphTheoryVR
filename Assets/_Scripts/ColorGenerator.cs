using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorGenerator : MonoBehaviour {
  public List<Color> colors;
  public GameObject colorButton;
  public List<GameObject> colorButtons;

	void Start () {
    colors.Add (Color.red);
    colors.Add (Color.blue);
    colors.Add (Color.green);
    colors.Add (Color.magenta);
    colors.Add (Color.yellow);
    colors.Add (Color.cyan);

    GameObject button = Instantiate (colorButton, transform.position, transform.rotation, transform);
    colorButtons.Add (button);
    generateButtons ();
	}
	
	void Update () {
    int numColors = colors.Count;
    int numNodes = GameObject.FindGameObjectsWithTag ("Node").Length;

    if (numColors < numNodes) {
      for (int i = 0; i < (numNodes - numColors); i++) {
        colors.Add (Random.ColorHSV ());
      }
    }
	}

  void generateButtons() {
 //   float offset = 0f;
    for (int i = 0; i < colors.Count; i++) {
   //   Debug.Log ("TRANSFORM POS IS " + transform.position);

      Vector3 tempPos = colorButtons [colorButtons.Count - 1].transform.position;
      tempPos.x += 0.1f;

      GameObject button = Instantiate (colorButton, tempPos, transform.rotation, transform);
      colorButtons.Add (button);
      Material material = button.GetComponent<Renderer> ().material;
      material.SetColor ("_Color", colors [i]);
  //    Vector3 temp = button.transform.position; // copy to an auxiliary variable...
  //    temp.x += offset;
  //    button.transform.position = temp;
  //    offset = offset + 0.1f;
    }
  }
}
