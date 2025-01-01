using System;
using System.IO;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media.Imaging;
using Avalonia.VisualTree;
using ManagedBass;
using TagLib;

namespace Zplayer.Views;

public partial class MainView : UserControl
{
    private string _filePath;
    private bool _isPlaying;
    private int _currentHandle = 0;
    
    public MainView() {
        InitializeComponent();
        Bass.Init();
    }
    
    private async void LoadSong(object? sender, RoutedEventArgs e) {
        // Show dialog window and get file path
        var window = this.GetVisualRoot() as Window;
        var dialog = new OpenFileDialog();
        dialog.Title = "Otwórz plik mp3";
        string[] result = await dialog.ShowAsync(window);
        Console.WriteLine("Opening "+result[0]);
        
        if (result.Length != 1) return;
        
        // Set tag info to variables
        _filePath = result[0];
        TagLib.File tagFile = TagLib.File.Create(result[0]);
        double songLength = Convert.ToDouble(tagFile.Properties.Duration.TotalSeconds);
        
        SongTitle.Text = (!string.IsNullOrEmpty(tagFile.Tag.Title))?tagFile.Tag.Title:Path.GetFileNameWithoutExtension(_filePath);
        SongArtist.Text = (!string.IsNullOrEmpty(tagFile.Tag.FirstPerformer))?tagFile.Tag.FirstPerformer:"<Unknown>";
            
        SongSlider.Maximum = Math.Round(songLength, MidpointRounding.ToEven);
        string maxLenghtStr = tagFile.Properties.Duration.Minutes + ":";
        if (tagFile.Properties.Duration.Seconds < 10) {
            maxLenghtStr += "0";
        }
        maxLenghtStr += tagFile.Properties.Duration.Seconds;
        SongLength.Text = maxLenghtStr;
        
        if (tagFile.Tag.Pictures.Length > 0) {
            CoverImage.Source = new Bitmap(new MemoryStream(tagFile.Tag.Pictures[0].Data.Data));
        } else {
            CoverImage.Source = new Bitmap(AppContext.BaseDirectory+"/../../../../Zplayer/Assets/default.png");
        }
    }
    private int initBass(string filePath) {
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
    private void PlaySong(object? sender, RoutedEventArgs e) {
        if (string.IsNullOrEmpty(_filePath)) return;
        if (_isPlaying == true) return;
        if (_currentHandle == 0) {
            _currentHandle = initBass(_filePath);
        } 
        Console.WriteLine("Playing "+_filePath+" at handle "+_currentHandle);
        Bass.ChannelPlay(_currentHandle);
        _isPlaying = true;
    }

    private void StopSong(object? sender, RoutedEventArgs e) {
        if (_isPlaying == false) return;
        Console.WriteLine("Stopping playback");
        Bass.ChannelPause(_currentHandle);
        _isPlaying = false;
    }
}