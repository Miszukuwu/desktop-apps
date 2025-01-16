using System.Collections.Generic;

namespace Zplayer.Models;

public class MusicPlaylist
{
    private List<MusicFile> songs = new List<MusicFile>();

    public void AddSong(MusicFile musicFile) {
        songs.Add(musicFile);
    }

    public List<MusicFile> GetSongs() {
        return songs;
    }
    
}