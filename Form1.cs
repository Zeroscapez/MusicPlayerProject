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

        string[] paths, files;

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
                paths = dialog.FileNames;
                files = dialog.SafeFileNames;
                for (int i = 0; i < files.Length; i++)
                {
                    track_list.Items.Add(files[i]);
                }
            }
        }

        private void track_list_SelectedIndexChanged(object sender, EventArgs e)
        {
            musicPlayer.URL = paths[track_list.SelectedIndex];
            musicPlayer.Ctlcontrols.play();
        }

        private void stop_Button_Click(object sender, EventArgs e)
        {
            musicPlayer.Ctlcontrols.stop();
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

            if (musicPlayer.playState == WMPLib.WMPPlayState.wmppsPlaying && item != null)
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
            musicPlayer.Ctlcontrols.currentPosition = (double)e.X / progressBar.Width * musicPlayer.currentMedia.duration;
        }
    }
}
