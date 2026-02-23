using System;
using System.Collections.Generic;
using System.Text;

namespace MusicPlayer
{
    public partial class Form1
    {
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

    }
}
