using ManagedBass;
using System;
using System.IO;

namespace VoicebankSerializer.Models
{
    public class Audio
    {
        public string Name { get; set; }
        public byte[] Data { get; set; }

        public void Read(string path)
        {
            Data = File.ReadAllBytes(path);
        }

        public void Write(string path)
        {
            var format = new WaveFormat(44100, 16, 1);
            using (FileStream fs = File.Create(Path.Combine(path, Name + ".wav")))
            using (WaveFileWriter wfw = new WaveFileWriter(fs, format))
            {
                wfw.Write(Data, Data.Length);
            }
        }
    }
}