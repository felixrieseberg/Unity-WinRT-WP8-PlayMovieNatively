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
        public static void PlayVideoFullscreen(string videoUrl)
        {
            PlayVideoFullscreen(videoUrl, false);
        }
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
        public static void PlayVideoFullscreenOnUIThread(string videoUrl, bool tapSkipsVideo)
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
}
;