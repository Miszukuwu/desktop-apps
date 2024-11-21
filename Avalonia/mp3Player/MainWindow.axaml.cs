using System;
using System.IO;
using System.Reflection.Metadata;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Platform.Storage;
using ManagedBass;

namespace mp3Player;

public partial class MainWindow : Window
{
    private string fileSource;
    private int currentHandle = 0;
    public MainWindow()
    {
        InitializeComponent();
        Bass.Init();
    }
    public static FilePickerFileType MusicAll { get; } = new("All Music")
    {
        Patterns = new[] { "*.mp3", "*.wav", "*.ogg" },
    };
    
    private async void chooseFile(object? sender, RoutedEventArgs e)
    {
        var topLevel = TopLevel.GetTopLevel(this);
        var files = await topLevel.StorageProvider.OpenFilePickerAsync(new FilePickerOpenOptions
        {
            Title = "Choose music file to play",
            AllowMultiple = false,
            FileTypeFilter = new[] { MusicAll }
        });
        
        fileSource = files[0].Path.ToString();
        CurrentAudio.Text = fileSource;
        Console.WriteLine(files[0].Path);
    }

    private void playButton(object? sender, RoutedEventArgs e)
    {
        playMusic(currentHandle);
    }
    private void pauseButton(object? sender, RoutedEventArgs e)
    {
        pauseMusic(currentHandle);
    }
    private void stopButton(object? sender, RoutedEventArgs e)
    {
        stopMusic(currentHandle);
    }

    private void playMusic(int handle)
    {
        if (handle == 0) {
            currentHandle = initBass(fileSource);
            handle = currentHandle;
        }
        Console.WriteLine("Playing "+fileSource+" at handle "+handle);
        Bass.ChannelPlay(handle);
    }

    private int initBass(string filePath)
    {
        int handle = 0;
        Console.WriteLine("Handle not initialized.");
        try {
            handle = Bass.CreateStream(filePath);
            Console.WriteLine("Opened Stream at handle "+handle);
        } catch (BassException e) {
            Console.WriteLine(e.Message);
        }
        return handle;
    }
    private void pauseMusic(int handle)
    {
        Bass.ChannelPause(handle);
        Console.WriteLine("Music at handle "+handle+" paused");
    }
    private void stopMusic(int handle)
    {
        Bass.ChannelStop(handle);
        Console.WriteLine("Music at handle "+handle+" stopped");
    }
}