using System;
using System.Collections.Generic;
using System.Text;

namespace VoicebankSerializer.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private string[] greetings = { "What the fuck?", "This is a terrible idea.", "Why are you using this?", "For the lols" };
        public string Greeting
        {
            get
            {
                var rand = new Random();
                return greetings[rand.Next(greetings.Length)];
            }
        }
    }
}
