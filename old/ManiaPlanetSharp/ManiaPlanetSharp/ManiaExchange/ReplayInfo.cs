using System;
using System.Collections.Generic;
using System.Text;

namespace ManiaPlanetSharp.ManiaExchange
{
    public class ReplayInfo
    {
        public ReplayInfo()
        { }

        public int ReplayID { get; set; }
        public int UserID { get; set; }
        public string Username { get; set; }
        public int TrackID { get; set; }
        public string UploadedAt { get; set; }
        public int ReplayTime { get; set; }
        public int StuntScore { get; set; }
        public int Respawns { get; set; }
        public int Position { get; set; }
        public int Beaten { get; set; }
        public int Percentage { get; set; }
        public double ReplayPoints { get; set; }
        public int NadeoPoints { get; set; }

        public static implicit operator int(ReplayInfo replay)
        {
            return replay.ReplayID;
        }
    }
}
