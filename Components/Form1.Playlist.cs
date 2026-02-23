using System;
using System.Collections.Generic;
using System.Text;

namespace MusicPlayer
{
    public partial class Form1
    {
        private List<Track> playlist = [];
        private readonly List<int> playOrder = [];
        private int playOrderPosition = -1;
        private string musicFolderPath = null;
        private readonly string[] supportedExtensions = { ".mp3", ".wav", ".flac", ".aac", ".ogg" };


        private void BuildPlayerOrder()
        {
            playOrder.Clear();
            for (int i = 0; i < playlist.Count; i++)
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

        private void track_list_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (track_list.SelectedIndex < 0 || playOrder.Count == 0)
                return;

            // UI index is always the playOrder position
            int selectedPlayOrderPosition = track_list.SelectedIndex;

            PlayTrackByPlayOrder(selectedPlayOrderPosition);

        }

        private void LoadMusicFromFolder(string folderPath)
        {
            if (!Directory.Exists(folderPath))
                return;

            var files = Directory.GetFiles(folderPath)
                                 .Where(f => supportedExtensions.Contains(
                                     Path.GetExtension(f).ToLower()))
                                 .ToList();

            foreach (var file in files)
            {
                AddTrack(file);
            }

            BuildPlayerOrder();
            UpdatePlaylistDisplay();
        }

        private void AddTrack(string path)
        {
            if (!File.Exists(path))
                return;

            var track = new Track(path);
            playlist.Add(track);
        }

        private void SaveFolderPath()
        {
            Properties.Settings.Default.MusicFolderPath = musicFolderPath;
            Properties.Settings.Default.Save();
        }

        private void UpdatePlaylistDisplay()
        {
            track_list.Items.Clear();

            foreach (var index in playOrder)
            {
                track_list.Items.Add(playlist[index].DisplayName);
            }
        }

    }
}
