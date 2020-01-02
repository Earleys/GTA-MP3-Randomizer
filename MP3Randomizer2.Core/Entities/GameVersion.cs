using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace MP3Randomizer2.Core
{
    public class GameVersion
    {
        public Process Process { get; set; }
        public string GameName { get; set; }
        public int Offset { get; set; }
        public int MemAddress { get; set; }

        public GameVersion(Process process, string gameName, int offset, int memAddress)
        {
            this.Process = process;
            this.GameName = gameName;
            this.MemAddress = memAddress;
            this.Offset = offset;
        }

        public GameVersion(Process process, GameVersion gameVersion, int memAddress)
        {
            this.Process = process;
            this.GameName = gameVersion.GameName;
            this.Offset = gameVersion.Offset;
            this.MemAddress = memAddress;
        }

        public GameVersion(string gameName, int offset)
        {
            this.GameName = gameName;
            this.Offset = offset;
        }

        public GameVersion()
        {

        }
    }
}
