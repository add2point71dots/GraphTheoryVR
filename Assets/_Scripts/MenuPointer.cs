using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPointer : MonoBehaviour {
	public GameObject leftController;
  public AudioClip graphSound;
  public AudioClip destroySound;
  public AudioClip colorSound;
  public event System.Action<AudioClip> PlayButtonSound = delegate {};

	private SteamVR_TrackedController device;
	private SteamVR_TrackedController leftDevice;
	private GameObject graphController;
	private GameObject destroyController;
	private GameObject colorController;
	private TextMesh controllerLabelText;
	private GameObject menu;
	private GameObject colorPalette;

	// Use this for initialization
	void Start () {
		graphController = transform.parent.Find ("GraphController").gameObject;
		destroyController = transform.parent.Find ("DestroyController").gameObject;
		colorController = transform.parent.Find ("ColorController").gameObject;

		menu = leftController.transform.Find ("Menu").gameObject;
		colorPalette = leftController.transform.Find ("ColorPalette").gameObject;
		controllerLabelText = transform.parent.Find ("RightControllerLabel").gameObject.GetComponent<TextMesh>();

		device = gameObject.GetComponentInParent<SteamVR_TrackedController>();
		device.TriggerClicked += selectMode;
	}

	void selectMode(object sender, ClickedEventArgs e) {
		if (!gameObject.activeSelf)
			return;

		RaycastHit hit;
		Ray ray = new Ray(transform.position, transform.forward);

		if (Physics.Raycast (ray, out hit, 100f)) {
			if (hit.transform.gameObject.tag == "Button") {

				if (hit.transform.name == "GraphButton") {
					graphController.SetActive (true);
					controllerLabelText.text = "Graph Maker";
          PlayButtonSound (graphSound);
        //  audioSource.PlayOneShot (graphSound);
       //   Debug.Log ("GRAPH SOUND " + graphSound.name);
				} else if (hit.transform.name == "DestroyButton") {
					destroyController.SetActive (true);
					controllerLabelText.text = "Destroyer";
          PlayButtonSound (destroySound);
				} else if (hit.transform.name == "ColorButton") {
					colorController.SetActive (true);
					controllerLabelText.text = "Colorer";
					colorPalette.SetActive (true);
          PlayButtonSound (colorSound);
				}


        AudioSource sound = hit.transform.GetComponent<AudioSource> ();

        sound.Play ();
        Debug.Log ("SOUND PLAYING? " + sound.isPlaying);
       
        Debug.Log (sound.clip);
        menu.SetActive (false);
        gameObject.SetActive (false);
			}
		}
	}
}
