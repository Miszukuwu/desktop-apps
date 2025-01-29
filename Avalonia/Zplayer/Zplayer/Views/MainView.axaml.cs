using System;
using System.Collections.ObjectModel;
using System.IO;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Input;
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
            Console.WriteLine("Getting current position");
            return Bass.ChannelBytes2Seconds(_currentHandle, Bass.ChannelGetPosition(_currentHandle, PositionFlags.Bytes));
        }
        set {
            Console.WriteLine("Setting current position "+value);
            Bass.ChannelSetPosition(_currentHandle, Bass.ChannelSeconds2Bytes(_currentHandle, value));
        }
    }
    private DispatcherTimer _timer;
    private bool _isPlaying = false;
    private int _currentHandle = 0;
    
    public MainView() {
        InitializeComponent();
        Bass.Init();

        _timer = new DispatcherTimer();
        _timer.Interval = TimeSpan.FromMilliseconds(500);
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
    
    private async void AddFile(object? sender, RoutedEventArgs e) {
        // Show dialog window and get file path
        var window = this.GetVisualRoot() as Window;
        var dialog = new OpenFileDialog();
        dialog.Title = "Otwórz plik mp3";
        string[] result = await dialog.ShowAsync(window);
        if (result.Length != 1) return;
        Console.WriteLine("Opening "+result[0]);
        
        // Check if the format is supported
        if (!Bass.SupportedFormats.Contains(Path.GetExtension(result[0]))) {
            Console.WriteLine("Format not supported "+result[0]);
            return;
        }
        // Creating song object and adding it to list
        MusicFile song = new MusicFile(result[0]);
        Playlist.Add(song);
    }

    void LoadSong(MusicFile song) {
        if (song == null) throw new ArgumentNullException(nameof(song));
        // If already playing free channel
        if (_currentHandle != 0) {
            StopSong();
            Bass.StreamFree(_currentHandle);
            Bass.ChannelStop(_currentHandle);
            Bass.MusicFree(_currentHandle);
        }
        SongTitle.Text = song.Title;
        SongArtist.Text = song.Artist;
        
        SongSlider.Maximum = Math.Round(Convert.ToDouble(song.Duration.TotalSeconds), MidpointRounding.ToEven);
        SongSlider.Value = -1;
        lastPosition = 0;
        string maxLenghtStr = song.Duration.Minutes + ":";
        if (song.Duration.Seconds < 10) {
            maxLenghtStr += "0";
        }
        maxLenghtStr += song.Duration.Seconds;
        SongLengthBlock.Text = maxLenghtStr;
        CurrentTimestampBlock.Text = "0:00";
        CoverImage.Source = song.Cover;
        
        _currentHandle = InitBass(song.FilePath);
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
    private double lastPosition = 0;
    private void UpdateTick(object? sender, EventArgs e) {
        if (!_isPlaying) {
            return;
        }
        double currentPosition = CurrentPosition;
        Console.WriteLine("Start Timetick "+currentPosition);
        string currentTimestampstr = Math.Floor(currentPosition/60)+":";
        if (currentPosition % 60 < 10) currentTimestampstr += 0;
        currentTimestampstr += Math.Floor(currentPosition % 60);
        CurrentTimestampBlock.Text = currentTimestampstr;
        if (CurrentSong != null) {
            if (Math.Abs(currentPosition-CurrentSong.Duration.TotalSeconds) < 0.2) {
                Console.WriteLine("Song ended "+currentPosition+" / "+CurrentSong.Duration.TotalSeconds+" Stopping");
                LoadSong(CurrentSong);
                return;
            }
        }
        if (!sliderChanging) {
            SongSlider.Value = currentPosition; 
            lastPosition = SongSlider.Value;
        }
        Console.WriteLine("Timetick "+currentPosition+" "+CurrentSong.Duration.TotalSeconds+" Playing");
    }
    private void SongSelected(object? sender, SelectionChangedEventArgs e) {
        LoadSong(PlaylistBox.SelectedItem as MusicFile);
    }
    private bool sliderChanging = false;
    private void SliderChanged(object? sender, RangeBaseValueChangedEventArgs e) {
        if (SongSlider.Value - lastPosition == 0.5 || SongSlider.Value == -1) {
            Console.WriteLine("Slider didnt change by user");
            sliderChanging = false;
            return;
        }
        Console.WriteLine("Slider Change by user "+SongSlider.Value+" - "+lastPosition+" = "+(SongSlider.Value - lastPosition));
        sliderChanging = true;
        CurrentPosition = SongSlider.Value;
        lastPosition = SongSlider.Value;
        if (_isPlaying == false) PlaySong();
    }
}