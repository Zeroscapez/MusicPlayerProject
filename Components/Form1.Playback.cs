using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace MusicPlayer
{
    public partial class Form1
    {

        private bool isIntentionalStop = false;
        #region Playback Controls
        private void PlayTrackByPlayOrder(int orderPosition)
        {
            if (orderPosition < 0 || orderPosition >= playOrder.Count)
                return;

            // Dispose old audio file without calling outputDevice.Stop()
            // This avoids firing PlaybackStopped entirely
            isIntentionalStop = true;
            outputDevice.Stop();

            if (audioFile != null)
            {
                audioFile.Dispose();
                audioFile = null;
            }

            if (music_art.Image != null)
            {
                music_art.Image.Dispose();
                music_art.Image = null;
            }

            progressBar.Value = 0;

            playOrderPosition = orderPosition;
            currentIndex = playOrder[orderPosition];

            var track = playlist[currentIndex];
            audioFile = new AudioFileReader(track.FilePath);
            audioFile.Volume = music_volume.Value / 100f;


            outputDevice.Init(audioFile);
            outputDevice.Play();

            LoadAlbumArt(track.FilePath);

            isSyncingSelection = true;
            track_list.SelectedIndex = playOrderPosition;
            isSyncingSelection = false;
        }

        private void StopPlayback()
        {
            isIntentionalStop = true;
            outputDevice?.Stop();

            if (audioFile != null)
            {
                audioFile.Dispose();
                audioFile = null;
            }

            progressBar.Value = 0;

            if (music_art.Image != null)
            {
                music_art.Image.Dispose();
                music_art.Image = null;
            }
        }

        private void PausePlayback()
        {
            outputDevice?.Pause();
        }

        private void ResumePlayback()
        {
            if (audioFile == null && playOrder.Count > 0)
            {
                PlayTrackByPlayOrder(playOrderPosition >= 0 ? playOrderPosition : 0);
            }
            else if (audioFile != null)
            {
                outputDevice?.Play();
            }

        }



        private void OutputDevice_PlaybackStopped(object sender, StoppedEventArgs e)
        {
            if (e.Exception != null) return;

            if (InvokeRequired)
            {
                BeginInvoke(new Action(() => OutputDevice_PlaybackStopped(sender, e)));
                return;
            }

            if (isIntentionalStop)
            {
                isIntentionalStop = false;
                return;
            }

            // Track naturally finished, auto advance
            if (playOrderPosition + 1 < playOrder.Count)
            {
                PlayTrackByPlayOrder(playOrderPosition + 1);
            }
            else
            {
                isIntentionalStop = true; // prevent StopPlayback from triggering auto advance
                StopPlayback();
                isIntentionalStop = false;
                playOrderPosition = -1;
            }
        }
        #endregion







    }
}
