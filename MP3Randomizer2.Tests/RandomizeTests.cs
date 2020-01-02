using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MP3Randomizer2.Core;
using System.Collections.Generic;
using System.Linq;

namespace MP3Randomizer2.Tests
{
    [TestClass]
    public class RandomizeTests
    {
        SongRandomizer sr;
        List<Song> songList;

        [TestInitialize]
        public void Initialize()
        {
            sr = new SongRandomizer();
            songList = new List<Song>();
        }

        /// <summary>
        /// Tests if songs get randomized and renamed correctly
        /// </summary>
        [TestMethod]
        public void RandomizeEverythingTest()
        {
            // Arrange
            Song s1 = new Song { number = 0, originalExtension = ".mp3", originalTitle = "Song1" };
            songList.Add(s1);

            // Act
            sr.RandomizeEverything("", songList);

            // Assert
            Assert.AreEqual(1, songList.FirstOrDefault().number);
        }
    }
}
