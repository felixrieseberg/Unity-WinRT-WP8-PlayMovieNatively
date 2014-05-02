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

		}
	}

	void OnGUI() {
		if (GUI.Button(new Rect(Screen.width/2 - 100, Screen.height/2 - 100, 200, 200), "Play Movie")) {
			Play();
		}
	}

	void Play() {
		Debug.Log("Trying to play movie");

		// The ifdefs here are optional, since both methods fail silently.
		#if NETFX_CORE
		WinControls.VideoPlayback.PlayVideoFullscreen("ms-appx:///Assets/videoplayback.mp4", true, true);
		#endif
		#if UNITY_WP8
		WinControlsWP8.VideoPlayback.PlayVideoFullscreen("http://video-js.zencoder.com/oceans-clip.mp4", true);
		#endif
	}
	
}
