using System;
using System.Collections.Generic;
using System.Text;

namespace MusicPlayer
{
    public partial class Form1
    {

        private void open_Button_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog
            {
                Multiselect = true,
                Filter = "Audio Files|*.mp3;*.wav;*.flac;*.aac"
            };

            if (dialog.ShowDialog() != DialogResult.OK) return;


            foreach (var file in dialog.FileNames)
            {
                var track = new Track(file);
                playlist.Add(track);
                track_list.Items.Add(track.DisplayName);
            }
            BuildPlayerOrder();
            UpdatePlaylistDisplay();
            track_list.Invalidate();

        }







        #region Buttons
        private void play_Button_Click(object sender, EventArgs e) => ResumePlayback();
        private void stop_Button_Click(object sender, EventArgs e) => StopPlayback();
        private void pause_Button_Click(object sender, EventArgs e) => PausePlayback();

        private void next_button_Click(object sender, EventArgs e)
        {
            if (playOrder.Count == 0) return;
            int nextIndex = (playOrderPosition + 1) % playOrder.Count;
            PlayTrackByPlayOrder(nextIndex);
        }

        private void previous_Button_Click(object sender, EventArgs e)
        {
            if (playOrder.Count == 0) return;
            int prevIndex = (playOrderPosition - 1 + playOrder.Count) % playOrder.Count;
            PlayTrackByPlayOrder(prevIndex);
        }

        private void shuffle_button_Click(object sender, EventArgs e)
        {
            isShuffleEnabled = !isShuffleEnabled;

            shuffle_button.BackColor = isShuffleEnabled
                ? Color.LightGreen
                : Color.Black;

            shuffle_button.Text = isShuffleEnabled ? "Shuffle: ON" : "Shuffle: OFF";

            BuildPlayerOrder();
            UpdatePlaylistDisplay();


            playOrderPosition = -1;
            currentIndex = -1;
        }

        #endregion

        #region Volume Control

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            audioFile?.Volume = music_volume.Value / 100f;

            volume_percent.Text = music_volume.Value.ToString() + "%";
        }
        #endregion

        #region Playlist Selection



        private void track_list_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0) return;

            e.DrawBackground();

            string text = track_list.Items[e.Index].ToString() ?? string.Empty;
            var rect = e.Bounds;
            var flags = TextFormatFlags.VerticalCenter | TextFormatFlags.Left | TextFormatFlags.EndEllipsis;

            Color foreColor = ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
                ? SystemColors.HighlightText
                : track_list.ForeColor;

            TextRenderer.DrawText(e.Graphics, text, track_list.Font, rect, foreColor, flags);
            e.DrawFocusRectangle();
        }

        #endregion
        private void folder_button_Click(object sender, EventArgs e)
        {
            using var dialog = new FolderBrowserDialog();

            if (dialog.ShowDialog() != DialogResult.OK)
                return;

            musicFolderPath = dialog.SelectedPath;

            LoadMusicFromFolder(musicFolderPath);

            SaveFolderPath();
        }


        private void label_trackEnd_Click(object sender, EventArgs e)
        {

        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {

            outputDevice.Stop();
            outputDevice.Dispose();
            base.OnFormClosing(e);

        }
    }
}
