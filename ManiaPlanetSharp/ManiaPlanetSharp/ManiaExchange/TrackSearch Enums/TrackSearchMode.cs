using System;
using System.Collections.Generic;
using System.Text;

namespace ManiaPlanetSharp.ManiaExchange
{
    public enum TrackSearchMode
        : int
    {
        Default = 0,
        SearchByUser = 1,
        Latest = 2,
        RecentlyAwarded = 3,
        BestOfTheWeek = 4,
        BestOfTheMonth = 5,
        //Favorite = 6,
        //Downloaded = 7,
        //Awarded = 8,
        //TracksWithOwnReplay = 9,
        MXSupporter = 10,
        Duo = 11,
        MostCompetitiveOfTheWeek = 19,
        MostCompetitiveOfTheMonth = 20,
        BestOfTheWeekByRating = 21,
        BestOfTheMonthByRating = 22
    }
}
