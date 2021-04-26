using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using VoicebankSerializer.ViewModels;

namespace VoicebankSerializer.Views
{
    public class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
        }

        private void InitializeComponent()
        {
            this.DataContext = new MainWindowViewModel(this);
            AvaloniaXamlLoader.Load(this);
        }
    }
}
