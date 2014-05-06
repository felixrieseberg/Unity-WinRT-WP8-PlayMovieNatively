using UnityEngine;
using System.Collections;

public class PlayMovie : MonoBehaviour {

	private WinControlsWP8.VideoElement myNativeVideo;
	private bool videoStarted;

	// Use this for initialization
	void Start () {
		// Creating video element
		#if UNITY_WP8
		myNativeVideo = new WinControlsWP8.VideoElement("http://video-js.zencoder.com/oceans-clip.mp4", true, false);
		#endif
	}


	void OnGUI() {
		if (GUI.Button(new Rect(Screen.width/2 - 100, Screen.height/2 + 210, 200, 200), "Play Simple")) {
			PlaySimple();
		}
		if (GUI.Button(new Rect(Screen.width/2 - 100, Screen.height/2 - 210, 200, 200), "Play")) {
			PlaySimple();
		}
	}

	void PlaySimple() {
		Debug.Log("Trying to play movie");

		// NETFX_CORE is WINRT
		#if NETFX_CORE
		WinControls.VideoPlayback.PlayVideoFullscreen("ms-appx:///Assets/videoplayback.mp4", true, true);
		#endif
		#if UNITY_WP8
		WinControlsWP8.VideoPlayback.PlayVideoFullscreen("http://video-js.zencoder.com/oceans-clip.mp4", true);
		#endif
	}

	void Play() {
		Debug.Log("Trying to play movie");

		// The ifdefs here are optional, since both methods fail silently.
		#if UNITY_WP8
		myNativeVideo.Play();
		#endif
	}

	void Update() {
		Debug.Log("Elapsed time: " + myNativeVideo.elapsedTime.ToString());
	}
	
}
