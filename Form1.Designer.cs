namespace MusicPlayer
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            groupBox1 = new GroupBox();
            next_button = new Button();
            volume_percent = new Label();
            volume_label = new Label();
            music_volume = new TrackBar();
            track_list = new ListBox();
            open_Button = new Button();
            stop_Button = new Button();
            pause_Button = new Button();
            play_Button = new Button();
            previous_Button = new Button();
            progressBar = new ProgressBar();
            music_art = new PictureBox();
            label_trackStart = new Label();
            label_trackEnd = new Label();
            panel1 = new Panel();
            timer1 = new System.Windows.Forms.Timer(components);
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)music_volume).BeginInit();
            ((System.ComponentModel.ISupportInitialize)music_art).BeginInit();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.BackColor = Color.Black;
            groupBox1.Controls.Add(next_button);
            groupBox1.Controls.Add(volume_percent);
            groupBox1.Controls.Add(volume_label);
            groupBox1.Controls.Add(music_volume);
            groupBox1.Controls.Add(track_list);
            groupBox1.Controls.Add(open_Button);
            groupBox1.Controls.Add(stop_Button);
            groupBox1.Controls.Add(pause_Button);
            groupBox1.Controls.Add(play_Button);
            groupBox1.Controls.Add(previous_Button);
            groupBox1.Font = new Font("SamsungOneUI Medium Condensed", 11.25F, FontStyle.Bold);
            groupBox1.ForeColor = Color.Blue;
            groupBox1.Location = new Point(12, 61);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(754, 283);
            groupBox1.TabIndex = 1;
            groupBox1.TabStop = false;
            groupBox1.Text = "Controls";
            groupBox1.Enter += groupBox1_Enter;
            // 
            // next_button
            // 
            next_button.BackColor = Color.Black;
            next_button.FlatAppearance.BorderColor = Color.Blue;
            next_button.FlatStyle = FlatStyle.Flat;
            next_button.Font = new Font("SamsungOneUI Medium Condensed", 11.25F, FontStyle.Bold);
            next_button.ForeColor = Color.Blue;
            next_button.Location = new Point(131, 26);
            next_button.Name = "next_button";
            next_button.Size = new Size(105, 30);
            next_button.TabIndex = 7;
            next_button.Text = "Next";
            next_button.UseVisualStyleBackColor = false;
            next_button.Click += next_button_Click;
            // 
            // volume_percent
            // 
            volume_percent.AutoSize = true;
            volume_percent.Location = new Point(705, 57);
            volume_percent.Name = "volume_percent";
            volume_percent.Size = new Size(47, 21);
            volume_percent.TabIndex = 4;
            volume_percent.Text = "100%";
            // 
            // volume_label
            // 
            volume_label.AutoSize = true;
            volume_label.Location = new Point(709, 252);
            volume_label.Name = "volume_label";
            volume_label.Size = new Size(36, 21);
            volume_label.TabIndex = 4;
            volume_label.Text = "VOL";
            volume_label.Click += label1_Click_1;
            // 
            // music_volume
            // 
            music_volume.Location = new Point(707, 81);
            music_volume.Maximum = 100;
            music_volume.Name = "music_volume";
            music_volume.Orientation = Orientation.Vertical;
            music_volume.Size = new Size(45, 170);
            music_volume.TabIndex = 6;
            music_volume.TickFrequency = 0;
            music_volume.TickStyle = TickStyle.Both;
            music_volume.Scroll += trackBar1_Scroll;
            // 
            // track_list
            // 
            track_list.BackColor = Color.DarkGray;
            track_list.Font = new Font("SamsungOneUI Medium Condensed", 11.25F, FontStyle.Bold);
            track_list.FormattingEnabled = true;
            track_list.Location = new Point(6, 62);
            track_list.Name = "track_list";
            track_list.Size = new Size(695, 214);
            track_list.TabIndex = 5;
            track_list.DrawItem += track_list_DrawItem;
            track_list.SelectedIndexChanged += track_list_SelectedIndexChanged;
            // 
            // open_Button
            // 
            open_Button.BackColor = Color.Black;
            open_Button.FlatAppearance.BorderColor = Color.Blue;
            open_Button.FlatStyle = FlatStyle.Flat;
            open_Button.Font = new Font("SamsungOneUI Medium Condensed", 11.25F, FontStyle.Bold);
            open_Button.ForeColor = Color.Blue;
            open_Button.Location = new Point(575, 26);
            open_Button.Name = "open_Button";
            open_Button.Size = new Size(105, 30);
            open_Button.TabIndex = 4;
            open_Button.Text = "Open";
            open_Button.UseVisualStyleBackColor = false;
            open_Button.Click += open_Button_Click;
            // 
            // stop_Button
            // 
            stop_Button.BackColor = Color.Black;
            stop_Button.FlatAppearance.BorderColor = Color.Blue;
            stop_Button.FlatStyle = FlatStyle.Flat;
            stop_Button.Font = new Font("SamsungOneUI Medium Condensed", 11.25F, FontStyle.Bold);
            stop_Button.ForeColor = Color.Blue;
            stop_Button.Location = new Point(464, 26);
            stop_Button.Name = "stop_Button";
            stop_Button.Size = new Size(105, 30);
            stop_Button.TabIndex = 3;
            stop_Button.Text = "Stop";
            stop_Button.UseVisualStyleBackColor = false;
            stop_Button.Click += stop_Button_Click;
            // 
            // pause_Button
            // 
            pause_Button.BackColor = Color.Black;
            pause_Button.FlatAppearance.BorderColor = Color.Blue;
            pause_Button.FlatStyle = FlatStyle.Flat;
            pause_Button.Font = new Font("SamsungOneUI Medium Condensed", 11.25F, FontStyle.Bold);
            pause_Button.ForeColor = Color.Blue;
            pause_Button.Location = new Point(353, 26);
            pause_Button.Name = "pause_Button";
            pause_Button.Size = new Size(105, 30);
            pause_Button.TabIndex = 2;
            pause_Button.Text = "Pause";
            pause_Button.UseVisualStyleBackColor = false;
            pause_Button.Click += pause_Button_Click;
            // 
            // play_Button
            // 
            play_Button.BackColor = Color.Black;
            play_Button.FlatAppearance.BorderColor = Color.Blue;
            play_Button.FlatStyle = FlatStyle.Flat;
            play_Button.Font = new Font("SamsungOneUI Medium Condensed", 11.25F, FontStyle.Bold);
            play_Button.ForeColor = Color.Blue;
            play_Button.Location = new Point(242, 26);
            play_Button.Name = "play_Button";
            play_Button.Size = new Size(105, 30);
            play_Button.TabIndex = 1;
            play_Button.Text = "Play";
            play_Button.UseVisualStyleBackColor = false;
            play_Button.Click += play_Button_Click;
            // 
            // previous_Button
            // 
            previous_Button.BackColor = Color.Black;
            previous_Button.FlatAppearance.BorderColor = Color.Blue;
            previous_Button.FlatStyle = FlatStyle.Flat;
            previous_Button.Font = new Font("SamsungOneUI Medium Condensed", 11.25F, FontStyle.Bold);
            previous_Button.ForeColor = Color.Blue;
            previous_Button.Location = new Point(20, 26);
            previous_Button.Name = "previous_Button";
            previous_Button.Size = new Size(105, 30);
            previous_Button.TabIndex = 0;
            previous_Button.Text = "Previous";
            previous_Button.UseVisualStyleBackColor = false;
            previous_Button.Click += previous_Button_Click;
            // 
            // progressBar
            // 
            progressBar.Location = new Point(18, 350);
            progressBar.Name = "progressBar";
            progressBar.Size = new Size(1050, 18);
            progressBar.TabIndex = 2;
            progressBar.MouseDown += progressBar_MouseDown;
            // 
            // music_art
            // 
            music_art.BackColor = Color.Black;
            music_art.BackgroundImageLayout = ImageLayout.None;
            music_art.Image = Properties.Resources._09;
            music_art.Location = new Point(772, 61);
            music_art.Name = "music_art";
            music_art.Size = new Size(320, 283);
            music_art.SizeMode = PictureBoxSizeMode.Zoom;
            music_art.TabIndex = 3;
            music_art.TabStop = false;
            music_art.Click += pictureBox1_Click;
            // 
            // label_trackStart
            // 
            label_trackStart.AutoSize = true;
            label_trackStart.Font = new Font("SamsungOneUILatin 700", 24F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label_trackStart.ForeColor = Color.Blue;
            label_trackStart.Location = new Point(12, 375);
            label_trackStart.Name = "label_trackStart";
            label_trackStart.Size = new Size(100, 43);
            label_trackStart.TabIndex = 4;
            label_trackStart.Text = "00:00";
            label_trackStart.Click += label1_Click_3;
            // 
            // label_trackEnd
            // 
            label_trackEnd.AutoSize = true;
            label_trackEnd.Font = new Font("SamsungOneUILatin 700", 24F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label_trackEnd.ForeColor = Color.Blue;
            label_trackEnd.Location = new Point(968, 375);
            label_trackEnd.Name = "label_trackEnd";
            label_trackEnd.Size = new Size(100, 43);
            label_trackEnd.TabIndex = 5;
            label_trackEnd.Text = "00:00";
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(0, 0, 192);
            panel1.Location = new Point(2, 51);
            panel1.Name = "panel1";
            panel1.Size = new Size(1103, 321);
            panel1.TabIndex = 6;
            // 
            // timer1
            // 
            timer1.Enabled = true;
            timer1.Tick += timer1_Tick;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Black;
            ClientSize = new Size(1102, 427);
            Controls.Add(label_trackEnd);
            Controls.Add(label_trackStart);
            Controls.Add(music_art);
            Controls.Add(progressBar);
            Controls.Add(groupBox1);
            Controls.Add(panel1);
            Name = "Form1";
            Text = "C# Music Player";
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)music_volume).EndInit();
            ((System.ComponentModel.ISupportInitialize)music_art).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private GroupBox groupBox1;
        private Button previous_Button;
        private ListBox track_list;
        private Button open_Button;
        private Button stop_Button;
        private Button pause_Button;
        private Button play_Button;
        private TrackBar music_volume;
        private ProgressBar progressBar;
        private PictureBox music_art;
        private Label volume_label;
        private Label volume_percent;
        private Label label_trackStart;
        private Label label_trackEnd;
        private Panel panel1;
        private Button next_button;
        private System.Windows.Forms.Timer timer1;
    }
}
