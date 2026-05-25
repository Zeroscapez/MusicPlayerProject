using System;
using System.Collections.Generic;
using System.Text;

namespace MusicPlayer
{
    public partial class Form1
    {
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (audioFile == null)
            {
                progressBar.Value = 0;
                label_trackStart.Text = "00:00";
                label_trackEnd.Text = "00:00";
                return;
            }

            var current = (int)audioFile.CurrentTime.TotalSeconds;
            var total = (int)audioFile.TotalTime.TotalSeconds;

            // Only set Maximum once per track, not every tick
            if (progressBar.Maximum != total)
                progressBar.Maximum = total;

            progressBar.Value = Math.Min(current, progressBar.Maximum - 1);

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
