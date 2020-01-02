using MP3Randomizer2.Core.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MP3Randomizer2.Core
{
    public class SongRandomizer
    {
        Random r = new Random();
        RandomizedSong rs;
        public static int progress = 0;

        /// <summary>
        ///  This mode will start by generating a random number (with a max, depending on the amount of *.mp3 and *.lnk files.)
        /// After that it will compare this number with the existing files in the folder.
        /// If the file name happens to exist already, it will repeat the process, until it finds an unique ID. - nothing is renamed at this point.
        /// (This seems to be a slow process, but in this new version it can process over 300 songs in less than 1 second!)
        /// When a song passes(so the chosen number doesn't exist yet), it will then try to link the original song, to the currently 'selected' song.
        /// It will create a variable which will contain the chosen number, followed by the correct file extension (e.g.Mamma Mia - Abba.mp3 to 11.mp3)
        /// Once that's all finished, it will (finally) replace the song title to the random ID. After that it will repeat the process,
        /// until all songs are processed.
        /// </summary>
        /// <param name="path">The path to the MP3 player of the user</param>
        /// <param name="songList">The list with the original songs</param>
        /// <returns></returns>
        public RandomizedSong RandomizeEverything(string path, List<Song> songList)
        {
            rs = new RandomizedSong();
            bool repeat = true;
            int randomNumber = 0;
            int totalNumberOfSongs = GetTotalNumberOfSongs(path);

            foreach (var song in songList)
            {

                foreach (FileInfo file in GetFileArrayFiles(path))
                {
                    while (repeat) // This will be true as long as the chosen number isn't unique
                    {
                        randomNumber = r.Next(totalNumberOfSongs);
                        randomNumber += 1;
                        string randomNr = randomNumber + ".mp3";
                        string randomLnk = randomNumber + ".lnk";

                        foreach (var item in GetFileArrayFiles(path)) // re-loop through the folder after every 'repeat'
                        {
                            if (item.Extension == ".mp3" || item.Extension == ".lnk")
                            {
                                if (randomNr != item.Name && randomLnk != item.Name)
                                {
                                    // The chosen number is unique so far :D!
                                    repeat = false;
                                }
                                else
                                { // The number it chose already exists; Break out of the loop so we don't overwrite the value.
                                    repeat = true;
                                    break;
                                }
                            }
                            else
                            {
                                repeat = false;
                            }

                        }
                    }
                    // NEXT STEP:
                    if (file.Extension == song.originalExtension && file.Name == song.number.ToString() + song.originalExtension || file.Extension == song.originalExtension && file.Name == song.originalTitle)
                    {
                        int originalNumber = song.number;
                        string changedFileName = randomNumber.ToString() + song.originalExtension;
                        song.number = randomNumber;

                        try
                        {
                            File.Move(file.FullName, file.FullName.ToString().Replace(file.Name, changedFileName));
                            rs.totalAmount++;
                            rs.totalRenamed++;
                        }
                        catch (IOException ioe)
                        {
                            Console.WriteLine(ioe.Message);
                        }


                        FileInfo[] fileArray = GetFileArrayFiles(path);
                        repeat = true;
                    }

                }
            }
            return rs;
        }


        /// <summary>
        ///  This shuffle mode is kinda hard to explain, so there's an example further down.
        ///It will start by choosing a random song to start playing from.
        ///After that it will loop through the songs, and the folder itself, and start by renaming the randomly chosen song to 1.
        ///
        /// Once the starting point is set, it will rename all songs (starting from the 'starting point'), to 2, 3, 4, ... respectively.
        /// There's a check to see if it reaches the end of the folder too. In that case, it will attempt to continue renaming from the top.
        /// An example that might make it a bit easier to understand:
        /// Imagine you have a folder with the following file names: a, b, c, d, e, f.
        /// The program will first choose a random starting point, let's say it chooses c. 
        /// It will then rename c to 1, and continue from there, so: d becomes 2, e becomes 3, f becomes 4...
        /// Now it reached the end of the folder, but a and b are still left.That's where the additional check comes in.
        /// It will start from the top again, and rename a and b to 5 and 6 respectively.
        /// </summary>
        /// <param name="path">The path to the MP3 player of the user</param>
        /// <param name="songList">The list with the original songs</param>
        /// <returns></returns>
        public RandomizedSong RandomizeFromFixedStart(string path, List<Song> songList)
        {
            rs = new RandomizedSong();
            bool isStartNumberSet = false;
            int startNumber = r.Next(GetTotalNumberOfSongs(path));
            string convertedStartNumber = startNumber + ".mp3";

            int songsInMemoryPosition = 0;
            int toBeRenamedInto = songsInMemoryPosition + 1; // starts at 1, just like the other shuffle modes
            int folderPosition = startNumber; // value that looks in the folder
            bool hasReset = false;

            FileInfo[] fileArray = GetFileArrayFiles(path);

            for (int i = 0; i < fileArray.Length; i++)
            {

                if (folderPosition >= fileArray.Length)
                {
                    // if end of folder is reached
                    folderPosition = 0;
                    hasReset = true;
                }

                string changedFileName = songsInMemoryPosition.ToString();

                foreach (var item in songList)
                {
                    foreach (FileInfo file in fileArray)
                    {

                        if (!isStartNumberSet)
                        {
                            if (item.originalTitle == fileArray[folderPosition].Name && file.Extension == ".mp3" || item.originalTitle == fileArray[folderPosition].Name && file.Extension == ".lnk")
                            {
                                File.Move(fileArray[folderPosition].FullName, fileArray[folderPosition].FullName.ToString().Replace(fileArray[folderPosition].Name, toBeRenamedInto + item.originalExtension));

                                //song.number = startAt;
                                isStartNumberSet = true;
                                item.number = toBeRenamedInto;
                                toBeRenamedInto++;
                                break;
                            }
                            else if (fileArray[folderPosition].Extension != ".mp3" && fileArray[folderPosition].Extension != ".lnk")
                            {
                                Console.WriteLine("first");
                                folderPosition++;


                            }
                        }
                        else // starting point has been selected
                        {

                            if (!hasReset) // if it hasn't reached the end of the folder yet...
                            {
                                if (item.originalTitle == fileArray[folderPosition].Name && file.Extension == ".mp3" || item.originalTitle == fileArray[folderPosition].Name && file.Extension == ".lnk")
                                {
                                    Console.WriteLine("Item (not@end) at: " + folderPosition + ": " + fileArray[folderPosition].Name.ToString() + " | i: " + i + ": " + fileArray[i].Name.ToString());

                                    if (File.Exists(path + @"\" + folderPosition + ".mp3") || File.Exists(path + @"\" + folderPosition + ".lnk"))
                                    {
                                        int temp = toBeRenamedInto;
                                        // item.number = temp;
                                        File.Move(fileArray[folderPosition].FullName, fileArray[folderPosition].FullName.ToString().Replace(fileArray[folderPosition].Name, temp + item.originalExtension));
                                        item.number = temp;
                                        toBeRenamedInto++;

                                    }
                                    else
                                    {
                                        File.Move(fileArray[folderPosition].FullName, fileArray[folderPosition].FullName.ToString().Replace(fileArray[folderPosition].Name, toBeRenamedInto + item.originalExtension));
                                        item.number = toBeRenamedInto;
                                        toBeRenamedInto++;
                                    }
                                    //  Thread.Sleep(20);

                                    break;
                                }
                                else if (fileArray[folderPosition].Extension != ".mp3" && fileArray[folderPosition].Extension != ".lnk")
                                {
                                    Console.WriteLine("second");
                                    folderPosition++;
                                }
                            }
                            else // if it has reached the end of the folder
                            {
                                if (item.originalTitle == fileArray[songsInMemoryPosition].Name && file.Extension == ".mp3" || item.originalTitle == fileArray[songsInMemoryPosition].Name && file.Extension == ".lnk")
                                {
                                    Console.WriteLine("Item at: " + songsInMemoryPosition + ": " + fileArray[songsInMemoryPosition].Name.ToString() + " | i: " + i + ": " + fileArray[i].Name.ToString());

                                    if (File.Exists(path + @"\" + songsInMemoryPosition + ".mp3") || File.Exists(path + @"\" + songsInMemoryPosition + ".lnk"))
                                    {
                                        int temp = toBeRenamedInto;
                                        //  item.number = temp;
                                        File.Move(fileArray[songsInMemoryPosition].FullName, fileArray[songsInMemoryPosition].FullName.ToString().Replace(fileArray[songsInMemoryPosition].Name, toBeRenamedInto + item.originalExtension));
                                        item.number = toBeRenamedInto;
                                        toBeRenamedInto++;
                                    }
                                    else
                                    {
                                        Console.WriteLine("Renaming into " + songsInMemoryPosition);
                                        File.Move(fileArray[songsInMemoryPosition].FullName, fileArray[songsInMemoryPosition].FullName.ToString().Replace(fileArray[songsInMemoryPosition].Name, toBeRenamedInto + item.originalExtension));
                                        item.number = toBeRenamedInto;
                                        toBeRenamedInto++;
                                    }

                                    break;
                                }
                                else if (fileArray[songsInMemoryPosition].Extension != ".mp3" && fileArray[songsInMemoryPosition].Extension != ".lnk")
                                {
                                    Console.WriteLine("third");
                                    break;
                                }
                            }

                        }
                    }

                }


                rs.totalRenamed = toBeRenamedInto - 1; // for the GUI
                songsInMemoryPosition++;
                folderPosition++;

                fileArray = GetFileArrayFiles(path);
            }
            return rs;
        }

        /// <summary>
        /// This will choose a random song that should be put at the top, it will rename that song to 1 ( + extension), and leave the rest as-is.
        /// This process repeats everytime you reset. That way the first song - and only the first song - will always be different.
        /// </summary>
        /// <param name="path">The path to the MP3 player of the user</param>
        /// <param name="songList">The list with the original songs</param>
        /// <returns></returns>
        public RandomizedSong OnlyChangeFirstSong(string path, List<Song> songList)
        {
            rs = new RandomizedSong();
            try
            {
                int startNumber = r.Next(GetTotalNumberOfSongs(path));
                while (GetFileArrayFiles(path)[startNumber].Extension.ToString() != ".mp3" && GetFileArrayFiles(path)[startNumber].Extension.ToString() != ".lnk")
                {
                    // repeat this loop until one of the chosen songs is an .mp3 or .lnk file.
                    startNumber = r.Next(GetTotalNumberOfSongs(path));
                }

                Song chosenSong = songList.Where(x => x.originalTitle == GetFileArrayFiles(path)[startNumber].Name).FirstOrDefault();
                chosenSong.number = 1;
                File.Move(GetFileArrayFiles(path)[startNumber].FullName, GetFileArrayFiles(path)[startNumber].FullName.ToString().Replace(GetFileArrayFiles(path)[startNumber].Name, "1.mp3"));
                rs.totalRenamed = 1;

                if (GetFileArrayFiles(path)[startNumber].Extension != ".mp3" || GetFileArrayFiles(path)[startNumber].Extension != ".lnk")
                {
                    // it should never get to this point
                    // return 0;
                }
                else
                {
                    rs.totalRenamed = 0;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in OnlyChangeFirstSong(): " + ex.Message);
            }
            return rs;
        }

        public RandomizedSong RandomizeEverythingButFirstSong(string path, List<Song> songList)
        {
            rs = new RandomizedSong();
            bool isFirstSongSet = false; // this way the program knows the first song is fixed/set
            bool repeat = true;
            int randomNumber = 0;

            foreach (var song in songList)
            {

                foreach (FileInfo file in GetFileArrayFiles(path))
                {
                    while (repeat) // This will be true as long as the chosen number isn't unique
                    {
                        randomNumber = r.Next(GetTotalNumberOfSongs(path));
                        if (!isFirstSongSet)
                        {
                            randomNumber = 0;
                            isFirstSongSet = true;
                        }
                        randomNumber += 1;
                        string randomNr = randomNumber + ".mp3";
                        string randomLnk = randomNumber + ".lnk";

                        foreach (var item in GetFileArrayFiles(path)) // re-loop through the folder after every 'repeat'
                        {
                            if (item.Extension == ".mp3" || item.Extension == ".lnk")
                            {
                                if (randomNr != item.Name && randomLnk != item.Name)
                                {
                                    // The chosen number is unique so far :D!
                                    repeat = false;
                                }
                                else
                                { // The number it chose already exists; Break out of the loop so we don't overwrite the value.
                                    repeat = true;
                                    break;
                                }
                            }
                            else
                            {
                                repeat = false;
                            }

                        }

                    }
                    // NEXT STEP:
                    if (file.Extension == song.originalExtension && file.Name == song.number.ToString() + song.originalExtension || file.Extension == song.originalExtension && file.Name == song.originalTitle)
                    {
                        int originalNumber = song.number;
                        string changedFileName = randomNumber.ToString() + song.originalExtension;
                        song.number = randomNumber;


                        try
                        {
                            File.Move(file.FullName, file.FullName.ToString().Replace(file.Name, changedFileName));
                            rs.totalAmount++;
                            rs.totalRenamed++;
                        }
                        catch (IOException ioe)
                        {

                            Console.WriteLine(ioe.Message);
                        }

                        repeat = true;
                    }
                }
            }
            return rs;
        }

        /// <summary>
        /// This will reset all filenames to their original state.
        /// This needs to be called after every reset and before re-ordering again, so we don't lose the original song names.
        /// </summary>
        /// <param name="path">The path to the MP3 player of the user</param>
        /// <param name="songList">The list with the original songs</param>
        public bool RecoverSongs(string path, List<Song> songList)
        {
            try
            {

                foreach (FileInfo file in GetFileArrayFiles(path)) // for every file
                {
                    foreach (var original in songList) // for every song in the list
                    {
                        string orig = original.number.ToString() + original.originalExtension; // make a string containing file name + extension

                        if (file.Name == orig) // if the file name (from directory) is equal to the original that was saved in the list

                        {
                            try
                            {
                                File.Move(file.FullName, file.FullName.ToString().Replace(file.Name, original.originalTitle)); // try to rename the file with the original file name
                                original.number = 0;
                                // renamed -= amount;
                                break; // no need to check remaining files, because there cannot be a match
                            }
                            catch (IOException ioe)
                            {
                                Console.WriteLine(ioe.Message);
                            }

                        }

                    }
                }
                return true;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        /// <summary>
        /// Gets total number of .MP3 and .LNK files and returns this value
        /// </summary>
        /// <param name="path">Path to the songs</param>
        /// <returns>Total number of MP3 and LNK files</returns>
        private int GetTotalNumberOfSongs(string path)
        {
            DirectoryInfo d = GetDirectoryInfo(path);

            int totalNumberOfSongs = d.GetFiles("*.mp3", SearchOption.AllDirectories).Length;
            int numLnk = d.GetFiles("*.lnk", SearchOption.AllDirectories).Length;
            totalNumberOfSongs += numLnk;
            return totalNumberOfSongs;
        }

        private DirectoryInfo GetDirectoryInfo(string path)
        {
            return new DirectoryInfo(path);
        }

        private FileInfo[] GetFileArrayFiles(string path)
        {
            DirectoryInfo d = new DirectoryInfo(path);
            return d.GetFiles();
        }

    }
}
