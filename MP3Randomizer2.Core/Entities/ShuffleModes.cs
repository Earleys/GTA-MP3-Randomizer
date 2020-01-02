namespace MP3Randomizer2.Core.Entities
{
    /// <summary>
    /// The existing shuffle modes.
    /// !!! When adding a new mode, add it to the bottom of the list !!!
    /// </summary>
    public enum ShuffleModes
    {
        randomizeEverything,
        randomizeFromRandomPoint,
        randomizeFirstSongOnly,
        randomizeEverythingButFirstSong
    }
}
