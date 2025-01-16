using System;
using System.IO;
using Avalonia.Media.Imaging;

namespace Zplayer.Models;

public class MusicFile {
    public string FilePath { get; private set; }

    public string FileName
    {
        get {
            return Path.GetFileNameWithoutExtension(FilePath);
        }
        private set {
            FileName = value;
        }
    }

    public string? Title { get; private set; }
    public string? Album { get; private set; }
    public string? Artist { get; private set; }
    public Bitmap? Cover { get; private set; }
    public TimeSpan Duration { get; private set; }
    public MusicFile(string filePath, string? title, string? album, string? artist, Bitmap? cover, TimeSpan duration) {
        FilePath = filePath;
        Title = title;
        Album = album;
        Artist = artist;
        Cover = cover;
        Duration = duration;
    }

    public MusicFile(string path) {
        FilePath = path;
        TagLib.File file = TagLib.File.Create(path);
        Title = file.Tag.Title;
        Album = file.Tag.Album;
        Artist = file.Tag.FirstPerformer;
        Duration = file.Properties.Duration;
        if (file.Tag.Pictures != null && file.Tag.Pictures.Length > 0) {
            Cover = new Bitmap(new MemoryStream(file.Tag.Pictures[0].Data.Data));
        } else {
            Cover = null;
        }
    }
}