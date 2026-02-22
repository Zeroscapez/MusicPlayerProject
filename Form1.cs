using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using NAudio.Wave;
using TagLib;


namespace MusicPlayer
{
    public partial class Form1 : Form
    {
        private WaveOutEvent outputDevice;
        private AudioFileReader audioFile;

        // Playlist
        private List<string> paths = new();
        private List<string> files = new();
        private int currentIndex = -1;

        //Shuffling
        private bool isShuffleEnabled = false;
        private List<int> playOrder = new();
        private int playOrderPosition = -1;
        private readonly Random random = new();

        public Form1()
        {
            InitializeComponent();
            music_volume.Value = 50; // Set default volume to 50%
            volume_percent.Text = music_volume.Value.ToString() + "%";

            // owner-draw so long text doesn't wrap
            track_list.HorizontalScrollbar = true;
            track_list.DrawMode = DrawMode.OwnerDrawFixed;
            track_list.DrawItem += track_list_DrawItem;

            // Timer for progress bar
            timer1.Interval = 500;
            timer1.Tick += timer1_Tick;
            timer1.Start();
        }




        private void label1_Click(object sender, EventArgs e) { }

        private void groupBox1_Enter(object sender, EventArgs e) { }

        private void pictureBox1_Click(object sender, EventArgs e) { }

        private void musicPlayer_Enter(object sender, EventArgs e) { }

        private void label1_Click_1(object sender, EventArgs e) { }

        private void label1_Click_2(object sender, EventArgs e) { }

        private void label1_Click_3(object sender, EventArgs e) { }



        #region File Handling
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
                paths.Add(file);
                files.Add(Path.GetFileName(file));
                track_list.Items.Add(Path.GetFileNameWithoutExtension(file));
            }
            BuildPlayerOrder();
            UpdatePlaylistDisplay();
            track_list.Invalidate();

        }

        private void BuildPlayerOrder()
        {
            playOrder.Clear();
            for (int i = 0; i < paths.Count; i++)
            {
                playOrder.Add(i);
            }
            if (isShuffleEnabled)
            {
                // Fisher–Yates shuffle
                for (int i = playOrder.Count - 1; i > 0; i--)
                {
                    int j = random.Next(i + 1);
                    (playOrder[i], playOrder[j]) = (playOrder[j], playOrder[i]);
                }
            }
            playOrderPosition = -1;
            currentIndex = -1;

        }
        #endregion

        #region Playback Controls
        private void PlayTrackByPlayOrder(int orderPosition)
        {
            if (orderPosition < 0 || orderPosition >= playOrder.Count)
                return;

            StopPlayback();

            playOrderPosition = orderPosition;
            currentIndex = playOrder[orderPosition];

            audioFile = new AudioFileReader(paths[currentIndex]);
            audioFile.Volume = music_volume.Value / 100f;

            outputDevice = new WaveOutEvent();
            outputDevice.Init(audioFile);
            outputDevice.PlaybackStopped += OutputDevice_PlaybackStopped;
            outputDevice.Play();

            LoadAlbumArt(paths[currentIndex]);
            track_list.SelectedIndex = playOrderPosition;
        }

        private void StopPlayback()
        {
            if (outputDevice != null)
            {
                outputDevice.PlaybackStopped -= OutputDevice_PlaybackStopped;
                outputDevice.Stop();
                outputDevice.Dispose();
                outputDevice = null;
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
            outputDevice?.Play();
        }

        private void OutputDevice_PlaybackStopped(object sender, StoppedEventArgs e)
        {
            if (audioFile == null) return;

            // Check if song reached the end (allow a tiny tolerance)
            bool reachedEnd = audioFile.Position >= audioFile.Length - 500; // 500 bytes tolerance

            if (reachedEnd)
            {
                // If there is a next track, play it
                if (playOrderPosition + 1 < playOrder.Count)
                {
                    PlayTrackByPlayOrder(playOrderPosition + 1);
                }
                else
                {
                    // No more tracks, stop playback and reset index
                    playOrderPosition = -1;
                    StopPlayback();
                }
            }
        }
        #endregion


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
            if (audioFile != null)
            {
                audioFile.Volume = music_volume.Value / 100f;
            }

            volume_percent.Text = music_volume.Value.ToString() + "%";
        }
        #endregion

        #region Playlist Selection
        private void track_list_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (track_list.SelectedIndex < 0 || playOrder.Count == 0)
                return;

            // UI index is always the playOrder position
            int selectedPlayOrderPosition = track_list.SelectedIndex;

            PlayTrackByPlayOrder(selectedPlayOrderPosition);

        }

        private void UpdatePlaylistDisplay()
        {
            track_list.Items.Clear();

            for (int i = 0; i < playOrder.Count; i++)
            {
                int fileIndex = playOrder[i];
                string name = Path.GetFileNameWithoutExtension(files[fileIndex]);

                if (isShuffleEnabled)
                    track_list.Items.Add($"{name}");
                else
                    track_list.Items.Add(name);
            }
        }


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


        #region Progress Bar
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (audioFile == null) return;

            progressBar.Maximum = (int)audioFile.TotalTime.TotalSeconds;
            progressBar.Value = Math.Min((int)audioFile.CurrentTime.TotalSeconds, progressBar.Maximum);

            label_trackStart.Text = audioFile.CurrentTime.ToString(@"mm\:ss");
            label_trackEnd.Text = audioFile.TotalTime.ToString(@"mm\:ss");
        }

        private void progressBar_MouseDown(object sender, MouseEventArgs e)
        {
            if (audioFile == null) return;

            double ratio = (double)e.X / progressBar.Width;
            audioFile.CurrentTime = TimeSpan.FromSeconds(audioFile.TotalTime.TotalSeconds * ratio);

        }
        #endregion

        #region Album Art

        private void LoadAlbumArt(string path)
        {
            try
            {
                var file = TagLib.File.Create(path);
                var pic = file.Tag.Pictures.FirstOrDefault();

                if (pic != null)
                {
                    using var ms = new MemoryStream(pic.Data.Data);
                    if (music_art.Image != null)
                    {
                        music_art.Image.Dispose();
                    }
                    music_art.Image = Image.FromStream(ms);
                }
                else
                {
                    if (music_art.Image != null)
                    {
                        music_art.Image.Dispose();
                    }
                    music_art.Image = Properties.Resources._09;

                }
            }
            catch
            {
                if (music_art.Image != null)
                {
                    music_art.Image.Dispose();
                }
                music_art.Image = Properties.Resources._09;

            }
        }

        #endregion




        private void label_trackEnd_Click(object sender, EventArgs e)
        {

        }

        
    }
}
