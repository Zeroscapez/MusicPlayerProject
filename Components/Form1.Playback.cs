using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace MusicPlayer
{
    public partial class Form1
    {
        private bool isHandlingStop = false;
        #region Playback Controls
        private void PlayTrackByPlayOrder(int orderPosition)
        {
            if (orderPosition < 0 || orderPosition >= playOrder.Count)
                return;

            StopPlayback();

            playOrderPosition = orderPosition;
            currentIndex = playOrder[orderPosition];

            var track = playlist[currentIndex];
            audioFile = new AudioFileReader(track.FilePath);

            outputDevice.Init(audioFile);
            outputDevice.Play();

            if (audioFile != null)
            {
                audioFile.Volume = music_volume.Value / 100f;
            }

            LoadAlbumArt(track.FilePath);
            track_list.SelectedIndex = playOrderPosition;
        }

        private void StopPlayback()
        {
            Console.WriteLine("Playback Stopped");
            if (outputDevice != null)
            {
                //outputDevice.PlaybackStopped -= OutputDevice_PlaybackStopped;
                outputDevice.Stop();
            }

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
            if(audioFile == null && playOrder.Count > 0)
            {
                PlayTrackByPlayOrder(playOrderPosition >= 0 ? playOrderPosition : 0);
            }
            else if( audioFile != null)
            {
                outputDevice?.Play();
            }
            
        }

      

        private void OutputDevice_PlaybackStopped(object sender, StoppedEventArgs e)
        {
            if (e.Exception != null) return;
            if (isHandlingStop) return;

            isHandlingStop = true;

            if (InvokeRequired)
            {
                BeginInvoke(new Action(() =>
                {
                    isHandlingStop = false;
                    OutputDevice_PlaybackStopped(sender, e);
                }));
                return;
            }

            // Only auto-advance if the track actually finished
            if (audioFile != null && audioFile.Position >= audioFile.Length - 500)
            {
                if (playOrderPosition + 1 < playOrder.Count)
                {
                    PlayTrackByPlayOrder(playOrderPosition + 1);
                }
                else
                {
                    StopPlayback();
                    playOrderPosition = -1;
                }
            }

            isHandlingStop = false;
        }
        #endregion







    }
}
