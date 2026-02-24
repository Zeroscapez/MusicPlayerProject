using System;
using System.Collections.Generic;
using System.Text;

namespace MusicPlayer
{
    public class Track
    {
        public string FilePath { get; set; }
        public string FileName => Path.GetFileName(FilePath);
        public string DisplayName => Path.GetFileNameWithoutExtension(FilePath);

        public Track(string path)
        {
            FilePath = path;
        }
    }
}
