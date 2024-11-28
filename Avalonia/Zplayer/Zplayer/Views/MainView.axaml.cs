using System;
using System.IO;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media.Imaging;
using Avalonia.VisualTree;
using TagLib;

namespace Zplayer.Views;

public partial class MainView : UserControl
{
    private string _path;
    public MainView()
    {
        InitializeComponent();
    }

    private async void openFile(object? sender, RoutedEventArgs e)
    {
        var window = this.GetVisualRoot() as Window;
        var dialog = new OpenFileDialog();
        dialog.Title = "Otwórz plik mp3";
        string[] result = await dialog.ShowAsync(window);
        if (result != null)
        {
            _path = result[0];
            TagLib.File file = TagLib.File.Create(result[0]);
            double fileLength = Convert.ToDouble(file.Properties.Duration.TotalSeconds);
            
            songSlider.Maximum = Math.Round(fileLength, MidpointRounding.ToEven);
            songLength.Text = file.Properties.Duration.Minutes.ToString() + ":" + file.Properties.Duration.Seconds.ToString() ;

            songTitle.Text = file.Tag.Title;
            
            if (file.Tag.Pictures.Length > 0) {
                coverImage.Source = new Bitmap(new MemoryStream(file.Tag.Pictures[0].Data.Data));
            }
        }
    }

    private void playSong(object? sender, RoutedEventArgs e)
    {
        
    }
}