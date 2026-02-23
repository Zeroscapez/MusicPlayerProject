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
        private int currentIndex = -1;
        //Shuffling
        private bool isShuffleEnabled = false;

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
            outputDevice = new WaveOutEvent();
            outputDevice.PlaybackStopped += OutputDevice_PlaybackStopped;

            playlist.Clear();
            musicFolderPath = Properties.Settings.Default.MusicFolderPath;

            if (!string.IsNullOrEmpty(musicFolderPath) &&
                Directory.Exists(musicFolderPath))
            {
                LoadMusicFromFolder(musicFolderPath);
            }
        }




        private void label1_Click(object sender, EventArgs e) { }

        private void groupBox1_Enter(object sender, EventArgs e) { }

        private void pictureBox1_Click(object sender, EventArgs e) { }

        private void musicPlayer_Enter(object sender, EventArgs e) { }

        private void label1_Click_1(object sender, EventArgs e) { }

        private void label1_Click_2(object sender, EventArgs e) { }

        private void label1_Click_3(object sender, EventArgs e) { }

       
    }
}
