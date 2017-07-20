using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Colorer : MonoBehaviour {
  private SteamVR_TrackedController device;

	void Start () {
    device = gameObject.GetComponentInParent<SteamVR_TrackedController>();
    device.TriggerClicked += tryColor;
	}
	
  void tryColor (object sender, ClickedEventArgs e) {
    if (!gameObject.activeSelf)
      return;
	}
}
