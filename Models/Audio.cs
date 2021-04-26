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
            byte[] rawBytes = File.ReadAllBytes(path);
            Data = new ArraySegment<byte>(rawBytes, 46, rawBytes.Length - 46).Array;
        }

        public void Write(string path)
        {
            // write data to path
        }
    }
}