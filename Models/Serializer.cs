using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoicebankSerializer.Models
{
    public class Serializer
    {
        private string folder, file;
        public Serializer(string folder, string file)
        {
            this.folder = folder;
            this.file = file;
        }

        public void Serialize()
        {
            var vb = new VoiceBank();
            var audioFiles = new List<Audio>();
            vb.Name = Path.GetFileName(folder);
            Encoding e = CodePagesEncodingProvider.Instance.GetEncoding(932);

            var allFiles = Directory.GetFiles(folder);
            foreach (var filePath in allFiles)
            {
                var name = Path.GetFileName(filePath);
                var ext = Path.GetExtension(filePath);

                if (name == "character.txt")
                {
                    vb.Character = File.ReadAllText(filePath, e);
                }
                else if (name == "readme.txt")
                {
                    vb.ReadMe = File.ReadAllText(filePath, e);
                }
                else if (name == "prefix.map")
                {
                    vb.PrefixMap = File.ReadAllText(filePath, e);
                }
                else if (name == "oto.ini")
                {
                    vb.Config = File.ReadAllText(filePath, e);
                }
                else if (ext == ".bmp")
                {
                    var img = new Bitmap(filePath);
                    vb.Icon = new int[100, 100];
                    for (var i = 0; i < 100; i++)
                    {
                        for (var j = 0; j < 100; j++)
                        {
                            vb.Icon[i, j] = img.GetPixel(i, j).ToArgb();
                        }
                    }
                }
                else if (ext == ".wav")
                {
                    var audio = new Audio();
                    audio.Name = Path.GetFileNameWithoutExtension(filePath);
                    audio.Read(filePath);
                    audioFiles.Add(audio);
                }
            }
            
            vb.Recordings = audioFiles.ToArray();

            using (StreamWriter outFile = new(file))
            {
                var serializer = new YamlDotNet.Serialization.Serializer();
                serializer.Serialize(outFile, vb);
            }
        }

        public void Deserialize()
        {
            // do the thing
        }
    }
}
