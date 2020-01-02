using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MP3Randomizer2.Core
{
    
    public class Song
    {
        /// <summary>
        ///   This number is used to identify the original song.
        ///   E.G: If a song is renamed to 6,  the number would become 6. After that it will(in another method) check for a file named '6',
        ///   and give back it's original title & extension.
        /// </summary>

        public int number { get; set; } 
        public string originalTitle { get; set; }
        public string originalExtension { get; set; }

        public Song(int number, string originalTitle, string originalExtension)
        {
            this.number = number;
            this.originalExtension = originalExtension;
            this.originalTitle = originalTitle;
        }

        public Song()
        {

        }
    }
}
