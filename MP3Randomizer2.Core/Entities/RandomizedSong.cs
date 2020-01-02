using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MP3Randomizer2.Core.Entities
{
    public class RandomizedSong
    {
        public int totalAmount { get; set; }
        public int totalRenamed { get; set; }
        List<Song> songList { get; set; }

        public RandomizedSong(int totalAmount, int totalRenamed, List<Song> songList)
        {
            this.totalAmount = totalAmount;
            this.totalRenamed = totalRenamed;
            this.songList = songList;
        }

        public RandomizedSong()
        {

        }
    }
}
