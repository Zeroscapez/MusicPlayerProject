namespace MusicPlayer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            music_volume.Value = 50; // Set default volume to 50%
            volume_percent.Text = music_volume.Value.ToString() + "%";
        }



        List<string> paths = new();
        List<string> files = new();

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }


        private void musicPlayer_Enter(object sender, EventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void label1_Click_2(object sender, EventArgs e)
        {

        }

        private void label1_Click_3(object sender, EventArgs e)
        {

        }



        private void open_Button_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Multiselect = true;


            if (dialog.ShowDialog() == DialogResult.OK)
            {
                //Paths and files are generated as files are added
                var newPaths = dialog.FileNames;
                var newFiles = dialog.SafeFileNames;


                //Append files to the end of the current list
                for (int i = 0; i < newPaths.Length; i++)
                {
                    paths.Add(newPaths[i]);
                    files.Add(newFiles[i]);

                    //Show filesname without the extension at the end when shown in list
                    var displayName = Path.GetFileNameWithoutExtension(newFiles[i]);
                    track_list.Items.Add(displayName);
                }

                track_list_DrawItem(track_list, new DrawItemEventArgs(track_list.CreateGraphics(), track_list.Font, track_list.ClientRectangle, 0, DrawItemState.Selected));
            }
        }

        private void track_list_SelectedIndexChanged(object sender, EventArgs e)
        {
            int idx = track_list.SelectedIndex;
            if (idx < 0 || idx >= paths.Count)
            {
                return; // Invalid index, do nothing
            }

            musicPlayer.URL = paths[idx];
            musicPlayer.Ctlcontrols.play();

            try
            {
                var file = TagLib.File.Create(paths[track_list.SelectedIndex]);
                var pic = file.Tag.Pictures?.FirstOrDefault();
                if (pic != null)
                {
                    var bin = (byte[])pic.Data.Data;
                    using var ms = new MemoryStream(bin);
                    //Replace image when the data for it exists, otherwise set it to null
                    music_art.Image = Image.FromStream(ms);
                }
                else
                {
                    //if imagine does not exist
                    music_art.Image = Properties.Resources._09;
                }

            }
            catch
            {
                //If there's an error reading the file or its metadata, set the image to a default one
                music_art.Image = Properties.Resources._09;
            }


        }

        private void stop_Button_Click(object sender, EventArgs e)
        {
            musicPlayer.Ctlcontrols.stop();
            progressBar.Value = 0;
            music_art.Image = null;
        }

        private void pause_Button_Click(object sender, EventArgs e)
        {
            musicPlayer.Ctlcontrols.pause();
        }

        private void play_Button_Click(object sender, EventArgs e)
        {
            musicPlayer.Ctlcontrols.play();
        }

        private void previous_Button_Click(object sender, EventArgs e)
        {
            if (track_list.Items.Count == 0)
            {
                return;
            }
            if (track_list.SelectedIndex > 0)
            {
                track_list.SelectedIndex = track_list.SelectedIndex - 1;
            }
            else
            {
                track_list.SelectedIndex = track_list.Items.Count - 1;
            }
        }

        private void next_button_Click(object sender, EventArgs e)
        {
            if (track_list.Items.Count == 0)
            {
                return;
            }
            if (track_list.SelectedIndex < track_list.Items.Count - 1)
            {
                track_list.SelectedIndex = track_list.SelectedIndex + 1;
            }
            else
            {
                track_list.SelectedIndex = 0;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            var ctl = musicPlayer?.Ctlcontrols;
            if (ctl == null) return;

            var item = ctl.currentItem;

            if (musicPlayer?.playState == WMPLib.WMPPlayState.wmppsPlaying && item != null)
            {
                progressBar.Maximum = (int)item.duration;
                progressBar.Value = (int)ctl.currentPosition;
            }

            // Safely update labels
            label_trackStart.Text = ctl.currentPositionString ?? "00:00";
            label_trackEnd.Text = item?.durationString ?? "00:00";
        }


        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            musicPlayer.settings.volume = music_volume.Value;
            volume_percent.Text = music_volume.Value.ToString() + "%";
        }

        private void label_trackEnd_Click(object sender, EventArgs e)
        {

        }

        private void progressBar_MouseDown(object sender, MouseEventArgs e)
        {
            if (musicPlayer.playState == WMPLib.WMPPlayState.wmppsPlaying)
            {
                musicPlayer.Ctlcontrols.currentPosition = (double)e.X / progressBar.Width * musicPlayer.currentMedia.duration;
            }

        }

        private void track_list_DrawItem(object sender, DrawItemEventArgs e)
        {
            if(e.Index < 0) return;

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
    }
}
