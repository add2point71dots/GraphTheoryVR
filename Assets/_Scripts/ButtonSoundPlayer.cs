using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSoundPlayer : MonoBehaviour {
	public MenuPointer menuPointer;
	private AudioSource audioSource;

	void Start () {
		menuPointer.PlayButtonSound += MenuPointer_PlayButtonSound;
		audioSource = GetComponent<AudioSource> ();
	}

	void MenuPointer_PlayButtonSound (AudioClip buttonSound) {
		audioSource.PlayOneShot (buttonSound);
	}
}
