using System.Drawing;

namespace VoicebankSerializer.Models
{
    public class VoiceBank
    {
        public string Name { get; set; }
        public string Character { get; set; }
        public string ReadMe { get; set; }
        public string PrefixMap { get; set; }
        public string IconName { get; set; }
        public int[] Icon { get; set; }
        public string Config { get; set; }
        public Audio[] Recordings { get; set; }
    }
}
