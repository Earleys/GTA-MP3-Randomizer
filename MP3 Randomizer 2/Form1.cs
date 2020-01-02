using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using System.Diagnostics;
using System.Security.Principal;
using MP3Randomizer2.Core;
using MP3Randomizer2.Core.Entities;

namespace MP3_Randomizer_2
{
    public partial class Form1 : Form
    {
        SongRandomizer songRandomizer = new SongRandomizer();

        private int previousResult = 0; // count of all songs (if this number changes during runtime, songs were potentially added/removed)
        private ShuffleModes shuffleMode;
        private int totalRenamed = 0; // amount of songs in list
                                      // private int renamed = 0; // amount of songs that were renamed 
        private string currentGame = "";
        private string path = "";
        private string status = ""; // label that shows the status of the program

        private bool gameDetected = false;
        private bool checkReset = false; // checkbox value
        private bool containsNumbers = false; // if any of the songs contain a number (invalid symbol)
        private bool firstLaunch = false;
        private bool possibleLaunchIssue = false;
        private decimal percentage = 0;

        private double elapsedTime = 0;
        private int seconds = 0;

        bool isReordered = false;
        bool hasReorderingFinished = true;
        bool loadingError = false;

        List<Song> originalFileNameList = new List<Song>(); // contains all songs that are renamed (Song object)
        List<GameVersion> versionList = new List<GameVersion>(); // Contains data about EVERY game (III, VC, SA)
        GameVersionDetector gvd = new GameVersionDetector();

        Thread threadUpdateUI; // updates the UI labels
        Thread threadGameDetector; // detects the game process
        Thread threadRename; // Renames the songs

        // NO OFFSETS (!) - Steam version of SA is mostly incorrect
        private int iiiFirstMissionMemAddress = 0x0075BA20; // 0x0074B8E0
        private int vcFirstMissionMemAddress = 0x008215F0;
        private int sa1FirstMissionMemAddress = 0x008EAFCC; // regular version (gta-sa in task manager)
        private int sa2FirstMissionMemAddress = 0x008EAFCC; // steam version (gta_sa in task manager) // this is not correct
        private int sa3FirstMissionMemAddress = 0x008DF778; // downgraded sa version (gta-sa in task manager)

        public Form1()
        {
            InitializeComponent();

            try
            {
                firstLaunch = true;
                previousResult = originalFileNameList.Count();
                enableButtons();
                this.cmbShuffleMode.SelectedIndex = 0;
                this.shuffleMode = (ShuffleModes)cmbShuffleMode.SelectedIndex;

                txtPath.Text = MP3_Randomizer_2.Properties.Settings.Default.Path;
                path = MP3_Randomizer_2.Properties.Settings.Default.Path;
                shuffleMode = (ShuffleModes)MP3_Randomizer_2.Properties.Settings.Default.ShuffleMode;
                cmbShuffleMode.SelectedIndex = MP3_Randomizer_2.Properties.Settings.Default.ShuffleMode;
                checkReset = MP3_Randomizer_2.Properties.Settings.Default.shouldAutoReset;
                chkautoReOrder.Checked = checkReset;

                if (!checkReset)
                {
                    btnRearrangeNow.Enabled = true;
                    btnResetDisable.Enabled = true;
                }
                else
                {
                    btnRearrangeNow.Enabled = false;
                    btnResetDisable.Enabled = false;
                }


                threadUpdateUI = new Thread(new ThreadStart(updateUI));
                threadUpdateUI.Start();

                threadGameDetector = new Thread(new ThreadStart(detectGame));
                threadGameDetector.Start();

                threadRename = new Thread(new ThreadStart(RenameFiles));

                if (path != "")
                {
                    FolderBrowserDialog fbd = new FolderBrowserDialog();
                    fbd.SelectedPath = path;
                    updateToNewPath(fbd);
                    lblMp3Files.ForeColor = Color.Black;
                    loadingError = false;
                }

            }
            catch (IOException io)
            {
                status = "Unable to read or write to path, is the path accessible?";
                lblMp3Files.ForeColor = Color.Red;
                loadingError = true;
                Console.WriteLine(io.Message);
                //MessageBox.Show("An error occured when trying to launch the application. Unable to read or write to path. Is the path accessible?", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occured when trying to launch the application.\r\n " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.WriteLine(ex.Message);
            }

        }

        bool IsAllDigits(string s)
        {
            foreach (char c in s)
            {
                if (!char.IsDigit(c))
                    return false;
            }
            return true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.Description = "Select the MP3 folder of the GTA game you want to play.";

            if (path != "")
            {
                fbd.SelectedPath = path;
            }

            DialogResult result = fbd.ShowDialog();

            if (result != System.Windows.Forms.DialogResult.Cancel)
            {
                RecoverFileName();
                updateToNewPath(fbd);
            }

            /* if (!loadingError)
             {
                 if (elapsedTime > 999)
                 {
                     seconds = Convert.ToInt32(elapsedTime / 1000);
                     status = "Finished loading in " + elapsedTime + " seconds!";
                 }
                 else
                 {
                     status = "Finished loading in " + elapsedTime + " ms!";
                 }

             }*/

        } // path change

        /// <summary>
        /// Resets all loaded songs, and reloads them.
        /// </summary>
        /// <param name="fbd"></param>
        private void updateToNewPath(FolderBrowserDialog fbd)
        {

            var watch = Stopwatch.StartNew();
            try
            {
                containsNumbers = false;

                if (isRoot(fbd.SelectedPath) && !IsUserAdministrator())
                {
                    MessageBox.Show("Warning! You have selected a directory that is known to cause issues without Admin rights. Please consider choosing another path or starting the application as Administrator", "Error when selecting path", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                elapsedTime = 0;
                originalFileNameList.Clear();
                lblRenamed.Text = "0";
                lblMp3Files.Text = "0";
                status = "Please wait while song files are being processed...";
                txtPath.Text = fbd.SelectedPath;
                path = fbd.SelectedPath;
                string[] files = Directory.GetFiles(fbd.SelectedPath);
                foreach (var s in files) // for every song in file directory
                {
                    if (Path.GetExtension(s) == ".mp3" || Path.GetExtension(s) == ".lnk")
                    {
                        if (!IsAllDigits(Path.GetFileNameWithoutExtension(s))) // to prevent invalid symbols
                        {
                            Song song = new Song(0, Path.GetFileName(s), Path.GetExtension(s));
                            originalFileNameList.Add(song);
                        }
                        else
                        {
                            containsNumbers = true;
                            MessageBox.Show("Due to how this program works, your songs cannot be named with numbers only. Please (re)move or rename file '" + Path.GetFileName(s) + "' from  the folder. You can right click the program and view the File Details to remove all invalid files at once. Loading files has been cancelled.", "Cannot load files", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            txtPath.Text = "";
                            status = "Loading files has been aborted.";
                            originalFileNameList.Clear();
                            isReordered = false;
                            break;
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                lblMp3Files.ForeColor = Color.Red;
                loadingError = true;
                Console.WriteLine(ex.Message);
            }

            watch.Stop();
            elapsedTime = watch.ElapsedMilliseconds;
            MP3_Randomizer_2.Properties.Settings.Default.Path = path;
            MP3_Randomizer_2.Properties.Settings.Default.Save();
        }

        /// <summary>
        /// Handles UI related stuff. Updates text fields, and changes font sizes if necessary.
        /// </summary>
        public void updateUI()
        {
            try
            {
                while (true)
                {
                    Thread.Sleep(100);
                    if (originalFileNameList.Count != previousResult)
                    {
                        previousResult = originalFileNameList.Count();
                        if (IsHandleCreated)
                        {
                            this.Invoke((MethodInvoker)delegate
                            {
                                lblMp3Files.Text = originalFileNameList.Count.ToString();
                            });
                        }


                    }


                    if (IsHandleCreated)
                    {
                        this.Invoke((MethodInvoker)delegate
                        {

                            lblStatus.Text = status;
                            //  lblRenamed.Text = renamed.ToString();
                            lblCurrentGame.Text = currentGame;

                            lblRenamed.Text = totalRenamed.ToString();

                            if (percentage <= 0 || percentage >= 100)
                            {
                                prgProgress.Visible = false;
                            }
                            else
                            {
                                prgProgress.Visible = true;
                                prgProgress.Value = Convert.ToInt32(percentage);
                            }

                            if (originalFileNameList.Count < totalRenamed)
                            {
                                lblRenamed.Text = "N/A";
                            }

                            if (gameDetected && checkReset)
                            {
                                lblCurrentGame.ForeColor = Color.DarkGreen;
                            }
                            else if (!gameDetected && checkReset)
                            {
                                lblCurrentGame.ForeColor = Color.DarkRed;
                            }
                            else
                            {
                                lblCurrentGame.ForeColor = Color.DarkOrange;
                            }

                            if (status.Length > 80)
                            {
                                lblStatus.Font = new Font("Microsoft Sans Serif", 10, FontStyle.Bold);
                            }
                            else if (status.Length <= 80)
                            {
                                lblStatus.Font = new Font("Microsoft Sans Serif", 11, FontStyle.Bold);
                            }
                        });

                    }

                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }


        } // thread

        public void detectGame()
        {
            while (true)
            {
                versionList.Clear();
                Process processIII = Process.GetProcessesByName("gta3").FirstOrDefault();
                Process processVC = Process.GetProcessesByName("gta-vc").FirstOrDefault();
                Process processSA1 = Process.GetProcessesByName("gta_sa").FirstOrDefault();
                Process processSA2 = Process.GetProcessesByName("gta-sa").FirstOrDefault();

                GameVersion gtaiiiVersion = new GameVersion(processIII, GameVersionDetector.getGameInformation(processIII), iiiFirstMissionMemAddress);
                versionList.Add(gtaiiiVersion);
                GameVersion gtavcVersion = new GameVersion(processVC, GameVersionDetector.getGameInformation(processVC), vcFirstMissionMemAddress);
                versionList.Add(gtavcVersion);
                GameVersion gtasa1Version = new GameVersion(processSA1, GameVersionDetector.getGameInformation(processSA1), sa1FirstMissionMemAddress);
                versionList.Add(gtasa1Version);
                GameVersion gtasa2Version = new GameVersion(processSA2, GameVersionDetector.getGameInformation(processSA2), sa2FirstMissionMemAddress);
                versionList.Add(gtasa2Version);
                GameVersion gtasa3Version = new GameVersion(processSA1, GameVersionDetector.getGameInformation(processSA1), sa3FirstMissionMemAddress);
                versionList.Add(gtasa1Version);

                GameVersion cg = new GameVersion();

                if (firstLaunch && IsHandleCreated)
                {
                    foreach (var game in versionList)
                    {
                        if (GameVersionDetector.isProcessActive(game.Process))
                        {
                            status = "Please restart the game (recommended), or click 'OK' if you want to continue anyway.";
                            possibleLaunchIssue = true;
                            cg = game;
                            this.Invoke((MethodInvoker)delegate
                            {
                                btnOk.Visible = true;
                            });
                            break;
                        }

                    }
                }


                foreach (var game in versionList) // loops through each game
                {
                    if (gvd.DetectCurrentVersion(game) && GameVersionDetector.isProcessActive(game.Process)) // if the game is running
                    {
                        gameDetected = true; // if the code gets to this point, this means the game has been detected.

                        currentGame = game.GameName;

                        if (gameDetected && path == "") // if the game is detected, but the path is still empty, then prompt the user to select a path before starting the game (safety feature)
                        {
                            status = "Please close the game and try choosing a path first!";
                            isReordered = false;
                        }
                        else if (gameDetected && path != "" && checkReset && !possibleLaunchIssue) // if the game is detected, a (valid) path was entered, and the songs need to be checked for a reset
                        {
                            if (Memory.GetMemoryResult(game.Process, game.MemAddress + game.Offset, 4) == 0 && !isReordered) // if the memaddress (first mission) is 0 (not done), and the songs have not been ordered
                            {
                                isReordered = true;
                                hasReorderingFinished = false; // this boolean is being changed in Rename thread

                                try
                                {
                                    if (!threadRename.IsAlive && path != "")    // if thread is not running, and path is not empty
                                    {
                                        threadRename.Start(); // start the 'rename'-thread
                                    }
                                }

                                catch (Exception)
                                {
                                    Console.WriteLine("Thread RENAME is already running!!");
                                }


                                if (hasReorderingFinished)
                                {
                                    if (elapsedTime > 999)
                                    {
                                        seconds = Convert.ToInt32(elapsedTime / 1000);
                                        status = "Finished in " + seconds + " seconds!\r\nSongs don't need another reset right now.";
                                    }
                                    else
                                    {
                                        status = "Finished in " + elapsedTime + " ms!\r\nSongs don't need another reset right now.";
                                    }
                                }


                                //  hasResetFinished = true;
                                // }
                            }
                            else if (Memory.GetMemoryResult(game.Process, game.MemAddress + game.Offset, 4) == 1 && checkReset) // if the memaddress (first mission) is 1 (done), and the songs should be checked for a reset
                            {
                                if (hasReorderingFinished && gameDetected && path != "" && !loadingError) // if reodering is (still) finished, game is detected, path is valid and no errors were found
                                {
                                    status = "Waiting for another reset..."; // just let the user know
                                }

                                isReordered = false;
                            }
                            else if (Memory.GetMemoryResult(game.Process, game.MemAddress + game.Offset, 4) == 0) // if the memaddress (first mission) is 0 (not done)
                            {

                                if (hasReorderingFinished && gameDetected && !loadingError) // if reordering is finished, game is detected, and no loading errors
                                {
                                    if (elapsedTime > 999)
                                    {
                                        seconds = Convert.ToInt32(elapsedTime / 1000);
                                        status = "Finished in " + seconds + " seconds!\r\nSongs don't need another reset right now.";
                                    }
                                    else
                                    {
                                        status = "Finished in " + elapsedTime + " ms!\r\nSongs don't need another reset right now.";
                                    }
                                }
                            }
                            else
                            {

                            }
                            //  hasResetFinished = true;
                        }
                        break; // no need to check for any remaining games in the loop
                    }
                    else // if the game is not running
                    {
                        if (firstLaunch && checkReset)
                        {
                            if (!GameVersionDetector.isProcessActive(cg.Process) && IsHandleCreated)
                            {
                                firstLaunch = false;
                                possibleLaunchIssue = false;
                                this.Invoke((MethodInvoker)delegate
                                {
                                    btnOk.Visible = false;
                                });
                            }
                        }
                        if (checkReset) // if the game should be checked
                        {
                            currentGame = "Game is not supported or running!"; // let the user know
                        }

                        if (path == "" && hasReorderingFinished) // if path is empty
                        {
                            status = "Please select a path to your MP3 player using the button above."; // tell the user to select a path
                            if (!checkReset) // if reset should not be checked
                            {
                                currentGame = "Not checking current game."; // let the user know
                            }
                            // hasResetFinished = false;
                        }
                        else if (!checkReset && !loadingError) // if reset should not be checked, and no errors occured
                        {
                            if (elapsedTime > 999)
                            {
                                seconds = Convert.ToInt32(elapsedTime / 1000);
                                status = "Finished in " + seconds + " seconds!\r\nThe auto-reset feature is disabled.\r\n Use the 'Quick settings' instead.";
                            }
                            else
                            {
                                status = "Finished in " + elapsedTime + " ms!\r\nThe auto-reset feature is disabled.\r\n Use the 'Quick settings' instead."; // let the user know
                            }
                            this.Invoke((MethodInvoker)delegate
                            {
                                enableButtons();
                            });
                        }

                        if (hasReorderingFinished && path != "" && checkReset && !loadingError && !possibleLaunchIssue && !gameDetected) // if reodering is finished, path is valid, resets should be checked, and no errors occured
                        {
                            if (elapsedTime > 999)
                            {
                                seconds = Convert.ToInt32(elapsedTime / 1000);
                                status = "Finished in " + seconds + " seconds!\r\n Please launch a supported GTA game."; // let the user know
                            }
                            else
                            {
                                status = "Finished in " + elapsedTime + " ms!\r\n Please launch a supported GTA game."; // let the user know
                            }

                            // hasResetFinished = false;
                        }


                        gameDetected = false;
                        // hasResetFinished = false;
                    }
                }

                try
                {
                    if (!gameDetected) // if game is not detected
                    {
                        /*  if (hasReorderingFinished)
                          {
                              status = "Finished!\r\nPlease launch a supported GTA game.";
                          }*/
                        this.Invoke((MethodInvoker)delegate
                        {
                            enableButtons();  // enable all buttons
                        });
                    }

                }
                catch (Exception)
                { }

                Thread.Sleep(1220);
            }


        }

        /// <summary>
        /// Blocks all buttons that might cause issues if they remain enabled.
        /// </summary>
        private void blockButtonsDuringRuntime()
        {
            txtPath.Enabled = false;
            //hasResetFinished = true;
            cmbShuffleMode.Enabled = false;
            btnRearrangeNow.Enabled = false;
            btnResetDisable.Enabled = false;
            btnPath.Enabled = false;
        }

        /// <summary>
        /// Enable all buttons that were blocked.
        /// </summary>
        private void enableButtons()
        {
            //txtPath.Enabled = true;
            btnPath.Enabled = true;
            cmbShuffleMode.Enabled = true;
            btnResetDisable.Enabled = true;
            btnRearrangeNow.Enabled = true;
        }

        /// <summary>
        /// This method will recover the filenames.
        /// </summary>
        /// <returns></returns>
        public bool RecoverFileName()
        {
            bool result = false;
            hasReorderingFinished = false;
            totalRenamed = 0;
            status = "Recovering songs...";

            if (songRandomizer.RecoverSongs(path, originalFileNameList))
            {
                status = "Songs were recovered!";
                result = true;
            }
            else
            {
                status = "Songs could not be recovered";
                result = false;
            }

            hasReorderingFinished = true;
            return result;
        }

        /// <summary>
        /// Thread that will rename the files.
        /// </summary>
        public void RenameFiles()
        {
            /* I made up these algorithms myself, so they might not be the most optimal, but they get the job done.
                GTA Vice City, and possibly III load the file names the first time you load the game.*
                It doesn't link the file name to any song, but you can't just randomly choose a bunch of numbers/names and expect it to work.
                If the number you chose didn't exist the first time you loaded a new game, it will never play that song.
                If the number DID exist, it will start playing these from the top (in order).
                So... these shuffle modes are made with this idea in mind.
                * San Andreas allows you to rescan the songs whenever you want, so it might be less of a problem for SA.
            */

            Task.Factory.StartNew(() =>
            {
                lock (threadRename) // keeps everything synchronized, since it's threaded
                {

                    while (true)
                    {
                        // hasReorderingFinished = false;
                        try
                        {
                            if (!hasReorderingFinished)
                            {
                                elapsedTime = 0;
                                seconds = 0;
                                var watch = Stopwatch.StartNew();
                                Random r = new Random();

                                totalRenamed = 0;
                                if (RecoverFileName()) // if files are recovered successfully
                                {
                                    status = "Renaming songs...";
                                    RandomizedSong rs = new RandomizedSong();
                                    switch (shuffleMode)
                                    {
                                        case ShuffleModes.randomizeEverything:
                                            rs = songRandomizer.RandomizeEverything(path, originalFileNameList);
                                            break;
                                        case ShuffleModes.randomizeFromRandomPoint:
                                            rs = songRandomizer.RandomizeFromFixedStart(path, originalFileNameList);
                                            break;
                                        case ShuffleModes.randomizeFirstSongOnly:
                                            rs = songRandomizer.OnlyChangeFirstSong(path, originalFileNameList);
                                            break;
                                        case ShuffleModes.randomizeEverythingButFirstSong:
                                            rs = songRandomizer.RandomizeEverythingButFirstSong(path, originalFileNameList);
                                            break;
                                        default:
                                            rs.totalRenamed = 0;
                                            break;
                                    }
                                    totalRenamed = rs.totalRenamed;
                                    hasReorderingFinished = true;
                                }
                                else
                                {
                                    //return 0;
                                }

                                watch.Stop();
                                elapsedTime = watch.ElapsedMilliseconds;
                            }
                            hasReorderingFinished = true;
                        }

                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.StackTrace);
                            // return 0;
                        }
                        //isReordered = true;
                        hasReorderingFinished = true;
                        Thread.Sleep(2010);
                    }

                }
            });

        }

        private void cmbShuffleMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            shuffleMode = (ShuffleModes)this.cmbShuffleMode.SelectedIndex;
            MP3_Randomizer_2.Properties.Settings.Default.ShuffleMode = (int)shuffleMode;
            MP3_Randomizer_2.Properties.Settings.Default.Save();
        }

        /// <summary>
        /// This folder will check if the path is in a folder that might cause issues without admin rights.
        /// Checks for: My Documents, My Computer, Program Files (X86), Personal folder
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public bool isRoot(string p)
        {
            try
            {
                DirectoryInfo d = new DirectoryInfo(p);
                if (p == Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) || p == Environment.GetFolderPath(Environment.SpecialFolder.MyComputer) || p == Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86) || p == Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles) || p == Environment.GetFolderPath(Environment.SpecialFolder.Personal))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }

        }

        public bool IsUserAdministrator()
        {
            //bool value to hold our return value
            bool isAdmin;
            WindowsIdentity user = null;
            try
            {
                //get the currently logged in user
                user = WindowsIdentity.GetCurrent();
                WindowsPrincipal principal = new WindowsPrincipal(user);
                isAdmin = principal.IsInRole(WindowsBuiltInRole.Administrator);
            }
            catch (UnauthorizedAccessException ex)
            {
                isAdmin = false;
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                isAdmin = false;
                Console.WriteLine(ex.Message);
            }
            finally
            {
                if (user != null)
                    user.Dispose();
            }
            return isAdmin;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {

                if (!hasReorderingFinished) // safety feature
                {
                    DialogResult dr = MessageBox.Show("Songs are still being re-ordered. If you close the application now, some songs might not receive their original file name again. Are you sure you want to close the MP3 Randomizer? (It might be finished by now)", "Closing MP3 Randomizer", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (dr == DialogResult.Yes)
                    {
                        RecoverFileName();
                    }
                    else
                    {
                        e.Cancel = true;
                    }
                }

                RecoverFileName();

            }
            catch (Exception)
            {
                DialogResult dr = MessageBox.Show("Something went wrong when trying to recover your song names. Closing the application might cause certain songs to not be renamed properly. Are you sure you want to exit?", "Error when closing application", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                if (dr == DialogResult.Yes)
                {
                    threadRename.Abort();
                    threadGameDetector.Abort();
                    threadRename.Abort();
                    Application.Exit();
                }
            }
            threadRename.Abort();
            threadGameDetector.Abort();
            threadRename.Abort();
        }

        private void btnRearrangeNow_Click(object sender, EventArgs e)
        {
            isReordered = true;
            hasReorderingFinished = false;
            blockButtonsDuringRuntime();
            RenameFiles();
            try
            {
                if (!threadRename.IsAlive)
                {
                    threadRename.Start();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void chkautoReOrder_CheckedChanged(object sender, EventArgs e)
        {
            checkReset = chkautoReOrder.Checked;
            MP3_Randomizer_2.Properties.Settings.Default.shouldAutoReset = checkReset;
            MP3_Randomizer_2.Properties.Settings.Default.Save();
            if (!checkReset)
            {
                btnRearrangeNow.Enabled = true;
                btnResetDisable.Enabled = true;
            }
            else
            {
                btnRearrangeNow.Enabled = false;
                btnResetDisable.Enabled = false;
            }
        }

        private void testToolStripMenuItem_Click(object sender, EventArgs e)
        {
            path = "";
            MP3_Randomizer_2.Properties.Settings.Default.Path = path;
            MP3_Randomizer_2.Properties.Settings.Default.Save();
        }

        private void btnResetDisable_Click(object sender, EventArgs e)
        {
            try
            {
                RecoverFileName();
                isReordered = false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                Update update = MP3Randomizer2.Core.UpdateChecker.CheckUpdate();
                Version applicationVersion = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;

                if (applicationVersion.CompareTo(update.Mandatory) < 0)
                {

                    DialogResult dr = MessageBox.Show("A mandatory update is required!\r\nYou are currently using version " + applicationVersion + ", and you will have to upgrade to version " + update.Mandatory + " before you can use this application again.\r\n\r\nChangelog:\r\n" + update.About + "\r\n\r\nWould you like to update now? This will be downloaded using your default browser.", "Version " + update.Version + " is available!", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (dr == DialogResult.Yes)
                    {
                        try
                        {
                            System.Diagnostics.Process.Start(update.Url);
                        }
                        catch (Exception ex)
                        { MessageBox.Show("The update could not be downloaded. Please try again later.\r\n" + ex.Message, "Error when downloading update", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                        // return;
                    }
                    else
                    {
                        MessageBox.Show("This is a mandatory update and you will have to update before you can use this application again.", "Update cancelled", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        Application.Exit();
                    }
                }
                else if (applicationVersion.CompareTo(update.Version) < 0)
                {
                    DialogResult dr = MessageBox.Show("There's an update available for this application!\r\nYou are currently using version " + applicationVersion + ", and can now upgrade to version " + update.Version + ".\r\n\r\nChangelog:\r\n" + update.About + "\r\n\r\nWould you like to update now? This will be downloaded using your default browser.", "Version " + update.Version + " is available!", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (dr == DialogResult.Yes)
                    {
                        try
                        {
                            System.Diagnostics.Process.Start(update.Url);
                        }
                        catch (Exception ex)
                        {
                            DialogResult dr2 = MessageBox.Show("The update could not be downloaded. Please try again later.\r\n" + ex.Message, "Error when downloading update", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        // return;
                    }
                }
                else if (applicationVersion.CompareTo(update.Version) >= 0)
                {
                    MessageBox.Show("No update has been found. You are using the latest version!", "GTA MP3 Randomizer", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
            catch (Exception)
            {


            }
        }

        private void btnForceReload_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dr = MessageBox.Show("Forcing a reload will recover all songs in the selected path and reload them.  If you do this when a game is running, it might cause the MP3 player to stop working. Are you sure you want to continue?", "Forcing reload", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    RecoverFileName();
                    FolderBrowserDialog fbd = new FolderBrowserDialog();
                    fbd.SelectedPath = path;
                    updateToNewPath(fbd);
                    lblMp3Files.ForeColor = Color.Black;
                    loadingError = false;
                }
            }
            catch (IOException)
            {
                lblMp3Files.ForeColor = Color.Red;
                loadingError = true;
                MessageBox.Show("An error occured when trying to reload the songs. Unable to read or write to path. Is the path accessible?", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occured when trying to reload the songs.\r\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void lblRenamed_Click(object sender, EventArgs e)
        {
            /*FileViewer fv = new FileViewer(path);
            fv.Show();*/
        }

        private void rightClick_Opening(object sender, CancelEventArgs e)
        {

        }

        private void viewFileDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FileViewer fv = new FileViewer(path, isReordered);
            fv.Show();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                "© 2015-2020\r\nMade by Earleys, first tested by Pitp0.\r\n\r\nMessage me on #GTA, Twitch or Discord if you need help/want things added.\r\n\r\nKnown so far:\r\n- No SA Steam version works properly, except for the default 1.0 version.\r\n- Game might not be recognized with a renamed .exe file (not really a bug, but useful to know)",
                "About");

        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            btnOk.Visible = false;
            if (true)
            {
                firstLaunch = false;
                possibleLaunchIssue = false;
            }
        }
    }
}
