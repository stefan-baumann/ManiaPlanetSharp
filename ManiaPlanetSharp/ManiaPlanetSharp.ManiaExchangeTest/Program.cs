using ManiaPlanetSharp.ManiaExchange;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManiaPlanetSharp.ManiaExchangeTest
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.Write("Please enter a ManiaExchange track ID: ");
                string input = Console.ReadLine();
                if (int.TryParse(input, out int id))
                {
                    TrackInfo track = ManiaExchange.ManiaExchange.GetTrackInfo(id).Result;
                    Console.WriteLine($@"{track.Name} ({track.TrackUID}) by {track.Username} ({track.UserID})
Awards: {track.AwardCount}, Value: {track.TrackValue}
Length: {track.LengthName}
Environment: {track.VehicleName} in {track.EnvironmentName} ({track.TitlePack})");
                }
            }
        }
    }
}
