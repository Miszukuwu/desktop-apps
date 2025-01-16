using System;
using System.Collections.ObjectModel;
using System.IO;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media.Imaging;
using Avalonia.Threading;
using Avalonia.VisualTree;
using ManagedBass;
using Zplayer.Models;

namespace Zplayer.Views;

public partial class MainView : UserControl
{
    public MusicFile? CurrentSong;
    // public MusicPlaylist Playlist = new MusicPlaylist();
    public ObservableCollection<MusicFile> Playlist = new();
    public double CurrentPosition {
        get {
            return Bass.ChannelBytes2Seconds(_currentHandle, Bass.ChannelGetPosition(_currentHandle, PositionFlags.Bytes));
        }
        set {
            Bass.ChannelSetPosition(_currentHandle, Bass.ChannelSeconds2Bytes(_currentHandle, value));
        }
    }

    private DispatcherTimer _timer;
    private bool _isPlaying = false;
    private int _currentHandle = 0;
    
    public MainView() {
        InitializeComponent();
        DataContext = this;
        Bass.Init();

        _timer = new DispatcherTimer();
        _timer.Interval = TimeSpan.FromSeconds(1);
        _timer.Tick += UpdateTick;
        
        PlaylistBox.ItemsSource = Playlist;
    }
    
    private int InitBass(string filePath) {
        int handle = 0;
        Console.WriteLine("Initializing handle.");
        try {
            handle = Bass.CreateStream(filePath);
            Console.WriteLine("Opened Stream at handle "+handle);
        } catch (BassException e) {
            Console.WriteLine(e.Message);
        }
        return handle;
    }

    private void UpdateTick(object? sender, EventArgs e) {
        SongSlider.Value = CurrentPosition;
        string currentTimestamp = Math.Floor(CurrentPosition/60)+":";
        if (CurrentPosition % 60 < 10) currentTimestamp += 0;
        currentTimestamp += Math.Floor(CurrentPosition % 60);
        CurrentTimestamp.Text = currentTimestamp;
        if (CurrentSong != null) {
            if (CurrentPosition > CurrentSong.Duration.TotalSeconds) StopButton(null, null);
        }
    }
    
    private async void AddFile(object? sender, RoutedEventArgs e) {
        // Show dialog window and get file path
        var window = this.GetVisualRoot() as Window;
        var dialog = new OpenFileDialog();
        dialog.Title = "Otwórz plik mp3";
        string[] result = await dialog.ShowAsync(window);
        if (result.Length < 1) {
            return;
        }
        Console.WriteLine("Opening "+result[0]);
        
        if (result.Length != 1) return;
        
        // Set tag info to variables
        if (!Bass.SupportedFormats.Contains(Path.GetExtension(result[0]))) {
            Console.WriteLine("Format not supported "+result[0]);
            return;
        }
        MusicFile song = new MusicFile(result[0]);
        Playlist.Add(song);
    }

    void LoadSong(MusicFile song) {
        // If already playing free channel
        if (_currentHandle != 0) {
            Bass.StreamFree(_currentHandle);
            Bass.ChannelStop(_currentHandle);
            Bass.MusicFree(_currentHandle);
        }
        SongTitle.Text = (!string.IsNullOrEmpty(song.Title))?song.Title:song.FileName;
        SongArtist.Text = (!string.IsNullOrEmpty(song.Artist))?song.Artist:"<Unknown>";
        
        SongSlider.Maximum = Math.Round(Convert.ToDouble(song.Duration.TotalSeconds), MidpointRounding.ToEven);
        string maxLenghtStr = song.Duration.Minutes + ":";
        if (song.Duration.Seconds < 10) {
            maxLenghtStr += "0";
        }
        maxLenghtStr += song.Duration.Seconds;
        SongLength.Text = maxLenghtStr;
        
        if (song.Cover != null) {
            CoverImage.Source = song.Cover;
        } else {
            CoverImage.Source = new Bitmap(AppContext.BaseDirectory+"/../../../../Zplayer/Assets/default.png");
        }
        
        _currentHandle = InitBass(song.FilePath);
        _isPlaying = false;
        CurrentSong = song;
    }
    
    private void PlayButton(object? sender, RoutedEventArgs e) {
        PlaySong();
    }

    private void PlaySong() {
        if (CurrentSong == null) return;
        if (_isPlaying == true) return;
        Console.WriteLine("Playing "+CurrentSong.FilePath+" at handle "+_currentHandle);
        Bass.ChannelPlay(_currentHandle);
        _isPlaying = true;
        _timer.Start();
    }
    private void StopButton(object? sender, RoutedEventArgs e) {
        StopSong();
    }

    private void StopSong() {
        if (_isPlaying == false) return;
        Console.WriteLine("Stopping playback");
        Bass.ChannelPause(_currentHandle);
        _isPlaying = false;
        _timer.Stop();
    }
}