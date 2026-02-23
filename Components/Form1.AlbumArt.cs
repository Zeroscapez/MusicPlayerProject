using System;
using System.Collections.Generic;
using System.Text;

namespace MusicPlayer
{
    public partial class Form1
    {
        private void LoadAlbumArt(string path)
        {
            try
            {
                var file = TagLib.File.Create(path);
                var pic = file.Tag.Pictures.FirstOrDefault();

                if (pic != null)
                {
                    using var ms = new MemoryStream(pic.Data.Data);
                    music_art.Image?.Dispose();
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
    }
}
