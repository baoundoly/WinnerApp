using System.IO;
using System.Windows;
using System.Windows.Media;

namespace WinnerApp
{
    /// <summary>
    /// Interaction logic for WinnerWindow.xaml
    /// </summary>
    public partial class WinnerWindow : Window
    {

        private MediaPlayer mediaPlayer;

        public WinnerWindow(string winnerName)
        {
            InitializeComponent();
            lblWinnerName.Content = $"Congratulations, {winnerName}!";
            mediaPlayer = new MediaPlayer();
            mediaPlayer.MediaFailed += MediaPlayer_MediaFailed;
            mediaPlayer.MediaOpened += MediaPlayer_MediaOpened;
            mediaPlayer.MediaEnded += MediaPlayer_MediaEnded; // Handle MediaEnded event

        }
        private void MediaPlayer_MediaFailed(object sender, ExceptionEventArgs e)
        {
            MessageBox.Show($"Media Failed: {e.ErrorException.Message}");
        }

        private void MediaPlayer_MediaOpened(object sender, EventArgs e)
        {
            //MessageBox.Show("Media Opened Successfully");
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            // Play the congratulatory sound
            PlaySound("congratulations.mp3");
            //PlaySound("sounds/sound.mp3");
        }
        private void MediaPlayer_MediaEnded(object sender, EventArgs e)
        {
            // Restart the media playback
            mediaPlayer.Position = TimeSpan.Zero;
            mediaPlayer.Play();
        }
        private void PlaySound(string soundFile)
        {
            try
            {
                var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, soundFile);
                var uri = new Uri(path, UriKind.Absolute);
                Console.WriteLine($"URI: {uri}"); // Log the URI
                mediaPlayer.Open(uri);
                mediaPlayer.Play();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error playing sound: {ex.Message}");
            }
        }
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            mediaPlayer.Stop();
            this.Close();
        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            //e.Cancel = true; // Cancel the close event
            //var fadeOutStoryboard = (Storyboard)this.Resources["FadeOutStoryboard"];
            //fadeOutStoryboard.Completed += (s, a) => this.CloseWindow();
            //fadeOutStoryboard.Begin(this);
        }

        private void CloseWindow()
        {
            mediaPlayer.Stop();
            //  this.Closing -= Window_Closing; // Remove the event handler to avoid recursion
            this.Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // This method is required to handle the Closing event in XAML
        }
    }
}
