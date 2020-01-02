using MP3Randomizer2.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace MP3Randomizer2.Core
{
    public class GameVersionDetector
    {

        public static GameVersion getGameInformation(Process process)
        {
            // this is based on Lighnat0r's GameVersionCheck script ( https://github.com/Lighnat0r/Files/blob/master/Lib/GameVersionCheck.ahk )
            GameVersion version;
            try
            {
                if (isProcessActive(process))
                {
                    if (process.ProcessName == "gta-vc")
                    {
                        byte value = (byte)Memory.GetMemoryResult(process, 0x00608578, 4);

                        if (value == 0x5D)
                        {
                           version = new GameVersion("GTA: Vice City (Retail 1.0)", 0); //1.0
                        }
                        else if (value == 0x81)
                        {
                            version = new GameVersion("GTA: Vice City (1.1)", 0x81); // 1.1
                        }
                        else if (value == 0x5B)
                        {
                            version = new GameVersion("GTA: Vice City (Steam)", -0xFF8); // steam
                        }
                        else if (value == 0x44)
                        {
                            version = new GameVersion("GTA: Vice City (Japanese)", -0x2FF8); // japanese
                        }
                        else
                        {
                            //     System.Windows.Forms.MessageBox.Show("Test");
                           version = new GameVersion("Gta: Vice City (Unknown)", 0);
                        }
                    }
                    else if (process.ProcessName == "gta-sa" || process.ProcessName == "gta_sa")
                    {
                        // SA is a mess
                        long value = Memory.GetMemoryResult(process, 0x0082457C, 4);
                        long value2 = Memory.GetMemoryResult(process, 0x008245BC, 4);
                        long value3 = Memory.GetMemoryResult(process, 0x008252FC, 4);
                        long value4 = Memory.GetMemoryResult(process, 0x0082533C, 4);
                        long value5 = Memory.GetMemoryResult(process, 0x0085EC4A, 4);
                        long value6 = Memory.GetMemoryResult(process, 0x0085DEDA, 4);
                        long value7 = Memory.GetMemoryResult(process, 0x0089BEEC, 4);

                        if (value == 0x94BF)
                        {
                            version = new GameVersion("GTA: San Andreas (1.0 US)", 0); // Version 1.0 US
                        }
                        else if (value2 == 0x94BF)
                        {
                            version = new GameVersion("GTA: San Andreas (1.0 EU/AUS/US/DG)", 0); // 1.0 EU/AUS or 1.0 US Hoodlum or 1.0 Downgraded
                        }
                        else if (value3 == 0x94BF)
                        {
                            version = new GameVersion("GTA: San Andreas (1.01 US)", 0x2680); // 1.01 US
                        }
                        else if (value4 == 0x94BF)
                        {
                            version = new GameVersion("GTA: San Andreas (1.01 EU/US/DEV/DG)", 0x2680); //Version 1.01 EU / AUS or 1.01 Deviance or 1.01 Downgraded
                        }
                        else if (value5 == 0x94BF)
                        {
                            version = new GameVersion("GTA: San Andreas (3.0 Steam)", 0x75130); // 3.0 Steam
                        }
                        else if (value6 == 0x94BF)
                        {
                            version = new GameVersion("GTA: San Andreas (Steam?)", 0x75770); // 1.0 Steam ?
                        }
                        else if (value7 == 0x94BF)
                        {
                            version = new GameVersion("GTA: San Andreas (3.0 Steam, latest patch)", 0x6C1934); // 0xEA7970 //0x6C1934
                        }
                        else
                        {
                            //   System.Windows.Forms.MessageBox.Show("");
                            version = new GameVersion("GTA: San Andreas (Unknown)", 0);
                        }
                    }
                    else if (process.ProcessName == "gta3")
                    {
                        long value = Memory.GetMemoryResult(process, 0x005C1E70, 4);
                        long value2 = Memory.GetMemoryResult(process, 0x005C2130, 4);
                        long value3 = Memory.GetMemoryResult(process, 0x005C6FD0, 4);

                        if (value == 0x53E58955)
                        {
                            version = new GameVersion("GTA: III (1.0)", -0x10140); // v1.0 noCD
                        }
                        else if (value2 == 0x53E58955)
                        {
                            version = new GameVersion("GTA: III (1.1)", -0x10140); // 1.1 noCD
                        }
                        else if (value3 == 0x53E58955)
                        {
                            version = new GameVersion("GTA: III (1.1 Steam)", 0); // 1.1 Steam
                        }
                        else
                        {
                            //     System.Windows.Forms.MessageBox.Show("Test");
                            version = new GameVersion("GTA: III (Unknown)", 0);
                        }
                    }
                    else
                    {
                        version = new GameVersion(" - Game not running or unrecognized version - ", 0);
                    }


                }
                else
                {
                    version = new GameVersion(" - Game not running or unrecognized version - ", 0);
                }
            }
            catch (Exception ex)
            {
                version = new GameVersion(" - Unable to check current game - ", 0);
                Console.WriteLine(ex.Message);
            }
            return version;
        }

        /// <summary>
        /// Checks if the game is still running
        /// </summary>
        /// <param name="gv"></param>
        /// <returns>Boolean</returns>
        public bool DetectCurrentVersion(GameVersion gv)
        {
            Memory m = new Memory();

            try
            {
                if (isProcessActive(gv.Process))
                {
                    Console.WriteLine("Game: " + gv.GameName + @" --- Address: " + Memory.GetMemoryResult(gv.Process, 0x00608578, 4));

                    if (GameVersionDetector.getGameInformation(gv.Process).GameName != null || !GameVersionDetector.getGameInformation(gv.Process).GameName.Contains("Unrecognized"))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public static bool isProcessActive(Process process)
        {
            if (process == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

    }
}
