using System;
using System.Net;

#if WINDOWS_PHONE
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using System.Windows.Threading;
#endif

namespace WinControlsWP8
{
    public class VideoPlayback
    {
        /// <summary>
        /// Creates a native Windows Phone MediaElement and immediatly starts playback.
        /// </summary>
        /// <param name="videoUrl">URL to the vidoe (from Windows Phone's point of view)</param>
        public static void PlayVideoFullscreen(string videoUrl)
        {
            PlayVideoFullscreen(videoUrl, false);
        }

        /// <summary>
        /// Creates a native Windows Phone MediaElement and immediatly starts playback.
        /// </summary>
        /// <param name="videoUrl">URL to the vidoe (from Windows Phone's point of view)</param>
        /// <param name="tapSkipsVideo">If true, a tap will stop & remove the Video playback</param>
        public static void PlayVideoFullscreen(string videoUrl, bool tapSkipsVideo)
        {
#if WINDOWS_PHONE
            Deployment.Current.Dispatcher.BeginInvoke(
                (Action)(() =>
                {
                    PlayVideoFullscreenOnUIThread(videoUrl, tapSkipsVideo);
                }));
#endif
        }

        /// <summary>
        /// Creates a native Windows Phone MediaElement and immediatly starts playback.
        /// </summary>
        /// <param name="videoUrl">URL to the vidoe (from Windows Phone's point of view)</param>
        /// <param name="tapSkipsVideo">If true, a tap will stop & remove the Video playback</param>
        private static void PlayVideoFullscreenOnUIThread(string videoUrl, bool tapSkipsVideo)
        {
#if WINDOWS_PHONE
            var frame = Application.Current.RootVisual as PhoneApplicationFrame;
            var currentPage = frame.Content as PhoneApplicationPage;
            DrawingSurfaceBackgroundGrid drawingSurfaceBackgroundElement = (DrawingSurfaceBackgroundGrid)currentPage.FindName("DrawingSurfaceBackground");

            MediaElement videoPlayBackElement = new MediaElement();

            videoPlayBackElement.Height = Double.NaN;
            videoPlayBackElement.Width = Double.NaN;
            videoPlayBackElement.VerticalAlignment = System.Windows.VerticalAlignment.Stretch;
            videoPlayBackElement.HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch;
            videoPlayBackElement.Stretch = System.Windows.Media.Stretch.UniformToFill;
            videoPlayBackElement.Source = new Uri(videoUrl, UriKind.RelativeOrAbsolute);
            if (tapSkipsVideo)
            {
                videoPlayBackElement.Tap += delegate { drawingSurfaceBackgroundElement.Children.Remove(videoPlayBackElement); };
            }

            drawingSurfaceBackgroundElement.Children.Add(videoPlayBackElement);
            videoPlayBackElement.Play();
#endif
        }

    }

    public class VideoElement
    {
        /// <summary>
        /// Returns true if the playback has finished.
        /// </summary>
        public bool playbackFinished
        { get; set; }

        /// <summary>
        /// Returns true if the media is playing. This property isn't guaranteed and will be incorrect if the video is started/stopped from outside the plugin.
        /// </summary>
        public bool isPlaying
        { get; set; }
        
        /// <summary>
        /// Returns the progress of playback as a percentage.
        /// </summary>
        public int percentageDone
        {
            get {
#if WINDOWS_PHONE
                int percentage = (int)Math.Round((float)_mediaElement.Position.Seconds / (float)_mediaElement.NaturalDuration.TimeSpan.Seconds * 100);
                return percentage;
#else
                return 0;
#endif
            }
        }

        /// <summary>
        /// Returns the progress of playback from 0 to 1.
        /// </summary>
        public double progress
        {
            get
            {
#if WINDOWS_PHONE
                double progress = _mediaElement.Position.Seconds / _mediaElement.NaturalDuration.TimeSpan.Seconds;
                return progress;
#else
                return 0;
#endif
            }
        }

        /// <summary>
        /// Returns the elapsed playback time in milliseconds.
        /// </summary>
        public double elapsedTime
        { 
            get { 
#if WINDOWS_PHONE
                return (double)_mediaElement.Position.Milliseconds;
#else  
                return 0;
#endif
            } 
        }

        /// <summary>
        /// Returns the elapsed playback time in seconds.
        /// </summary>
        public double elapsedTimeInSeconds
        {
            get {
#if WINDOWS_PHONE
                return (double)_mediaElement.Position.Milliseconds * 1000;
#else  
                return 0;
#endif            
            }
        }
        
#if WINDOWS_PHONE
        /// <summary>
        /// Private: The XAML MediaElement. 
        /// </summary>
        private MediaElement _mediaElement
        { get; set; }
        private DrawingSurfaceBackgroundGrid _drawingSurfaceBackgroundElement
        { get; set; }
#endif

        /// <summary>
        /// Creates a new VideoElement
        /// </summary>
        /// <param name="videoUrl">URL to the vidoe (from Windows Phone's point of view)</param>
        /// <param name="tapSkipsVideo">If true, a tap will stop & remove the Video playback</param>
        /// <param name="autoPlay">If true, the video will immediatly start playback</param>
        public VideoElement(string videoUrl, bool tapSkipsVideo, bool autoPlay)
        {
#if WINDOWS_PHONE
            Deployment.Current.Dispatcher.BeginInvoke(
            (Action)(() =>
            {
                var frame = Application.Current.RootVisual as PhoneApplicationFrame;
                var currentPage = frame.Content as PhoneApplicationPage;
                _drawingSurfaceBackgroundElement = (DrawingSurfaceBackgroundGrid)currentPage.FindName("DrawingSurfaceBackground");

                _mediaElement = new MediaElement();

                _mediaElement.Height = Double.NaN;
                _mediaElement.Width = Double.NaN;
                _mediaElement.VerticalAlignment = System.Windows.VerticalAlignment.Stretch;
                _mediaElement.HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch;
                _mediaElement.Stretch = System.Windows.Media.Stretch.UniformToFill;
                _mediaElement.Source = new Uri(videoUrl, UriKind.RelativeOrAbsolute);
                if (tapSkipsVideo)
                {
                    _mediaElement.MediaEnded += delegate { playbackFinished = true; };
                    _mediaElement.Tap += delegate { _drawingSurfaceBackgroundElement.Children.Remove(_mediaElement); };
                }
                if (autoPlay)
                {
                    _drawingSurfaceBackgroundElement.Children.Add(_mediaElement);
                    _mediaElement.Play();
                    isPlaying = true;
                }
            }));
#endif
        }

        /// <summary>
        /// Starts playback
        /// </summary>
        public void Play() {
#if WINDOWS_PHONE
            if (_mediaElement != null && _drawingSurfaceBackgroundElement != null)
            {
                Deployment.Current.Dispatcher.BeginInvoke(
                (Action)(() =>
                {
                    _drawingSurfaceBackgroundElement.Children.Add(_mediaElement);
                    _mediaElement.Play();
                    isPlaying = true;
                }));
            }
#endif
        }

        /// <summary>
        /// Stops playback
        /// </summary>
        public void Stop()
        {
#if WINDOWS_PHONE
            if (_mediaElement != null && _drawingSurfaceBackgroundElement != null)
            {
                Deployment.Current.Dispatcher.BeginInvoke(
                (Action)(() =>
                {
                    _mediaElement.Stop();
                    isPlaying = false;
                }));
            }
#endif
        }

        /// <summary>
        /// Pauses playback
        /// </summary>
        public void Pause()
        {
#if WINDOWS_PHONE
            if (_mediaElement != null && _drawingSurfaceBackgroundElement != null)
            {
                Deployment.Current.Dispatcher.BeginInvoke(
                (Action)(() =>
                {
                    _mediaElement.Pause();
                    isPlaying = false;
                }));
            }
#endif
        }

        /// <summary>
        /// Sets the video's position to the given position.
        /// </summary>
        /// <param name="miliseconds">Position in milliseconds</param>
        public void ResetPosition(int milliseconds)
        {
#if WINDOWS_PHONE
            if (_mediaElement != null && _drawingSurfaceBackgroundElement != null)
            {
                Deployment.Current.Dispatcher.BeginInvoke(
                (Action)(() =>
                {
                    _mediaElement.Position = new TimeSpan(0,0,0,0,milliseconds);
                }));
            }
#endif
        }

        

    }
}
;