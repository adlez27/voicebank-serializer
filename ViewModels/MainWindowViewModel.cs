using Avalonia.Controls;
using System;
using System.Collections.Generic;

namespace VoicebankSerializer.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private Window window;

        public MainWindowViewModel() { }
        public MainWindowViewModel(Window w)
        {
            window = w;
        }

        private string[] greetings =
        {
            "What the fuck?",
            "This is a terrible idea.",
            "Why are you using this?",
            "For the lols",
            "Bruh what are you doing",
            "Unsexy but you do you"
        };
        public string Greeting
        {
            get
            {
                var rand = new Random();
                return greetings[rand.Next(greetings.Length)];
            }
        }

        public async void Serialize()
        {
            OpenFolderDialog openFolderDialog = new OpenFolderDialog();
            var selectedFolder = await openFolderDialog.ShowAsync(window);
            if (selectedFolder == null || selectedFolder.Length == 0)
            {
                // do nothing
            } 
            else
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.DefaultExtension = "yml";

                var yml = new FileDialogFilter();
                yml.Name = "YAML";
                yml.Extensions = new List<string>() { "yml" };
                saveFileDialog.Filters = new List<FileDialogFilter>() { yml };

                var exportFile = await saveFileDialog.ShowAsync(window);

                if (exportFile == null || exportFile.Length == 0)
                {
                    // do nothing
                }
                else
                {
                    // pass selectedFolder and exportFile to a serializer
                }
            }
        }

        public async void Deserialize()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.AllowMultiple = false;

            var yml = new FileDialogFilter();
            yml.Name = "YAML";
            yml.Extensions = new List<string>() { "yml" };
            openFileDialog.Filters = new List<FileDialogFilter>() { yml };
            
            var selectedFile = await openFileDialog.ShowAsync(window);
            if (selectedFile == null || selectedFile.Length == 0)
            {
                // do nothing
            }
            else
            {
                OpenFolderDialog openFolderDialog = new OpenFolderDialog();
                var exportFolder = await openFolderDialog.ShowAsync(window);
                if (exportFolder == null || exportFolder.Length == 0)
                {
                    // do nothing
                }
                else
                {
                    // pass selectedFile and exportFolder to a deserializer
                }
            }
        }
    }
}
