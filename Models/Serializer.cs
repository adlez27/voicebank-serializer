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
                    vb.IconName = Path.GetFileNameWithoutExtension(filePath);
                    var img = new Bitmap(filePath);
                    vb.Icon = new int[10000];
                    for (var x = 0; x < 100; x++)
                    {
                        for (var y = 0; y < 100; y++)
                        {
                            vb.Icon[x*100 + y] = img.GetPixel(x, y).ToArgb();
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

        public  void Deserialize()
        {
            var raw = File.ReadAllText(file);
            var deserializer = new YamlDotNet.Serialization.Deserializer();
            var vb = deserializer.Deserialize<VoiceBank>(raw);
            var destination = Path.Combine(folder, vb.Name);
            Encoding e = CodePagesEncodingProvider.Instance.GetEncoding(932);

            Directory.CreateDirectory(destination);
            File.WriteAllText(Path.Combine(destination, "readme.txt"), vb.ReadMe, e);
            File.WriteAllText(Path.Combine(destination, "character.txt"), vb.Character, e);
            File.WriteAllText(Path.Combine(destination, "prefix.map"), vb.PrefixMap, e);
            File.WriteAllText(Path.Combine(destination, "oto.ini"), vb.Config, e);

            var img = new Bitmap(100, 100);
            for (var x = 0; x < 100; x++)
            {
                for (var y = 0; y < 100; y++)
                {
                    img.SetPixel(x, y, Color.FromArgb(vb.Icon[x*100 + y]));
                }
            }
            img.Save(Path.Combine(destination, vb.IconName + ".bmp"));

            foreach(var audio in vb.Recordings)
            {
                audio.Write(destination);
            }
        }
    }
}
