using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#if NETFX_CORE
using System.Threading.Tasks;
using Windows.UI.Core;
using Windows.UI.Popups;
using Windows.ApplicationModel.Core;
using Windows.ApplicationModel.Store;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml;
#endif

namespace WinControls
{
    public class MessageBox
    {
        public delegate void ActionDelegate(object UICommand);

        public class Command
        {
            public string text;
            public ActionDelegate action;

            public Command(string text, ActionDelegate action)
            {
                this.text = text;
                this.action = action;
            }
        }

        public void ShowMessageBox(string message)
        {
            ShowMessageBox(message, null, null, null);
        }

        public void ShowMessageBox(string message, string title, Command command1)
        {
            ShowMessageBox(message, title, command1, null);
        }

        public void ShowMessageBox(string message, string title, Command command1, Command command2)
        {
        #if NETFX_CORE
            CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                MessageDialog messageBox = new MessageDialog(message, title);

                if (command1 != null)
                {
                    messageBox.Commands.Add(new UICommand(command1.text, new UICommandInvokedHandler(command1.action)));
                }

                if (command2 != null)
                {
                    messageBox.Commands.Add(new UICommand(command2.text, new UICommandInvokedHandler(command2.action)));
                }

                messageBox.ShowAsync();
            });
        #endif
        }
    }

    public class VideoPlayback {

        public static void PlayVideoFullscreen(string videoUrl)
        {
            PlayVideoFullscreen(videoUrl, true, false);
        }
        public static void PlayVideoFullscreen(string videoUrl, bool controlsEnabled)
        {
            PlayVideoFullscreen(videoUrl, controlsEnabled, false);
        }
        public static void PlayVideoFullscreen(string videoUrl, bool controlsEnabled, bool tapSkipsVideo)
        {
#if NETFX_CORE
            CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {

                Page page = (Page)Window.Current.Content;
                SwapChainBackgroundPanel backgroundPanel = (SwapChainBackgroundPanel)page.FindName("DXSwapChainBackgroundPanel");

                MediaElement videoPlayBackElement = new MediaElement();

                videoPlayBackElement.IsFullWindow = true;
                videoPlayBackElement.Source = new Uri(videoUrl);
                videoPlayBackElement.AreTransportControlsEnabled = controlsEnabled;

                if (tapSkipsVideo)
                {
                    videoPlayBackElement.Tapped += delegate { backgroundPanel.Children.Remove(videoPlayBackElement); };
                }
                videoPlayBackElement.MediaEnded += delegate { backgroundPanel.Children.Remove(videoPlayBackElement); };

                backgroundPanel.Children.Add(videoPlayBackElement);
            
                videoPlayBackElement.Play();
            });
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
        /// Get's the media's duration in seconds (whole and fractions)
        /// </summary>
        public double mediaDuration
        { get; set; }

#if NETFX_CORE
        private MediaElement _mediaElement
        { get; set; }
        private SwapChainBackgroundPanel _backgroundPanel
        { get; set; }
#endif

        public VideoElement(string videoUrl, bool controlsEnabled, bool tapSkipsVideo, bool autoPlay)
        {
#if NETFX_CORE
            CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {

                Page page = (Page)Window.Current.Content;
                _backgroundPanel = (SwapChainBackgroundPanel)page.FindName("DXSwapChainBackgroundPanel");

                _mediaElement = new MediaElement();

                _mediaElement.IsFullWindow = true;
                _mediaElement.Source = new Uri(videoUrl);
                _mediaElement.AreTransportControlsEnabled = controlsEnabled;
                _mediaElement.MediaEnded += delegate { _backgroundPanel.Children.Remove(_mediaElement); playbackFinished = true; isPlaying = false; };
                _mediaElement.MediaOpened += delegate { isPlaying = true; mediaDuration = _mediaElement.NaturalDuration.TimeSpan.TotalSeconds; };

                if (tapSkipsVideo)
                {
                    _mediaElement.Tapped += delegate { _backgroundPanel.Children.Remove(_mediaElement); playbackFinished = true; isPlaying = false; };
                }

                if (autoPlay)
                {
                    this.Play();
                }
            });
#endif
        }

        public void Play()
        {
#if NETFX_CORE
            CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                _backgroundPanel.Children.Add(_mediaElement);
                _mediaElement.Play();
            });
            isPlaying = true;
#endif
        }

        public void Stop()
        {
#if NETFX_CORE
            CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                _mediaElement.Stop();
            });
            isPlaying = false;
#endif
        }

        public void Pause()
        {
#if NETFX_CORE
            CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                _mediaElement.Pause();
            });
            isPlaying = false;
#endif
        }
    }

}
