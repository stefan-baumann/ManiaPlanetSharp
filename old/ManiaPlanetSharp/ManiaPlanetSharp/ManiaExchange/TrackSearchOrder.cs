using System;
using System.Collections.Generic;
using System.Text;

namespace ManiaPlanetSharp.ManiaExchange
{
    public enum TrackSearchOrder
    {
        Default = -1,
        Track = 0,
        Author = 1,
        UploadedNewest = 2,
        UploadedOldest = 3,
        UpdatedNewest = 4,
        UpdatedOldest = 5,
        ActivityNewest = 6,
        ActivityOldest = 7,
        AwardsMost = 8,
        AwardsLeast = 9,
        CommentsMost = 10,
        CommentsLeast = 11,
        DifficultyEasiest = 12,
        DifficultyHardest = 13,
        LengthShortest = 14,
        LengthLongest = 15,
        TrackValueLowest = 24,
        TrackValueHighest = 25,
        OnlineRatingLowest = 26,
        OnlineRatingHighest = 27,
    }
}
