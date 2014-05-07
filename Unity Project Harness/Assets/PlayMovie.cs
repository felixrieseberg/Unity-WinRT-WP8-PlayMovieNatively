using UnityEngine;
using System.Collections;

public class PlayMovie : MonoBehaviour {

    #if UNITY_WP8
	private WinControlsWP8.VideoElement myNativeVideo;
    #elif UNITY_METRO
    private WinControls.VideoElement myNativeVideo;
    #endif
	private bool videoStarted;


	// Use this for initialization
	void Start () {
		// Creating video element
		#if UNITY_WP8
		myNativeVideo = new WinControlsWP8.VideoElement("http://video-js.zencoder.com/oceans-clip.mp4", true, false);
        InvokeRepeating("LogMovieInfo", 2, 2);
		#elif UNITY_METRO
        myNativeVideo = new WinControls.VideoElement("http://video-js.zencoder.com/oceans-clip.mp4", false, true, false);
        #endif
	}


	void OnGUI() {
		if (GUI.Button(new Rect(Screen.width/2 - 100, Screen.height/2 + 210, 200, 200), "Play Simple")) {
			PlaySimple();
		}
		if (GUI.Button(new Rect(Screen.width/2 - 100, Screen.height/2 - 210, 200, 200), "Play")) {
			Play();
		}
       GUI.Label(new Rect(Screen.width / 2 - 200, 0, 400, 200), myNativeVideo.mediaDuration.ToString() + " P: " + myNativeVideo.isPlaying.ToString() + " F: " + myNativeVideo.playbackFinished.ToString());
	}

	void PlaySimple() {
		Debug.Log("Trying to play movie");

		// NETFX_CORE is WINRT
		#if UNITY_METRO
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

	void LogMovieInfo() {
		Debug.Log("Duration: " + myNativeVideo.mediaDuration.ToString() + " Playing: " + myNativeVideo.isPlaying.ToString() + " Done: " + myNativeVideo.playbackFinished.ToString());
	}

}
