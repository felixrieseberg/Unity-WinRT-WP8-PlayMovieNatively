WinRT-WP8-PlayMovieNatively
===========================

This plugin allows the native playback of movie files on both Windows Phone 8 as well as WinRT.

##Usage

### WinRT
```
public static void PlayVideoFullscreen(string videoUrl, bool controlsEnabled, bool tapSkipsVideo)
```
- videoUrl: The URL to the video as a string, including official qualifier (for instance ms-appx:/// for an asset inside the app package or http:// for a web resource).
- controlsEnabled (default: _true_): _true_ enables the native video controls (scrubber, play/pause, etc.), _false_ disables them. 
- tapSkipsVideo (default: _false_): _true_ automatically removes the video element if the user taps/clicks it, _false_ doesn't. If controlsEnabled is set to _true_ and tapSkipsVideo is set to _false_, a tap/click will pause the video.

### Windows Phone 8
```
public static void PlayVideoFullscreen(string videoUrl, bool tapSkipsVideo)
```
- videoUrl: The URL to the video as a string, including official qualifier.
- tapSkipsVideo (default: _false_): _true_ automatically removes the video element if the user taps/clicks it, _false_ doesn't.

## Installation
Copy the contents of the folder _CopyContentsToUnityProjectPlugins_ into the _Plugins_ folder in your Unity project. If it doesn't exist yet, create it.

## Windows Phone 8.1
While Windows Phone 8.1 is based on WinRT, Unity does currently not fully support Windows Phone 8.1 (as of May 1st 2014).
In theory, the WinRT plugin should work on both Windows Phone 8.1 as well as Xbox One. If you're reading this, Unity has support for WP8.1 and I haven't updated this repo: All source code is in this repo, so changing the #ifdefs as well as putting the plugin into the right subfolder inside _plugins_ should do the trick.

## License
While I work at Microsoft, this plugin and all contents are provided as-is without any warranties. For details, please see the attached license (MS-LPL) or http://clrinterop.codeplex.com/license.
