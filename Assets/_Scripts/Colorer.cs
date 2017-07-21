using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Colorer : MonoBehaviour {
  private SteamVR_TrackedController device;
  public Color selectedColor;
  public SteamVR_LaserPointer colorLaser;

	void Start () {
    device = gameObject.GetComponentInParent<SteamVR_TrackedController>();
    device.TriggerClicked += tryColor;
    colorLaser = gameObject.GetComponent<SteamVR_LaserPointer> ();
	}
	
  void tryColor (object sender, ClickedEventArgs e) {
    if (!gameObject.activeSelf)
      return;

    RaycastHit hit;
    Ray ray = new Ray(transform.position, transform.forward);

    if (Physics.Raycast (ray, out hit, 100f)) {
      GameObject hitObj = hit.transform.gameObject;

      if (hitObj.tag == "ColorButton") {
        Debug.Log ("HIT A COLOR BUTTON");
        selectedColor = hitObj.GetComponent<Renderer> ().material.color;
        Debug.Log ("ITS COLOR IS " + selectedColor);
        colorLaser.color = selectedColor;
      }
    }
	}
}
