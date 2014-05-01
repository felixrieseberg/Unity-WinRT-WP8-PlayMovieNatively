using UnityEngine;
using System.Collections;

public class PlayMovie : MonoBehaviour {
	
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown("Fire2")) {
			Play();
			Debug.Log("Trying to play movie");
		}
	}

	void Play() {
		WinControls.VideoPlayback.PlayVideoFullscreen("ms-appx:///Assets/videoplayback.mp4", true, true);
	}
	
}
